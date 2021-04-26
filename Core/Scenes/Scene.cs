using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Scenes
{
    public abstract class Scene : IScene
    {
        [NotNull] protected SceneManager SceneManager { get; }

        protected Scene([NotNull] SceneManager sceneManager)
        {
            SceneManager = sceneManager;
        }

        public virtual void OnActivate(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
        }

        public virtual void OnDeactivate()
        {
        }

        public virtual void OnInput()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }
    }
}
