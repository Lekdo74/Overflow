using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using Overflow.src;
using System;
using System.Collections.Generic;

namespace Overflow.Scenes
{
    public class EndScreen : GameScreen
    {
        private new Main Game => (Main)base.Game;
        public EndScreen(Main game) : base(game) { }

        private float remainingTime;

        private List<Button> buttons;

        private string textEnd;
        private Vector2 textEndPosition;

        private string textMerci;
        private Vector2 textMerciPosition;

        public override void Initialize()
        {
            Game.IsMouseVisible = true;
            Sound.ChangeBackgroundMusic(Sound.ending);

            remainingTime = 116f;

            textEnd = "The End";
            textEndPosition = new Vector2(160, 50);

            textMerci = "Merci d'avoir joue ;)";
            textMerciPosition = new Vector2(25, 120);

            base.Initialize();
        }

        public override void LoadContent()
        {
            Button menuButton = new Button("Retour au menu")
            {
                Position = new Vector2(145, 200)
            };
            menuButton.Click += MenuButton_Click;

            buttons = new List<Button>()
            {
                menuButton
            };

            base.LoadContent();
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Sound.PlaySound(Sound.buttonSoundEffect);
            Game.LoadMainMenu();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in buttons)
            {
                button.Update(gameTime);
            }

            remainingTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (remainingTime <= 0)
                Game.LoadMainMenu();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Main.renderTarget);
            Game.GraphicsDevice.Clear(Color.Black);

            Main._spriteBatch.Begin();
            Main._spriteBatch.DrawString(Art.fontBig, textEnd, textEndPosition, Color.White);
            Main._spriteBatch.DrawString(Art.fontBig, textMerci, textMerciPosition, Color.White);
            foreach (Button button in buttons)
            {
                button.Draw(gameTime, Main._spriteBatch);
            }
            Main._spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            Main._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            Main._spriteBatch.Draw(Main.renderTarget, new Rectangle(0, 0, Settings.currentWidthResolution, Settings.currentHeightResolution), Color.White);
            Main._spriteBatch.End();
        }
    }
}
