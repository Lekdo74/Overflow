using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overflow.src
{
    public class GhostEffectDash
    {
        private Texture2D _texture;
        private Vector2 _position;
        private float _remainingTime;

        public GhostEffectDash(Vector2 position, float remainingTime)
        {
            Texture = Player.CurrentDashTexture;
            Position = position;
            RemainingTime = remainingTime;
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public float RemainingTime
        {
            get { return _remainingTime; }
            set { _remainingTime = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White * 0.5f);
        }
    }
}
