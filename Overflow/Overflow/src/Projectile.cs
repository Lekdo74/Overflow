using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public class Projectile
    {
        private Texture2D _texture;

        private Vector2 _position;
        private Vector2 _direction;
        private float _rotation;
        private int _speed;
        private Vector2 _origin;

        private Room _room;

        private bool _isExpired = false;

        public Projectile(Texture2D texture, Vector2 position, Vector2 direction, int speed, Room room)
        {
            Texture = texture;
            Position = position;
            Direction = direction;
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
            Speed = speed;
            Room = room;
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
        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }
        public Room Room
        {
            get { return _room; }
            set { _room = value; }
        }
        public bool IsExpired
        {
            get { return _isExpired; }
            set { _isExpired = value; }
        }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += Direction * deltaTime * Speed;
            if (!Room.InsideRoom(Position) || Room.GetTile(Position).Type == "Wall")
            {
                _isExpired = true;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Rectangle, null, Color.White, Rotation, Origin, SpriteEffects.None, 0);
        }
    }
}
