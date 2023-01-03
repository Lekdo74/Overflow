using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Overflow.src;

namespace Overflow.Scenes
{
    public class Tutorial : GameScreen
    {
        private new Main Game => (Main)base.Game;
        public Tutorial(Main game) : base(game) { }

        private Map map;
        private Room currentRoom;
        private Player player;
        private Vector2 initPositionPlayer;
        private bool canPassThroughDoor;
        private string currentTile;
        private string previousTile;

        public override void Initialize()
        {
            base.Initialize();

            map = new Map(10, new Texture2D[] { Content.Load<Texture2D>("MapTiles/coinHautGauche"), Content.Load<Texture2D>("MapTiles/coinHautDroite"), Content.Load<Texture2D>("MapTiles/coinBasDroite"), Content.Load<Texture2D>("MapTiles/coinBasGauche"), Content.Load<Texture2D>("MapTiles/murHaut"), Content.Load<Texture2D>("MapTiles/murDroite"), Content.Load<Texture2D>("MapTiles/murBas"), Content.Load<Texture2D>("MapTiles/murGauche"), Content.Load<Texture2D>("MapTiles/herbe"), Content.Load<Texture2D>("MapTiles/porte") });
            currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];

            player = new Player(new Vector2(Settings.nativeWidthResolution / 2, Settings.nativeHeightResolution / 2), Content.Load<Texture2D>("perso"), 50);
            player.Position = currentRoom.Position + currentRoom.Size * 20 / 2 - new Vector2(player.Texture.Width / 2, player.Texture.Height / 2);

            canPassThroughDoor = true;
            previousTile = currentRoom.GetTile(player.Position, player).Type;
            currentTile = currentRoom.GetTile(player.Position, player).Type;
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        
        public override void Update(GameTime gameTime)
        {
            Game._keyboardState = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            initPositionPlayer = player.Position;
            player.Position += PlayerInputs.GetPlayerDirection(Game._keyboardState) * player.Speed * deltaTime;

            foreach (Rectangle obstacle in currentRoom.Obstacles)
            {
                if (player._rectangle.Intersects(obstacle) || !currentRoom.InsideRoom(player))
                {
                    player.Position = initPositionPlayer;
                }
            }

            currentTile = currentRoom.GetTile(player.Position, player).Type;
            if (!canPassThroughDoor && (previousTile == "DoorTop" || previousTile == "DoorRight" || previousTile == "DoorBottom" || previousTile == "DoorLeft") && currentTile == "Grass")
            {
                canPassThroughDoor = true;
            }
            previousTile = currentTile;

            if (canPassThroughDoor)
            {
                if(currentTile == "DoorTop")
                {
                    ChangeRoom("top");
                }
                else if (currentTile == "DoorRight")
                {
                    ChangeRoom("right");
                }
                else if (currentTile == "DoorBottom")
                {
                    ChangeRoom("bottom");
                }
                else if (currentTile == "DoorLeft")
                {
                    ChangeRoom("left");
                }
            }
            
            //Console.WriteLine(currentRoom.GetTile(player._position, player)._type);
        }

        private void ChangeRoom(string direction)
        {
            switch (direction)
            {
                case "top":
                    map.CurrentRoom = new int[] { map.CurrentRoom[0], map.CurrentRoom[1] - 1 };
                    currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                    player.Position = currentRoom.SpawnPoints[2];
                    break;
                case "right":
                    map.CurrentRoom = new int[] { map.CurrentRoom[0] + 1, map.CurrentRoom[1] };
                    currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                    player.Position = currentRoom.SpawnPoints[3];
                    break;
                case "bottom":
                    map.CurrentRoom = new int[] { map.CurrentRoom[0], map.CurrentRoom[1] + 1 };
                    currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                    player.Position = currentRoom.SpawnPoints[0];
                    break;
                case "left":
                    map.CurrentRoom = new int[] { map.CurrentRoom[0] - 1, map.CurrentRoom[1] };
                    currentRoom = map.Rooms[map.CurrentRoom[0], map.CurrentRoom[1]];
                    player.Position = currentRoom.SpawnPoints[1];
                    break;
            }
            player.Position -= new Vector2(player.Texture.Width / 2, player.Texture.Height / 2);
            canPassThroughDoor = false;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Game.renderTarget);
            Game.GraphicsDevice.Clear(Color.Black);

            Game._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            map.Draw(gameTime, Game._spriteBatch);
            player.Draw(gameTime, Game._spriteBatch);
            Game._spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            Game._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            Game._spriteBatch.Draw(Game.renderTarget, new Rectangle(0, 0, Settings.currentWidthResolution, Settings.currentHeightResolution), Color.White);
            Game._spriteBatch.End();
        }
    }
}
