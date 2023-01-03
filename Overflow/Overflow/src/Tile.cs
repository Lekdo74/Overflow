using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overflow.src
{
    public class Tile : Component
    {
        private Vector2 _position;
        private Texture2D _texture;
        private string _type;
        public Tile(Vector2 position, Texture2D texture, string type)
        {
            Position = position;
            Texture = texture;
            Type = type;
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
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

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
