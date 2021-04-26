using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Scenes
{
    public interface IScene
    {
        public void OnActivate([NotNull] ContentManager contentManager, [NotNull] GraphicsDevice graphicsDevice);

        public void OnDeactivate();

        public void OnInput();

        public void Update(GameTime gameTime);

        public void Draw([NotNull] SpriteBatch spriteBatch, GameTime gameTime);
    }
}
