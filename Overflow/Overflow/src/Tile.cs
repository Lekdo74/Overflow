using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overflow.src
{
    public class Tile
    {
        private Vector2 _position;
        private Texture2D _texture;
        private string _type;
        private bool _walkable;

        //PathFinding
        private int _gCost;
        private int _hCost;
        private int _mapX;
        private int _mapY;
        private Tile _parent;

        public Tile(Vector2 position, Texture2D texture, string type, int mapX, int mapY)
        {
            Position = position;
            Texture = texture;
            Type = type;
            if (Type == "Wall")
                Walkable = false;
            else
                Walkable = true;
            MapX = mapX;
            MapY = mapY;
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

        public bool Walkable
        {
            get { return _walkable; }
            set { _walkable = value; }
        }

        public Tile Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public int GCost
        {
            get { return _gCost; }
            set { _gCost = value; }
        }

        public int HCost
        {
            get { return _hCost; }
            set { _hCost = value; }
        }

        public int FCost
        {
            get { return GCost + HCost; }
        }

        public int MapX
        {
            get { return _mapX; }
            set { _mapX = value; }
        }

        public int MapY
        {
            get { return _mapY; }
            set { _mapY = value; }
        }

        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Rectangle, Color.White);
        }

        public void Draw(GameTime gameTime, SpriteBatch spritebatch, Color color)
        {
            spritebatch.Draw(Texture, Rectangle, color);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
