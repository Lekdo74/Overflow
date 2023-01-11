using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using System.Collections.Generic;
using System;
using System.Linq;
using Overflow.src;
using Microsoft.Xna.Framework.Media;
using System.Runtime.InteropServices;

namespace Overflow.Scenes
{
    public class SettingsMenu : GameScreen
    {
        private new Main Game => (Main)base.Game;
        public SettingsMenu(Main game) : base(game) { }
        private List<Button> buttons;

        private string textResolution;
        private Vector2 textResolutionPosition;

        private string textFps;
        private Vector2 textFpsPosition;

        private string textFullscreen;
        private Vector2 textFullscreenPosition;

        private string textNoDamage;
        private Vector2 textNoDamagePosition;

        private string textMusicVolume;
        private Vector2 textMusicVolumePosition;
        private Texture2D musicVolumeBar;
        private Vector2 musicVolumeBarPosition;

        private string textSoundEffectVolume;
        private Vector2 textSoundEffectVolumePosition;
        private Texture2D soundEffectVolumeBar;
        private Vector2 soundEffectVolumeBarPosition;

        public override void Initialize()
        {
            Game.IsMouseVisible = true;

            textResolution = $"{Settings.availableResolutionsStrings[Settings.currentResolution]}";
            textResolutionPosition = new Vector2(140 - (Art.font.MeasureString(textResolution).X / 2), 68 - (Art.font.MeasureString(textResolution).Y / 2));

            textFps = $"{Settings.availableFps[Settings.currentFps]} FPS";
            textFpsPosition = new Vector2(140 - Art.font.MeasureString(textFps).X / 2, 135 - Art.font.MeasureString(textFps).Y / 2);

            textFullscreen = "Fullscreen";
            textFullscreenPosition = new Vector2(170, 190);

            textNoDamage = "No Damage";
            textNoDamagePosition = new Vector2(304, 230);

            musicVolumeBar = Art.volumeBar[Settings.currentMusicVolume];
            textMusicVolume = "Musique";
            musicVolumeBarPosition = new Vector2(317 - Art.font.MeasureString(textMusicVolume).X / 2, 68 - musicVolumeBar.Height / 2);
            textMusicVolumePosition = new Vector2((int)(musicVolumeBarPosition.X + (musicVolumeBar.Width - Art.font.MeasureString(textMusicVolume).X) / 2), (int)(musicVolumeBarPosition.Y - Art.font.MeasureString(textMusicVolume).Y * 1.5f));

            soundEffectVolumeBar = Art.volumeBar[Settings.currentSoundEffectVolume];
            textSoundEffectVolume = "Effets Sonores";
            soundEffectVolumeBarPosition = new Vector2(360 - Art.font.MeasureString(textSoundEffectVolume).X / 2, 135 - soundEffectVolumeBar.Height / 2);
            textSoundEffectVolumePosition = new Vector2((int)(soundEffectVolumeBarPosition.X + (soundEffectVolumeBar.Width - Art.font.MeasureString(textSoundEffectVolume).X) / 2), (int)(soundEffectVolumeBarPosition.Y - Art.font.MeasureString(textSoundEffectVolume).Y * 1.5f));

            base.Initialize();
        }

        public override void LoadContent()
        {
            Button leftArrowResolutionButton = new Button(Art.leftArrow)
            {
                Position = PlaceToLeft(textResolutionPosition, textResolution, Art.leftArrow)
            };
            leftArrowResolutionButton.Click += leftArrowResolutionButton_Click;

            Button rightArrowResolutionButton = new Button(Art.rightArrow)
            {
                Position = PlaceToRight(textResolutionPosition, textResolution, Art.rightArrow)
            };
            rightArrowResolutionButton.Click += RightArrowResolutionButton_Click;

            Button leftArrowFpsButton = new Button(Art.leftArrow)
            {
                Position = PlaceToLeft(textFpsPosition, textFps, Art.leftArrow)
            };
            leftArrowFpsButton.Click += LeftArrowFpsButton_Click;

            Button rightArrowFpsButton = new Button(Art.rightArrow)
            {
                Position = PlaceToRight(textFpsPosition, textFps, Art.rightArrow)
            };
            rightArrowFpsButton.Click += RightArrowFpsButton_Click;

            Button fullscreenButton;
            if (Settings.fullscreen)
            {
                fullscreenButton = new Button(Art.checkedCase)
                {
                    Position = PlaceToRight(textFullscreenPosition, textFullscreen, Art.emptyCase)
                };
            }
            else
            {
                fullscreenButton = new Button(Art.emptyCase)
                {
                    Position = PlaceToRight(textFullscreenPosition, textFullscreen, Art.emptyCase)
                };
            }
            fullscreenButton.Click += FullscreenButton_Click;

            Button noDamage;
            if (Settings.noDamage)
            {
                noDamage = new Button(Art.checkedCase)
                {
                    Position = PlaceToRight(textNoDamagePosition, textNoDamage, Art.emptyCase)
                };
            }
            else
            {
                noDamage = new Button(Art.emptyCase)
                {
                    Position = PlaceToRight(textNoDamagePosition, textNoDamage, Art.emptyCase)
                };
            }
            noDamage.Click += NoDamageButton_Click;

            Button leftArrowMusicVolumeButton = new Button(Art.leftArrow)
            {
                Position = PlaceToLeft(musicVolumeBarPosition, musicVolumeBar, Art.leftArrow)
            };
            leftArrowMusicVolumeButton.Click += LeftArrowMusicVolumeButton_Click;

            Button rightArrowMusicVolumeButton = new Button(Art.rightArrow)
            {
                Position = PlaceToRight(musicVolumeBarPosition, musicVolumeBar, Art.rightArrow)
            };
            rightArrowMusicVolumeButton.Click += RightArrowMusicVolumeButton_Click;

            Button leftArrowSoundEffectVolumeButton = new Button(Art.leftArrow)
            {
                Position = PlaceToLeft(soundEffectVolumeBarPosition, soundEffectVolumeBar, Art.leftArrow)
            };
            leftArrowSoundEffectVolumeButton.Click += LeftArrowSoundEffectVolumeButton_Click;

            Button rightArrowSoundEffectVolumeButton = new Button(Art.rightArrow)
            {
                Position = PlaceToRight(soundEffectVolumeBarPosition, soundEffectVolumeBar, Art.rightArrow)
            };
            rightArrowSoundEffectVolumeButton.Click += RightArrowSoundEffectVolumeButton_Click;

            Button returnButton = new Button("Retour")
            {
                Position = new Vector2(30, 230)
            };

            returnButton.Click += QuitButton_Click;

            buttons = new List<Button>()
            {
                leftArrowResolutionButton, rightArrowResolutionButton, leftArrowFpsButton, rightArrowFpsButton, fullscreenButton, leftArrowMusicVolumeButton, rightArrowMusicVolumeButton, leftArrowSoundEffectVolumeButton, rightArrowSoundEffectVolumeButton, noDamage, returnButton
            };

            if (Settings.fullscreen)
            {
                textResolution = $"{Main._graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width}x{Main._graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height}";
                textResolutionPosition = new Vector2((Settings.nativeWidthResolution * 0.3f) - (Art.font.MeasureString(textResolution).X / 2), (int)(Settings.nativeHeightResolution * 0.25f) - (Art.font.MeasureString(textResolution).Y / 2));
                buttons[0].Position = PlaceToLeft(textResolutionPosition, textResolution, Art.leftArrow);
                buttons[1].Position = PlaceToRight(textResolutionPosition, textResolution, Art.rightArrow);
            }

            base.LoadContent();
        }


        private void leftArrowResolutionButton_Click(object sender, EventArgs e)
        {
            if(Settings.currentResolution >= 1 && !Settings.fullscreen)
            {
                Settings.currentResolution -= 1;
                Sound.PlaySound(Sound.buttonSoundEffect);
                ChangeResolution();
            }
        }

        private void RightArrowResolutionButton_Click(object sender, EventArgs e)
        {
            if (Settings.currentResolution <= Settings.availableResolutions.Length - 2 && !Settings.fullscreen)
            {
                Settings.currentResolution += 1;
                Sound.PlaySound(Sound.buttonSoundEffect);
                ChangeResolution();
            }
        }

        private void LeftArrowFpsButton_Click(object sender, EventArgs e)
        {
            if(Settings.currentFps >= 1)
            {
                Settings.currentFps -= 1;
                Sound.PlaySound(Sound.buttonSoundEffect);
                ChangeFPS();
            }
        }

        private void RightArrowFpsButton_Click(object sender, EventArgs e)
        {
            if (Settings.currentFps <= Settings.availableFps.Length - 2)
            {
                Settings.currentFps += 1;
                Sound.PlaySound(Sound.buttonSoundEffect);
                ChangeFPS();
            }
        }

        private void FullscreenButton_Click(object sender, EventArgs e)
        {
            if (Settings.fullscreen)
            {
                buttons[4].Texture = Art.emptyCase;
                Settings.fullscreen = false;
                Main._graphics.IsFullScreen = false;
                textResolution = $"{Settings.availableResolutionsStrings[Settings.currentResolution]}";
                Settings.currentWidthResolution = (int)Settings.availableResolutions[Settings.currentResolution].X;
                Settings.currentHeightResolution = (int)Settings.availableResolutions[Settings.currentResolution].Y;
            }
            else
            {
                buttons[4].Texture = Art.checkedCase;
                Settings.fullscreen = true;
                Main._graphics.IsFullScreen = true;
                textResolution = $"{Main._graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width}x{Main._graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height}";
                Settings.currentWidthResolution = Main._graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
                Settings.currentHeightResolution = Main._graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            }
            Sound.PlaySound(Sound.buttonSoundEffect);
            ChangeFullScreen();
        }

        private void NoDamageButton_Click(object sender, EventArgs e)
        {
            if (Settings.noDamage)
            {
                buttons[9].Texture = Art.emptyCase;
                Settings.noDamage = false;
            }
            else
            {
                buttons[9].Texture = Art.checkedCase;
                Settings.noDamage = true;
            }
            Sound.PlaySound(Sound.buttonSoundEffect);
        }

        private void LeftArrowMusicVolumeButton_Click(object sender, EventArgs e)
        {
            if (Settings.currentMusicVolume >= 1)
            {
                Settings.currentMusicVolume -= 1;
                ChangeMusicVolume();
            }
            Sound.PlaySound(Sound.buttonSoundEffect);
        }

        private void RightArrowMusicVolumeButton_Click(object sender, EventArgs e)
        {
            if (Settings.currentMusicVolume <= Settings.availableMusicVolumes.Length - 2)
            {
                Settings.currentMusicVolume += 1;
                ChangeMusicVolume();
            }
            Sound.PlaySound(Sound.buttonSoundEffect);
        }

        private void LeftArrowSoundEffectVolumeButton_Click(object sender, EventArgs e)
        {
            if (Settings.currentSoundEffectVolume >= 1)
            {
                Settings.currentSoundEffectVolume -= 1;
                ChangeSoundEffectVolume();
            }
            Sound.PlaySound(Sound.buttonSoundEffect);
        }

        private void RightArrowSoundEffectVolumeButton_Click(object sender, EventArgs e)
        {
            if (Settings.currentSoundEffectVolume <= Settings.availableSoundEffectVolumes.Length - 2)
            {
                Settings.currentSoundEffectVolume += 1;
                ChangeSoundEffectVolume();
            }
            Sound.PlaySound(Sound.buttonSoundEffect);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Sound.PlaySound(Sound.buttonSoundEffect);
            Game.LoadMainMenu();
        }

        private Vector2 PlaceToLeft(Vector2 textPosition, string text, Texture2D texture)
        {
            return new Vector2(textPosition.X - texture.Width - 8, textPosition.Y - texture.Height / 2 + Art.font.MeasureString(text).Y / 2);
        }

        private Vector2 PlaceToRight(Vector2 textPosition, string text, Texture2D texture)
        {
            return new Vector2(textPosition.X + Art.font.MeasureString(text).X - 2 + 8, textPosition.Y - texture.Height / 2 + Art.font.MeasureString(text).Y / 2);
        }
        private Vector2 PlaceToLeft(Vector2 textPosition, Texture2D button, Texture2D texture)
        {
            return new Vector2(textPosition.X - texture.Width - 8, textPosition.Y - texture.Height / 2 + button.Height / 2);
        }

        private Vector2 PlaceToRight(Vector2 textPosition, Texture2D button, Texture2D texture)
        {
            return new Vector2(textPosition.X + button.Width - 2 + 8, textPosition.Y - texture.Height / 2 + button.Height / 2);
        }

        private void ChangeResolution()
        {
            textResolution = $"{Settings.availableResolutionsStrings[Settings.currentResolution]}";
            Settings.currentWidthResolution = (int)Settings.availableResolutions[Settings.currentResolution].X;
            Settings.currentHeightResolution = (int)Settings.availableResolutions[Settings.currentResolution].Y;
            Main._graphics.PreferredBackBufferWidth = Settings.currentWidthResolution;
            Main._graphics.PreferredBackBufferHeight = Settings.currentHeightResolution;
            Main._graphics.ApplyChanges();
            textResolutionPosition = new Vector2(140 - (Art.font.MeasureString(textResolution).X / 2), 68 - (Art.font.MeasureString(textResolution).Y / 2));
            buttons[0].Position = PlaceToLeft(textResolutionPosition, textResolution, Art.leftArrow);
            buttons[1].Position = PlaceToRight(textResolutionPosition, textResolution, Art.rightArrow);
        }

        private void ChangeFPS()
        {
            textFps = $"{Settings.availableFps[Settings.currentFps]} FPS";
            Game.TargetElapsedTime = TimeSpan.FromSeconds(1f / Settings.availableFps[Settings.currentFps]);
            Main._graphics.ApplyChanges();
            textFpsPosition = new Vector2(140 - Art.font.MeasureString(textFps).X / 2, 135 - Art.font.MeasureString(textFps).Y / 2);
            buttons[2].Position = PlaceToLeft(textFpsPosition, textFps, Art.leftArrow);
            buttons[3].Position = PlaceToRight(textFpsPosition, textFps, Art.rightArrow);
        }

        private void ChangeFullScreen()
        {
            Main._graphics.PreferredBackBufferWidth = Settings.currentWidthResolution;
            Main._graphics.PreferredBackBufferHeight = Settings.currentHeightResolution;
            Main._graphics.ApplyChanges();
            textResolutionPosition = new Vector2(140 - (Art.font.MeasureString(textResolution).X / 2), 68 - (Art.font.MeasureString(textResolution).Y / 2));
            buttons[0].Position = PlaceToLeft(textResolutionPosition, textResolution, Art.leftArrow);
            buttons[1].Position = PlaceToRight(textResolutionPosition, textResolution, Art.rightArrow);
        }

        private void ChangeMusicVolume()
        {
            musicVolumeBar = Art.volumeBar[Settings.currentMusicVolume];
            Settings.soundMusicVolume = Settings.availableMusicVolumes[Settings.currentMusicVolume];
            Sound.ApplyVolumeEqualizeBackgroudMusic(Sound.currentSong);
        }

        private void ChangeSoundEffectVolume()
        {
            soundEffectVolumeBar = Art.volumeBar[Settings.currentSoundEffectVolume];
            Settings.soundSoundEffectVolume = Settings.availableSoundEffectVolumes[Settings.currentSoundEffectVolume];
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
            GraphicsDevice.SetRenderTarget(Main.renderTarget);
            Game.GraphicsDevice.Clear(Color.Black);

            Main._spriteBatch.Begin();
            Main._spriteBatch.Draw(Art.backgroundSettingsMenu, Vector2.Zero, Color.White * 0.6f);
            Main._spriteBatch.DrawString(Art.font, textResolution, textResolutionPosition, Color.White);
            Main._spriteBatch.DrawString(Art.font, textFullscreen, textFullscreenPosition, Color.White);
            Main._spriteBatch.DrawString(Art.font, textNoDamage, textNoDamagePosition, Color.White);
            Main._spriteBatch.DrawString(Art.font, textFps, textFpsPosition, Color.White);
            Main._spriteBatch.DrawString(Art.font, textMusicVolume, textMusicVolumePosition, Color.White);
            Main._spriteBatch.DrawString(Art.font, textSoundEffectVolume, textSoundEffectVolumePosition, Color.White);
            foreach (Button button in buttons)
            {
                button.Draw(gameTime, Main._spriteBatch);
            }
            Main._spriteBatch.Draw(musicVolumeBar, musicVolumeBarPosition, Color.White);
            Main._spriteBatch.Draw(soundEffectVolumeBar, soundEffectVolumeBarPosition, Color.White);
            Main._spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            Main._spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            Main._spriteBatch.Draw(Main.renderTarget, new Rectangle(0, 0, Settings.currentWidthResolution, Settings.currentHeightResolution), Color.White);
            Main._spriteBatch.End();
        }
    }
}
