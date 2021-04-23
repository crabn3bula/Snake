using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Entities
{
    public enum SnakeDirections
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Snake
    {
        public Vector2 Head { get; private set; }

        public List<Vector2> Body { get; private set; }

        private Vector2 _direction;

        private readonly Texture2D _texture;

        private readonly Rectangle _textureRect;

        private readonly SoundEffect _growSound;

        public Snake(Texture2D texture, SoundEffect growSound)
        {
            _texture = texture;
            _textureRect = new Rectangle(new Point(0), new Point(Settings.CellSize));
            _growSound = growSound;
            Spawn();
        }

        public void Spawn()
        {
            Head = new Vector2(10, 5);
            Body = new List<Vector2>(new[]
            {
                new Vector2(9, 5),
                new Vector2(8, 5),
            });
            _direction = new Vector2(1, 0);
        }

        public void Move()
        {
            Body.Insert(0, Head);
            Body.RemoveAt(Body.Count - 1);
            Head = Vector2.Add(Head, _direction);
        }

        public void Grow()
        {
            var tail = Body.Last();
            Body.Add(tail);
            _growSound.Play();
        }

        public void ChangeDirection(SnakeDirections direction)
        {
            var newDirection = direction switch
            {
                SnakeDirections.Up => new Vector2(0, -1),
                SnakeDirections.Down => new Vector2(0, 1),
                SnakeDirections.Left => new Vector2(-1, 0),
                SnakeDirections.Right => new Vector2(1, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Unknown snake direction")
            };

            /* if dot product of vectors is -1 then vectors in exactly opposite directions
               we cant compare current direction with new direction, because input update and simulation
               runs at different rates, so user can change direction multiply times before next simulation step */
            var currentHeadDirection = Vector2.Subtract(Head, Body.First());
            var isOppositeDirection = (int) Vector2.Dot(currentHeadDirection, newDirection) == -1;
            if (isOppositeDirection)
            {
                return;
            }

            _direction = newDirection;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // draw head
            spriteBatch.Draw(_texture, Head * Settings.CellSize, _textureRect, Color.Red);
            // draw body
            Body.ForEach(segment => spriteBatch.Draw(_texture, segment * Settings.CellSize, _textureRect, Color.Gray));
        }

        public IEnumerable<int> OccupiedCells()
        {
            var coordinates = new List<Vector2> { Head };
            coordinates.AddRange(Body);
            return coordinates
                .Select(segment => (int) (segment.Y * (SnakeGame.RowsCount - 1) + segment.X))
                .OrderBy(x => x);
        }
    }
}
