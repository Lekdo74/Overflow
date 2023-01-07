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

        public override void Initialize()
        {
            base.Initialize();

            map = new Map(10, Art.tileset1);
            
            currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];

            Player.Texture = Art.player;
            Player.Position = currentRoom.SpawnPoint;
            Player.Speed = 50;

            Player.CanPassThroughDoor = true;
            Player.PreviousTile = currentRoom.GetTile(Player.Position);
            Player.CurrentTile = currentRoom.GetTile(Player.Position);
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        
        public override void Update(GameTime gameTime)
        {
            PlayerInputs.KeyBoardState = Keyboard.GetState();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Player.Update(gameTime, currentRoom);
            map.Update(gameTime);
            Player.CurrentTile = currentRoom.GetTile(Player.Position);
            ChangeRoom();
            Player.PreviousTile = Player.CurrentTile;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Main.renderTarget);
            Game.GraphicsDevice.Clear(Color.Black);

            Main._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            map.Draw(gameTime, Main._spriteBatch);
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
