using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Overflow.Scenes;
using Overflow.src;
using System;

namespace Overflow
{
    public class Main : Game
    {
        private readonly ScreenManager _screenManager;
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static RenderTarget2D _renderTarget;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1f / Settings.availableFps[Settings.currentFps]);
            _graphics.SynchronizeWithVerticalRetrace = false;

            _graphics.PreferredBackBufferWidth = Settings.launchWidthResolution;
            _graphics.PreferredBackBufferHeight = Settings.launchHeightResolution;
            _graphics.ApplyChanges();
            _renderTarget = new RenderTarget2D(GraphicsDevice, Settings.nativeWidthResolution, Settings.nativeHeightResolution);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Art.Load(Content);
            Sound.Load(Content);

            LoadMainMenu();
            base.Initialize();
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                LoadMainMenu();

            base.Update(gameTime);
        }

        public void LoadMainMenu()
        {
            _screenManager.LoadScreen(new MainMenu(this), new FadeTransition(GraphicsDevice, Color.Black, 0.85f));
        }

        public void LoadSettingsMenu()
        {
            _screenManager.LoadScreen(new SettingsMenu(this), new FadeTransition(GraphicsDevice, Color.Black, 0.85f));
        }

        public void LoadTutorial()
        {
            _screenManager.LoadScreen(new Tutorial(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadLevel1()
        {
            _screenManager.LoadScreen(new Level1(this), new FadeTransition(GraphicsDevice, Color.Black, 1f));
        }

        public void LoadEndScreen()
        {
            _screenManager.LoadScreen(new EndScreen(this), new FadeTransition(GraphicsDevice, Color.Black, 1f));
        }

        public void LoadRoomTest()
        {
            _screenManager.LoadScreen(new RoomTest(this), new FadeTransition(GraphicsDevice, Color.Black, 1f));
        }
    }
}