﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public KeyboardState _keyboardState;
        public RenderTarget2D renderTarget;

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
            renderTarget = new RenderTarget2D(GraphicsDevice, Settings.nativeWidthResolution, Settings.nativeHeightResolution);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadMainMenu();
            base.Initialize();
        }

        //float time = 0;
        //int nbUpdate = 0;
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();
            /*
            if (time >= 1)
            {
                Console.WriteLine($"FPS : {nbUpdate}");
                time = 0;
                nbUpdate = 0;
            }
            time += gameTime.GetElapsedSeconds();
            nbUpdate++;
            */
            base.Update(gameTime);
        }

        public void LoadMainMenu()
        {
            _screenManager.LoadScreen(new MainMenu(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadSettingsMenu()
        {
            _screenManager.LoadScreen(new SettingsMenu(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadTutorial()
        {
            _screenManager.LoadScreen(new Tutorial(this), new FadeTransition(GraphicsDevice, Color.Black));
        }

        public void LoadLevel1()
        {
            _screenManager.LoadScreen(new Level1(this), new FadeTransition(GraphicsDevice, Color.Black));
        }
    }
}