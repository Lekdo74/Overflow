using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Overflow.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.Scenes
{
    public class MainMenu : GameScreen
    {
        private new Main Game => (Main)base.Game;
        public MainMenu(Main game) : base(game) { }

        private SpriteFont font;
        private List<Button> buttons;

        public override void Initialize()
        {
            Game.IsMouseVisible = true;
            font = Content.Load<SpriteFont>("Font");

            base.Initialize();
        }

        public override void LoadContent()
        {
            Button playButton = new Button(Content.Load<Texture2D>("bouton"), font)
            {
                Position = new Vector2(50, 40),
                Text = "Jouer"
            };
            playButton.Click += PlayButton_Click;

            Button settingsButton = new Button(Content.Load<Texture2D>("bouton"), font)
            {
                Position = new Vector2(50, 100),
                Text = "Options"
            };
            settingsButton.Click += SettingsButton_Click;

            buttons = new List<Button>()
            {
                playButton, settingsButton
            };
            base.LoadContent();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Game.LoadRoomTest();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Game.LoadSettingsMenu();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in buttons)
            {
                button.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Game.renderTarget);
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);

            Game._spriteBatch.Begin();
            foreach (Button button in buttons)
            {
                button.Draw(gameTime, Game._spriteBatch);
            }
            Game._spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            Game._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            Game._spriteBatch.Draw(Game.renderTarget, new Rectangle(0, 0, Settings.currentWidthResolution, Settings.currentHeightResolution), Color.White);
            Game._spriteBatch.End();
        }
    }
}
