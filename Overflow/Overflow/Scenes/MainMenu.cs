using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

        private List<Button> buttons;

        public override void Initialize()
        {
            Game.IsMouseVisible = true;
            Sound.ChangeBackgroundMusic(Sound.menu);

            base.Initialize();
        }

        public override void LoadContent()
        {
            Button playButton = new Button("Jouer")
            {
                Position = new Vector2(125, 120)
            };
            playButton.Click += PlayButton_Click;

            Button settingsButton = new Button("Options")
            {
                Position = new Vector2(275, 120)
            };
            settingsButton.Click += SettingsButton_Click;

            Button quitGameButton = new Button("Quitter")
            {
                Position = new Vector2(20, 230)
            };
            quitGameButton.Click += QuitButton_Click;

            buttons = new List<Button>()
            {
                playButton, settingsButton, quitGameButton
            };
            base.LoadContent();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Game.LoadLevel1();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Game.LoadSettingsMenu();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in buttons)
            {
                button.Update(gameTime);
            }
            PlayerInputs.KeyBoardState = Keyboard.GetState();

            if (PlayerInputs.KeyBoardState.IsKeyDown(Keys.A)){
                Sound.ChangeBackgroundMusic(Sound.menu);
            }
            else if (PlayerInputs.KeyBoardState.IsKeyDown(Keys.Z))
            {
                Sound.ChangeBackgroundMusic(Sound.tutorial);
            }
            else if (PlayerInputs.KeyBoardState.IsKeyDown(Keys.E))
            {
                Sound.ChangeBackgroundMusic(Sound.level1);
            }
            else if (PlayerInputs.KeyBoardState.IsKeyDown(Keys.R))
            {
                Sound.ChangeBackgroundMusic(Sound.level2);
            }
            else if (PlayerInputs.KeyBoardState.IsKeyDown(Keys.T))
            {
                Sound.ChangeBackgroundMusic(Sound.boss);
            }
            else if (PlayerInputs.KeyBoardState.IsKeyDown(Keys.Y))
            {
                Sound.ChangeBackgroundMusic(Sound.ending);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Main.renderTarget);
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);

            Main._spriteBatch.Begin();
            Main._spriteBatch.Draw(Art.backgroundMainMenu, Vector2.Zero, Color.White);
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
