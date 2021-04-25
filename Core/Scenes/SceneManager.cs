using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Scenes
{
    public class SceneManager
    {
        private readonly ContentManager _content;

        private readonly SpriteBatch _spriteBatch;
            
        private readonly List<IScene> _scenes;
        
        private IScene _currentScene;

        public SceneManager(ContentManager content, SpriteBatch spriteBatch)
        {
            _content = content;
            _spriteBatch = spriteBatch;
            _scenes = new List<IScene>();
        }

        public void Add(IScene scene)
        {
            _scenes.Add(scene);
            scene.OnCreate();
        }

        public void Remove(IScene scene)
        {
            scene.OnDestroy();
            _scenes.Remove(scene);
        }

        public void Switch(IScene scene)
        {
            _currentScene?.OnDeactivate();
            _currentScene = scene;
            _currentScene.OnActivate();
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
            _currentScene.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}
