using Microsoft.Xna.Framework;

namespace Core.Scenes
{
    public interface IScene
    {
        public virtual void OnCreate() {}
        
        public virtual void OnDestroy() {}

        public virtual void OnActivate() {}

        public virtual void OnDeactivate() {}
        
        public virtual void OnInput() {}
        
        public virtual void Update(GameTime gameTime) {}
        
        public virtual void Draw(GameTime gameTime) {}
    }
}
