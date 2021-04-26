using Core.Input;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core.Scenes
{
    public class MainMenuScene : Scene
    {
        private const string StartGameText = "Press space to start game";
        
        [NotNull] private SpriteFont _font;
        
        public MainMenuScene([NotNull] SceneManager sceneManager) : base(sceneManager)
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
            var textVector = _font.MeasureString(StartGameText);
            var screenVector = new Vector2(Settings.Width, Settings.Height);
            var textCenter = Vector2.Divide(textVector, 2);
            var screenCenter = Vector2.Divide(screenVector, 2);
            spriteBatch.DrawString(_font, StartGameText, Vector2.Subtract(screenCenter, textCenter), Color.White);
        }
    }
}
