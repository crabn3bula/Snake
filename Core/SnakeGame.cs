using System;
using Core.Input;
using Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core
{
    public class SnakeGame : Game
    {
        public static SnakeGame Instance;

        public static int ColsCount => Settings.Width / Settings.CellSize;

        public static int RowsCount => Settings.Height / Settings.CellSize;

        public readonly Random Random;

        private readonly GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        private RenderTarget2D _drawBuffer;

        private SceneManager _sceneManager;
        
        private readonly Rectangle _drawRect;

        public SnakeGame()
        {
            Instance = this;
            Random = new Random();
            _drawRect = new Rectangle(0, 0, Settings.Width * Settings.Scale, Settings.Height * Settings.Scale);
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Settings.Width * Settings.Scale;
            _graphics.PreferredBackBufferHeight = Settings.Height * Settings.Scale;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _drawBuffer = new RenderTarget2D(GraphicsDevice, Settings.Width, Settings.Height);
            _sceneManager = new SceneManager(Content, GraphicsDevice, _spriteBatch);
            _sceneManager.Switch(new MainMenuScene(_sceneManager));
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardController.Update();
            _sceneManager.HandleInput();
            _sceneManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // draw to the buffer
            GraphicsDevice.SetRenderTarget(_drawBuffer);
            GraphicsDevice.Clear(Color.Black);

            _sceneManager.Draw(gameTime);

            // draw buffer to screen
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Pink);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_drawBuffer, _drawRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
