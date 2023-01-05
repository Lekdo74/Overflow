using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public class Enemy
    {
        private Texture2D _texture;
        private Color _colors;

        private Vector2 _position;
        private Vector2 _direction;

        public Enemy(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _colors = Color.White;
            _position = position;
            _direction = Vector2.Zero;
        }

        public Texture2D Texture
        {
            get { return _texture; }
        }

        public Color Color
        {
            get { return _colors; }
            set { _colors = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public void Update()
        {
            Position += Direction;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
