using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overflow.src
{
    public class Player : Component
    {
        private Vector2 _position;
        private Texture2D _texture;
        private int _speed;

        public Player(Vector2 position, Texture2D texture, int speed)
        {
            _position = position;
            _texture = texture;
            _speed = speed;
        }

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value;
            }
        }

        public int Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public Rectangle _rectangle
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, _rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
