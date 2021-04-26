using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Scenes
{
    public class SceneManager
    {
        [NotNull] private readonly ContentManager _contentManager;

        [NotNull] private readonly GraphicsDevice _graphicsDevice;

        [NotNull] private readonly SpriteBatch _spriteBatch;

        private IScene _currentScene;

        public SceneManager([NotNull] ContentManager contentManager, [NotNull] GraphicsDevice graphicsDevice, [NotNull] SpriteBatch spriteBatch)
        {
            _contentManager = contentManager;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;
        }

        public void Switch(IScene scene)
        {
            _currentScene?.OnDeactivate();
            _currentScene = scene;
            _currentScene.OnActivate(_contentManager, _graphicsDevice);
        }

        public void HandleInput()
        {
            _currentScene?.OnInput();
        }

        public void Update(GameTime gameTime)
        {
            _currentScene?.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            if (_currentScene == null)
            {
                return;
            }
            
            _spriteBatch.Begin();
            _currentScene.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
        }
    }
}
