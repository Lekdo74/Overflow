using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Overflow.src;

namespace Overflow.Scenes
{
    public class RoomTest : GameScreen
    {
        private new Main Game => (Main)base.Game;
        public RoomTest(Main game) : base(game) { }

        private TileSet tileset;
        private Room room;

    public override void Initialize()
        {
            base.Initialize();

            tileset = Art.tilesetLevel1;
            room = PremadeRooms.Room(new bool[] { true, true, true, true }, 1, new int[] { 2, 6}, tileset, Art.enemysetLevel1, Sound.tutorial);

            Player.Texture = Art.player;
            Player.Position = room.SpawnPoint;
            Player.Speed = 50;
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Main.renderTarget);
            Game.GraphicsDevice.Clear(Color.Black);

            Main._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            room.Draw(gameTime, Main._spriteBatch);
            Player.Draw(gameTime, Main._spriteBatch);
            Main._spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            Main._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            Main._spriteBatch.Draw(Main.renderTarget, new Rectangle(0, 0, Settings.currentWidthResolution, Settings.currentHeightResolution), Color.White);
            Main._spriteBatch.End();
        }
    }
}
