using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Core.Scenes
{
    public abstract class Scene : IScene
    {
        private readonly SceneManager _sceneManager;

        private readonly ContentManager _contentManager;

        protected Scene(SceneManager sceneManager, ContentManager contentManager)
        {
            _sceneManager = sceneManager;
            _contentManager = contentManager;
        }

        public virtual void OnCreate() {}
        
        public virtual void OnDestroy() {}

        public virtual void OnActivate() {}

        public virtual void OnDeactivate() {}
        
        public virtual void OnInput() {}
        
        public virtual void Update(GameTime gameTime) {}
        
        public virtual void Draw(GameTime gameTime) {}
    }
}
