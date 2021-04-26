using Core.Entities;
using Core.Input;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core.Scenes
{
    public class GameplayScene : Scene
    {
        private static int ColsCount => Settings.Width / Settings.CellSize;

        private static int RowsCount => Settings.Height / Settings.CellSize;
        
        [NotNull] private Texture2D _pixelTexture;

        [NotNull] private SoundEffect _growSound;

        [NotNull] private SpriteFont _font;
        
        [NotNull] private Snake _snake;

        [NotNull] private Food _food;

        [NotNull] private Score _score;
        
        private float _simulationWaitedSeconds;
        
        public GameplayScene([NotNull] SceneManager sceneManager) : base(sceneManager)
        {
        }

        public override void OnActivate(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            base.OnActivate(contentManager, graphicsDevice);
            
            // textures
            _pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            _pixelTexture.SetData(new[] {Color.White});

            // fonts
            _font = contentManager.Load<SpriteFont>("Fonts/PressStart2P");

            // sounds
            _growSound = contentManager.Load<SoundEffect>("Sounds/snake_grow");

            // create game entities
            _snake = new Snake(_pixelTexture, _growSound);
            _food = new Food(_pixelTexture);
            _score = new Score(_font);

            // reset entities
            ResetState();
        }

        private void ResetState()
        {
            _snake.Spawn();
            _food.Spawn(_snake.OccupiedCells());
            _score.Reset();
        }

        private void UpdateSimulation()
        {
            _snake.Move();

            // collisions with food 
            if (_snake.Head == _food.Position)
            {
                _snake.Grow();
                _score.Increase();
                _food.Spawn(_snake.OccupiedCells());
            }

            // collisions with body
            if (_snake.Body.Contains(_snake.Head))
            {
                MoveToGameOver();
            }

            // collisions with map bounds
            if (_snake.Head.X >= ColsCount || _snake.Head.X < 0 || _snake.Head.Y >= RowsCount || _snake.Head.Y < 0)
            {
                MoveToGameOver();
            }

            _simulationWaitedSeconds = 0f;
        }

        private void MoveToGameOver()
        {
            SceneManager.Switch(new GameOverScene(SceneManager));
        }

        public override void OnInput()
        {
            base.OnInput();
                
            if (KeyboardController.IsKeyClicked(Keys.Space))
            {
                ResetState();
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // wait for updating simulation
            
            var elapsedTimeSinceLastUpdate = (float) gameTime.ElapsedGameTime.TotalSeconds;
            var waitTime = _simulationWaitedSeconds + elapsedTimeSinceLastUpdate;
            if (waitTime < Settings.SimulationDelaySeconds)
            {
                _simulationWaitedSeconds += elapsedTimeSinceLastUpdate;
                return;
            }

            UpdateSimulation();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
            _snake.Draw(spriteBatch);
            _food.Draw(spriteBatch);
            _score.Draw(spriteBatch);
        }
    }
}
