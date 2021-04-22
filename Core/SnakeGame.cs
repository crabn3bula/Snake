using System;
using Core.Entities;
using Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        // assets
        private Texture2D _pixelTexture;

        private SoundEffect _growSound;

        private SpriteFont _font;

        private readonly Rectangle _drawRect;

        // entities
        private Snake _snake;

        private Food _food;

        private Score _score;

        // timer
        private float _simulationWaitedSeconds;

        private bool _isGameOver = true;

        private const string GameOverText = "Game over. Press space to restart";

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

            // textures
            _pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            _pixelTexture.SetData(new[] {Color.White});

            // fonts
            _font = Content.Load<SpriteFont>("Fonts/PressStart2P");

            // sounds
            _growSound = Content.Load<SoundEffect>("Sounds/snake_grow");

            // create game entities
            _snake = new Snake(_pixelTexture, _growSound);
            _food = new Food(_pixelTexture);
            _score = new Score(_font);
            ResetGame();
        }

        private void ResetGame()
        {
            _snake.Spawn();
            _food.Spawn();
            _score.Reset();
            _isGameOver = false;
        }

        private void UpdateSimulation()
        {
            if (_isGameOver)
            {
                return;
            }

            _snake.Move();

            // collisions with food 
            if (_snake.Head == _food.Position)
            {
                _snake.Grow();
                _score.Increase();
                _food.Spawn();
            }

            // collisions with body
            if (_snake.Body.Contains(_snake.Head))
            {
                _isGameOver = true;
            }

            // collisions with map bounds
            if (_snake.Head.X >= ColsCount || _snake.Head.X < 0 || _snake.Head.Y >= RowsCount || _snake.Head.Y < 0)
            {
                _isGameOver = true;
            }

            _simulationWaitedSeconds = 0f;
        }

        private void HandleInput()
        {
            // TODO: Convert keyboard controller to GameComponent?
            KeyboardController.Update();
            if (KeyboardController.IsKeyClicked(Keys.Escape))
            {
                Exit();
            }

            if (KeyboardController.IsKeyClicked(Keys.Space))
            {
                ResetGame();
            }

            if (_isGameOver)
            {
                return;
            }

            if (KeyboardController.IsKeyClicked(Keys.Up))
            {
                _snake.ChangeDirection(SnakeDirections.Up);
                return;
            }

            if (KeyboardController.IsKeyClicked(Keys.Down))
            {
                _snake.ChangeDirection(SnakeDirections.Down);
                return;
            }

            if (KeyboardController.IsKeyClicked(Keys.Left))
            {
                _snake.ChangeDirection(SnakeDirections.Left);
                return;
            }

            if (KeyboardController.IsKeyClicked(Keys.Right))
            {
                _snake.ChangeDirection(SnakeDirections.Right);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            HandleInput();

            // wait for updating simulation
            var elapsedTimeSinceLastUpdate = (float) gameTime.ElapsedGameTime.TotalSeconds;
            var waitTime = _simulationWaitedSeconds + elapsedTimeSinceLastUpdate;
            if (waitTime < Settings.SimulationDelaySeconds)
            {
                _simulationWaitedSeconds += elapsedTimeSinceLastUpdate;
                return;
            }

            UpdateSimulation();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // draw to the buffer
            GraphicsDevice.SetRenderTarget(_drawBuffer);
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _snake.Draw(_spriteBatch);
            _food.Draw(_spriteBatch);
            _score.Draw(_spriteBatch);
            DrawGameOver();
            _spriteBatch.End();

            // draw buffer to screen
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Pink);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_drawBuffer, _drawRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawGameOver()
        {
            if (!_isGameOver)
            {
                return;
            }

            var textVector = _font.MeasureString(GameOverText);
            var screenVector = new Vector2(Settings.Width, Settings.Height);
            var textCenter = Vector2.Divide(textVector, 2);
            var screenCenter = Vector2.Divide(screenVector, 2);
            _spriteBatch.DrawString(_font, GameOverText, Vector2.Subtract(screenCenter, textCenter), Color.White);
        }
    }
}
