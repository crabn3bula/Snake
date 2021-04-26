using Core.Input;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core.Scenes
{
    public class GameOverScene : Scene
    {
        private const string GameOverText = "Game over. Press space to restart";
        
        [NotNull] private SpriteFont _font;
        
        public GameOverScene([NotNull] SceneManager sceneManager) : base(sceneManager)
        {
        }

        public override void OnActivate(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            base.OnActivate(contentManager, graphicsDevice);
            _font = contentManager.Load<SpriteFont>("Fonts/PressStart2P");
        }

        public override void OnInput()
        {
            base.OnInput();
            if (KeyboardController.IsKeyClicked(Keys.Space))
            {
                SceneManager.Switch(new GameplayScene(SceneManager));
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
            var textVector = _font.MeasureString(GameOverText);
            var screenVector = new Vector2(Settings.Width, Settings.Height);
            var textCenter = Vector2.Divide(textVector, 2);
            var screenCenter = Vector2.Divide(screenVector, 2);
            spriteBatch.DrawString(_font, GameOverText, Vector2.Subtract(screenCenter, textCenter), Color.White);
        }
    }
}
