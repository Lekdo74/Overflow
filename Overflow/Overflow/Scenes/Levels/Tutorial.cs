using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Overflow.src;
using System;

namespace Overflow.Scenes
{
    public class Tutorial : GameScreen
    {
        private new Main Game => (Main)base.Game;
        public Tutorial(Main game) : base(game) { }

        private Map map;
        private Room currentRoom;

        private Vector2 initPositionPlayer;

        public override void Initialize()
        {
            base.Initialize();

            map = new Map(10, new Texture2D[] { Content.Load<Texture2D>("MapTiles/murHautGauche"), Content.Load<Texture2D>("MapTiles/murHautDroite"), Content.Load<Texture2D>("MapTiles/murBasDroite"), Content.Load<Texture2D>("MapTiles/murBasGauche"), Content.Load<Texture2D>("MapTiles/murHaut"), Content.Load<Texture2D>("MapTiles/murDroite"), Content.Load<Texture2D>("MapTiles/murBas"), Content.Load<Texture2D>("MapTiles/murGauche"), Content.Load<Texture2D>("MapTiles/herbe"), Content.Load<Texture2D>("MapTiles/porte"), Content.Load<Texture2D>("MapTiles/coinHautGauche"), Content.Load<Texture2D>("MapTiles/coinHautDroite"), Content.Load<Texture2D>("MapTiles/coinBasDroite"), Content.Load<Texture2D>("MapTiles/coinBasGauche") });
            currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];

            Player.Texture = Art.player;
            Player.Position = currentRoom.SpawnPoint;
            Player.Speed = 50;

            Player.CanPassThroughDoor = true;
            Player.PreviousTile = currentRoom.GetTile(Player.Position).Type;
            Player.CurrentTile = currentRoom.GetTile(Player.Position).Type;
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        
        public override void Update(GameTime gameTime)
        {
            PlayerInputs.KeyBoardState = Keyboard.GetState();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Player.Update(gameTime, currentRoom.Obstacles);

            ChangeRoom();
            map.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Game.renderTarget);
            Game.GraphicsDevice.Clear(Color.Black);

            Game._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            map.Draw(gameTime, Game._spriteBatch);
            Player.Draw(gameTime, Game._spriteBatch);
            Game._spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            Game._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            Game._spriteBatch.Draw(Game.renderTarget, new Rectangle(0, 0, Settings.currentWidthResolution, Settings.currentHeightResolution), Color.White);
            Game._spriteBatch.End();
        }

        private void ChangeRoom()
        {
            Player.CurrentTile = currentRoom.GetTile(Player.Position).Type;
            if (!Player.CanPassThroughDoor && (Player.PreviousTile == "DoorTop" || Player.PreviousTile == "DoorRight" || Player.PreviousTile == "DoorBottom" || Player.PreviousTile == "DoorLeft") && Player.CurrentTile == "Grass")
            {
                Player.CanPassThroughDoor = true;
            }
            Player.PreviousTile = Player.CurrentTile;

            if (Player.CanPassThroughDoor)
            {
                switch (Player.CurrentTile)
                {
                    case "DoorTop":
                        map.CurrentRoom = new int[] { map.CurrentRoom[0], map.CurrentRoom[1] - 1 };
                        currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[2];
                        Player.Position -= new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2);
                        Player.CanPassThroughDoor = false;
                        break;
                    case "DoorRight":
                        map.CurrentRoom = new int[] { map.CurrentRoom[0] + 1, map.CurrentRoom[1] };
                        currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[3];
                        Player.Position -= new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2);
                        Player.CanPassThroughDoor = false;
                        break;
                    case "DoorBottom":
                        map.CurrentRoom = new int[] { map.CurrentRoom[0], map.CurrentRoom[1] + 1 };
                        currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[0];
                        Player.Position -= new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2);
                        Player.CanPassThroughDoor = false;
                        break;
                    case "DoorLeft":
                        map.CurrentRoom = new int[] { map.CurrentRoom[0] - 1, map.CurrentRoom[1] };
                        currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                        Player.Position = currentRoom.SpawnPoints[1];
                        Player.Position -= new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2);
                        Player.CanPassThroughDoor = false;
                        break;
                }
            }
        }
    }
}
