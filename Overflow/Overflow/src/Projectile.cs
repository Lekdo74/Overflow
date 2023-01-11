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
        private float _speed;
        private Vector2 _origin;

        private Room _room;

        private bool _isExpired = false;

        private float _remainingTime;

        public Projectile(Texture2D texture, Vector2 position, Vector2 direction, int speed, Room room)
        {
            Texture = texture;
            Position = position;
            Direction = direction;
            Speed = speed;
            Room = room;
        }

        public Projectile(Texture2D texture, Vector2 position, Vector2 direction, int speed, Room room, float remainingTime)
        {
            Texture = texture;
            Position = position;
            Direction = direction;
            Speed = speed;
            Room = room;
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
        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        
        public float Speed
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

        public float RemainingTime
        {
            get { return _remainingTime; }
            set { _remainingTime = value; }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += Direction * deltaTime * Speed;
            if(Room.RoomType != 3)
            {
                if (!Room.InsideRoom(Position) || (Room.GetTile(Position) != null && Room.GetTile(Position).Type == "Wall"))
                {
                    _isExpired = true;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Rectangle, null, Color.White, 0, Origin, SpriteEffects.None, 0);
        }
    }
}
