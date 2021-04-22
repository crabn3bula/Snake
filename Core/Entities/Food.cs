using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Entities
{
    public class Food
    {
        private static SnakeGame Game => SnakeGame.Instance;

        public Vector2 Position { get; private set; }

        private readonly Texture2D _texture;

        private readonly Rectangle _textureRect;

        public Food(Texture2D texture)
        {
            _texture = texture;
            _textureRect = new Rectangle(new Point(0), new Point(Settings.CellSize));
            Spawn();
        }

        public void Spawn()
        {
            var xPos = Game.Random.Next(0, SnakeGame.ColsCount - 1);
            var yPos = Game.Random.Next(0, SnakeGame.RowsCount - 1);
            Position = new Vector2(xPos, yPos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position * Settings.CellSize, _textureRect, Color.Green);
        }
    }
}
