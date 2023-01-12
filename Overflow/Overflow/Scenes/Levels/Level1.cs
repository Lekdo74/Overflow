using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Overflow.src;
using System.Collections.Generic;

namespace Overflow.Scenes
{
    public class Level1 : GameScreen
    {
        private new Main Game => (Main)base.Game;
        public Level1(Main game) : base(game) { }

        private Map map;
        private Room currentRoom;

        public override void Initialize()
        {
            base.Initialize();
            Sound.ChangeBackgroundMusic(Sound.level1);

            map = new Map(10, new int[] {3, 7}, Art.tilesetLevel1, Art.enemysetLevel1, Sound.level1);
            
            currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];

            Player.InitializePlayer();
            Player.Position = currentRoom.SpawnPoint;

            Boss.InitializeBoss();
            Boss.HealthTexture = new Texture2D(GraphicsDevice, 1, 1);
            Boss.HealthTexture.SetData(new[] { new Color(173, 200, 56) });

            Player.CanPassThroughDoor = true;
            Player.PreviousTile = currentRoom.GetPlayerTile();
            Player.CurrentTile = currentRoom.GetPlayerTile();
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        
        public override void Update(GameTime gameTime)
        {
            PlayerInputs.KeyBoardState = Keyboard.GetState();
            PlayerInputs.MouseState = Mouse.GetState();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Player.Update(gameTime, currentRoom);
            Player.TakeDamage(Player.CheckDamage(currentRoom));
            if(Player.Health <= 0)
            {
                Game.LoadMainMenu();
            }
            map.Update(gameTime, Game);

            Player.CurrentTile = currentRoom.GetPlayerTile();
            if (Player.CurrentTile != Player.PreviousTile)
            {
                foreach (Enemy enemy in currentRoom.Enemies)
                {
                    enemy.CalculPath = true;
                }
            }

            ChangeRoom();

            Player.PreviousTile = Player.CurrentTile;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Main.renderTarget);
            Game.GraphicsDevice.Clear(Color.Black);

            Main._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            map.Draw(gameTime, Main._spriteBatch);
            PlayerHealthBar.Draw(Main._spriteBatch);
            Player.Draw(gameTime, Main._spriteBatch);
            Main._spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            Main._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            Main._spriteBatch.Draw(Main.renderTarget, new Rectangle(0, 0, Settings.currentWidthResolution, Settings.currentHeightResolution), Color.White);
            Main._spriteBatch.End();
        }

        private void ChangeRoom()
        {
            if (!Player.CanPassThroughDoor && (Player.PreviousTile.Type == "DoorTop" || Player.PreviousTile.Type == "DoorRight" || Player.PreviousTile.Type == "DoorBottom" || Player.PreviousTile.Type == "DoorLeft") && Player.CurrentTile.Type == "Grass")
            {
                Player.CanPassThroughDoor = true;
            }

            if (Player.CanPassThroughDoor)
            {
                switch (Player.CurrentTile.Type)
                {
                    case "DoorTop":
                        currentRoom.Projectiles = new List<Projectile>();
                        map.CurrentRoom = new int[] { map.CurrentRoom[0], map.CurrentRoom[1] - 1 };
                        currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[2];
                        ChangedRoom();
                        break;
                    case "DoorRight":
                        currentRoom.Projectiles = new List<Projectile>();
                        map.CurrentRoom = new int[] { map.CurrentRoom[0] + 1, map.CurrentRoom[1] };
                        currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[3];
                        ChangedRoom();
                        break;
                    case "DoorBottom":
                        currentRoom.Projectiles = new List<Projectile>();
                        map.CurrentRoom = new int[] { map.CurrentRoom[0], map.CurrentRoom[1] + 1 };
                        currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[0];
                        ChangedRoom();
                        break;
                    case "DoorLeft":
                        currentRoom.Projectiles = new List<Projectile>();
                        map.CurrentRoom = new int[] { map.CurrentRoom[0] - 1, map.CurrentRoom[1] };
                        currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[1];
                        ChangedRoom();
                        break;
                }
            }
        }

        private void ChangedRoom()
        {
            Sound.PlaySound(Sound.changeRoom);
            Player.Position -= new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2);
            Player.GhostEffectDashes = new List<GhostEffectDash>();
            Player.CanPassThroughDoor = false;
            Sound.ChangeBackgroundMusic(currentRoom.BackgroundMusic);
        }
    }
}
