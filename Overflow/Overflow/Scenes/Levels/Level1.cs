using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Overflow.src;
using System;

namespace Overflow.Scenes
{
    public class Level1 : GameScreen
    {
        private new Main Game => (Main)base.Game;
        public Level1(Main game) : base(game) { }

        private Texture2D[] tileset;
        private Room room;

    public override void Initialize()
        {
            base.Initialize();

            tileset = new Texture2D[] { Content.Load<Texture2D>("MapTiles/coinHautGauche"), Content.Load<Texture2D>("MapTiles/coinHautDroite"), Content.Load<Texture2D>("MapTiles/coinBasDroite"), Content.Load<Texture2D>("MapTiles/coinBasGauche"), Content.Load<Texture2D>("MapTiles/murHaut"), Content.Load<Texture2D>("MapTiles/murDroite"), Content.Load<Texture2D>("MapTiles/murBas"), Content.Load<Texture2D>("MapTiles/murGauche"), Content.Load<Texture2D>("MapTiles/herbe"), Content.Load<Texture2D>("MapTiles/porte") };
            room = PremadeRooms.Room(new bool[] { true, false, false, false }, tileset);
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
            GraphicsDevice.SetRenderTarget(Game.renderTarget);
            Game.GraphicsDevice.Clear(Color.Blue);

            Game._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            room.Draw(gameTime, Game._spriteBatch);
            Game._spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            Game._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            Game._spriteBatch.Draw(Game.renderTarget, new Rectangle(0, 0, Settings.currentWidthResolution, Settings.currentHeightResolution), Color.White);
            Game._spriteBatch.End();
        }
    }
}
