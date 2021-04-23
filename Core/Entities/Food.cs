using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
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
        }

        public void Spawn(IEnumerable<int> occupiedCells)
        {
            var availableCells = Enumerable
                .Range(0, (SnakeGame.RowsCount * SnakeGame.ColsCount) - 1)
                .Except(occupiedCells)
                .ToArray();

            var spawnCell = availableCells[Game.Random.Next(0, availableCells.Length - 1)];
            var xPos = spawnCell % SnakeGame.ColsCount;
            var yPos = spawnCell / SnakeGame.ColsCount;
            Position = new Vector2(xPos, yPos);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position * Settings.CellSize, _textureRect, Color.Green);
        }
    }
}
