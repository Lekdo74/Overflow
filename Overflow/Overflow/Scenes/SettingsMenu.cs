using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using System.Collections.Generic;
using System;
using System.Linq;
using Overflow.src;

namespace Overflow.Scenes
{
    public class SettingsMenu : GameScreen
    {
        private new Main Game => (Main)base.Game;
        public SettingsMenu(Main game) : base(game) { }

        private SpriteFont font;
        private List<Button> buttons;

        Texture2D flecheGauche;
        Texture2D flecheDroite;
        private string textResolution;
        private Vector2 textResolutionPosition;

        private string textFps;
        private Vector2 textFpsPosition;

        private string textFullscreen;
        private Vector2 textFullscreenPosition;
        Texture2D caseVide;
        Texture2D caseCochee;

        public override void Initialize()
        {
            Game.IsMouseVisible = true;
            font = Content.Load<SpriteFont>("Font");

            textResolution = $"  {Settings.availableResolutionsStrings[Settings.currentResolution]}  ";
            textResolutionPosition = new Vector2((Settings.nativeWidthResolution / 2) - (font.MeasureString(textResolution).X / 2), (int)(Settings.nativeHeightResolution * 0.25f) - (font.MeasureString(textResolution).Y / 2));

            textFullscreen = "Fullscreen  ";
            textFullscreenPosition = new Vector2((Settings.nativeWidthResolution / 2) - (font.MeasureString(textFullscreen).X / 2), (int)(Settings.nativeHeightResolution * 0.75f) - (font.MeasureString(textFullscreen).Y / 2));
            caseVide = Content.Load<Texture2D>("case");
            caseCochee = Content.Load<Texture2D>("casecochee");

            textFps = $"  {Settings.availableFps[Settings.currentFps]} FPS  ";
            textFpsPosition = new Vector2((Settings.nativeWidthResolution / 2) - (font.MeasureString(textFps).X / 2), (int)(Settings.nativeHeightResolution * 0.5f) - (font.MeasureString(textFps).Y / 2));

            base.Initialize();
        }

        public override void LoadContent()
        {
            flecheGauche = Content.Load<Texture2D>("flechegauche");
            flecheDroite = Content.Load<Texture2D>("flechedroite");

            Button leftArrowResolutionButton = new Button(flecheGauche)
            {
                Position = new Vector2(textResolutionPosition.X - flecheGauche.Width, textResolutionPosition.Y - flecheGauche.Height / 2 + font.MeasureString(textResolution).Y / 2)
            };
            leftArrowResolutionButton.Click += leftArrowResolutionButton_Click;

            Button rightArrowResolutionButton = new Button(flecheDroite)
            {
                Position = new Vector2(textResolutionPosition.X + font.MeasureString(textResolution).X, textResolutionPosition.Y - flecheDroite.Height / 2 + font.MeasureString(textResolution).Y / 2)
            };
            rightArrowResolutionButton.Click += RightArrowResolutionButton_Click;

            Button leftArrowFpsButton = new Button(flecheGauche)
            {
                Position = new Vector2(textFpsPosition.X - flecheGauche.Width, textFpsPosition.Y - flecheGauche.Height / 2 + font.MeasureString(textFps).Y / 2)
            };
            leftArrowFpsButton.Click += LeftArrowFpsButton_Click;

            Button rightArrowFpsButton = new Button(flecheDroite)
            {
                Position = new Vector2(textFpsPosition.X + font.MeasureString(textFps).X, textFpsPosition.Y - flecheGauche.Height / 2 + font.MeasureString(textFps).Y / 2)
            };
            rightArrowFpsButton.Click += RightArrowFpsButton_Click;

            Button fullscreenButton = new Button(caseVide)
            {
                Position = new Vector2(textFullscreenPosition.X + font.MeasureString(textFullscreen).X, textFullscreenPosition.Y - flecheDroite.Height / 2 + font.MeasureString(textFullscreen).Y / 2)
            };
            fullscreenButton.Click += FullscreenButton_Click;

            Button returnButton = new Button(Content.Load<Texture2D>("bouton"), font)
            {
                Position = new Vector2(20, 40),
                Text = "Retour"
            };
            returnButton.Click += QuitButton_Click;

            buttons = new List<Button>()
            {
                leftArrowResolutionButton, rightArrowResolutionButton, leftArrowFpsButton, rightArrowFpsButton, fullscreenButton, returnButton
            };
            base.LoadContent();
        }


        private void leftArrowResolutionButton_Click(object sender, EventArgs e)
        {
            if(Settings.currentResolution >= 1 && !Settings.fullscreen)
            {
                Settings.currentResolution -= 1;
                textResolution = $"  {Settings.availableResolutionsStrings[Settings.currentResolution]}  ";
                Settings.currentWidthResolution = (int)Settings.availableResolutions[Settings.currentResolution].X;
                Settings.currentHeightResolution = (int)Settings.availableResolutions[Settings.currentResolution].Y;
                Game._graphics.PreferredBackBufferWidth = Settings.currentWidthResolution;
                Game._graphics.PreferredBackBufferHeight = Settings.currentHeightResolution;
                Game._graphics.ApplyChanges();
                textResolutionPosition = new Vector2((Settings.nativeWidthResolution / 2) - (font.MeasureString(textResolution).X / 2), (int)(Settings.nativeHeightResolution * 0.25f) - (font.MeasureString(textResolution).Y / 2));
                buttons[0].Position = new Vector2(textResolutionPosition.X - flecheGauche.Width, textResolutionPosition.Y - flecheGauche.Height / 2 + font.MeasureString(textResolution).Y / 2);
                buttons[1].Position = new Vector2(textResolutionPosition.X + font.MeasureString(textResolution).X, textResolutionPosition.Y - flecheDroite.Height / 2 + font.MeasureString(textResolution).Y / 2);
            }
        }

        private void RightArrowResolutionButton_Click(object sender, EventArgs e)
        {
            if (Settings.currentResolution <= Settings.availableResolutions.Length - 2 && !Settings.fullscreen)
            {
                Settings.currentResolution += 1;
                textResolution = $"  {Settings.availableResolutionsStrings[Settings.currentResolution]}  ";
                Settings.currentWidthResolution = (int)Settings.availableResolutions[Settings.currentResolution].X;
                Settings.currentHeightResolution = (int)Settings.availableResolutions[Settings.currentResolution].Y;
                Game._graphics.PreferredBackBufferWidth = Settings.currentWidthResolution;
                Game._graphics.PreferredBackBufferHeight = Settings.currentHeightResolution;
                Game._graphics.ApplyChanges();
                textResolutionPosition = new Vector2((Settings.nativeWidthResolution / 2) - (font.MeasureString(textResolution).X / 2), (int)(Settings.nativeHeightResolution * 0.25f) - (font.MeasureString(textResolution).Y / 2));
                buttons[0].Position = new Vector2(textResolutionPosition.X - flecheGauche.Width, textResolutionPosition.Y - flecheGauche.Height / 2 + font.MeasureString(textResolution).Y / 2);
                buttons[1].Position = new Vector2(textResolutionPosition.X + font.MeasureString(textResolution).X, textResolutionPosition.Y - flecheDroite.Height / 2 + font.MeasureString(textResolution).Y / 2);
            }
        }

        private void LeftArrowFpsButton_Click(object sender, EventArgs e)
        {
            if(Settings.currentFps >= 1)
            {
                Settings.currentFps -= 1;
                textFps = $"  {Settings.availableFps[Settings.currentFps]} FPS  ";
                Game.TargetElapsedTime = TimeSpan.FromSeconds(1f / Settings.availableFps[Settings.currentFps]);
                Game._graphics.ApplyChanges();
                textFpsPosition = new Vector2((Settings.nativeWidthResolution / 2) - (font.MeasureString(textFps).X / 2), (int)(Settings.nativeHeightResolution * 0.5f) - (font.MeasureString(textFps).Y / 2));
                buttons[2].Position = new Vector2(textFpsPosition.X - flecheGauche.Width, textFpsPosition.Y - flecheGauche.Height / 2 + font.MeasureString(textFps).Y / 2);
                buttons[3].Position = new Vector2(textFpsPosition.X + font.MeasureString(textFps).X, textFpsPosition.Y - flecheDroite.Height / 2 + font.MeasureString(textFps).Y / 2);
            }
        }

        private void RightArrowFpsButton_Click(object sender, EventArgs e)
        {
            if (Settings.currentFps <= Settings.availableFps.Length - 2)
            {
                Settings.currentFps += 1;
                textFps = $"  {Settings.availableFps[Settings.currentFps]} FPS  ";
                Game.TargetElapsedTime = TimeSpan.FromSeconds(1f / Settings.availableFps[Settings.currentFps]);
                Game._graphics.ApplyChanges();
                textFpsPosition = new Vector2((Settings.nativeWidthResolution / 2) - (font.MeasureString(textFps).X / 2), (int)(Settings.nativeHeightResolution * 0.5f) - (font.MeasureString(textFps).Y / 2));
                buttons[2].Position = new Vector2(textFpsPosition.X - flecheGauche.Width, textFpsPosition.Y - flecheGauche.Height / 2 + font.MeasureString(textFps).Y / 2);
                buttons[3].Position = new Vector2(textFpsPosition.X + font.MeasureString(textFps).X, textFpsPosition.Y - flecheDroite.Height / 2 + font.MeasureString(textFps).Y / 2);
            }
        }

        private void FullscreenButton_Click(object sender, EventArgs e)
        {
            if (Settings.fullscreen)
            {
                buttons[4].Texture = caseVide;
                Settings.fullscreen = false;
                Game._graphics.IsFullScreen = false;
                textResolution = $"  {Settings.availableResolutionsStrings[Settings.currentResolution]}  ";
                Settings.currentWidthResolution = (int)Settings.availableResolutions[Settings.currentResolution].X;
                Settings.currentHeightResolution = (int)Settings.availableResolutions[Settings.currentResolution].Y;
                Game._graphics.PreferredBackBufferWidth = Settings.currentWidthResolution;
                Game._graphics.PreferredBackBufferHeight = Settings.currentHeightResolution;
                Game._graphics.ApplyChanges();
                textResolutionPosition = new Vector2((Settings.nativeWidthResolution / 2) - (font.MeasureString(textResolution).X / 2), (int)(Settings.nativeHeightResolution * 0.25f) - (font.MeasureString(textResolution).Y / 2));
                buttons[0].Position = new Vector2(textResolutionPosition.X - flecheGauche.Width, textResolutionPosition.Y - flecheGauche.Height / 2 + font.MeasureString(textResolution).Y / 2);
                buttons[1].Position = new Vector2(textResolutionPosition.X + font.MeasureString(textResolution).X, textResolutionPosition.Y - flecheDroite.Height / 2 + font.MeasureString(textResolution).Y / 2);
            }
            else
            {
                buttons[4].Texture = caseCochee;
                Settings.fullscreen = true;
                Game._graphics.IsFullScreen = true;
                textResolution = $"  {Game._graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width}x{Game._graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height}  ";
                Settings.currentWidthResolution = Game._graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
                Settings.currentHeightResolution = Game._graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
                Game._graphics.PreferredBackBufferWidth = Settings.currentWidthResolution;
                Game._graphics.PreferredBackBufferHeight = Settings.currentHeightResolution;
                Game._graphics.ApplyChanges();
                textResolutionPosition = new Vector2((Settings.nativeWidthResolution / 2) - (font.MeasureString(textResolution).X / 2), (int)(Settings.nativeHeightResolution * 0.25f) - (font.MeasureString(textResolution).Y / 2));
                buttons[0].Position = new Vector2(textResolutionPosition.X - flecheGauche.Width, textResolutionPosition.Y - flecheGauche.Height / 2 + font.MeasureString(textResolution).Y / 2);
                buttons[1].Position = new Vector2(textResolutionPosition.X + font.MeasureString(textResolution).X, textResolutionPosition.Y - flecheDroite.Height / 2 + font.MeasureString(textResolution).Y / 2);
            }
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Game.LoadMainMenu();
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
            Game._spriteBatch.DrawString(font, textResolution, textResolutionPosition, Color.Black);
            Game._spriteBatch.DrawString(font, textFullscreen, textFullscreenPosition, Color.Black);
            Game._spriteBatch.DrawString(font, textFps, textFpsPosition, Color.Black);
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
