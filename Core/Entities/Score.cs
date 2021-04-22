using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Entities
{
    public class Score
    {
        private int _score;

        private int _highScore;

        private readonly SpriteFont _font;

        public Score(SpriteFont font)
        {
            _font = font;
            Reset();
        }

        public void Reset()
        {
            _score = 0;
        }

        public void Increase()
        {
            _score++;
            _highScore = Math.Max(_score, _highScore);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, $"Score: {_score}  Highscore: {_highScore}", Vector2.Zero, Color.White);
        }
    }
}
