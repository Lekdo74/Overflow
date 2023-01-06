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
        private Room _room;

        private Texture2D _texture;
        private Color _color;

        private Vector2 _position;
        private Vector2 _direction;
        private int _speed;
        float deltaTime;

        private bool _isExpired;

        private List<IEnumerator<int>> behaviours = new List<IEnumerator<int>>();

        public Enemy(Texture2D texture, Vector2 position, Room room)
        {
            Texture = texture;
            Color = Color.White;
            Position = position;
            Room = room;
            Direction = Vector2.Zero;
            Speed = 30;
            IsExpired = false;
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public Room Room
        {
            get { return _room; }
            set { _room = value; }
        }

        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public bool IsExpired
        {
            get { return _isExpired; }
            set { _isExpired = value; }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public void IsHurt()
        {
            IsExpired = true;
        }

        IEnumerable<int> FollowPlayer()
        {
            while (true)
            {
                Direction += (Player.Position - Position);
                Direction = Vector2.Normalize(Direction);
                Position += Direction * _speed * deltaTime;
                yield return 0;
            }
        }

        private void AddBehaviour(IEnumerable<int> behaviour)
        {
            behaviours.Add(behaviour.GetEnumerator());
        }

        private void ApplyBehaviours()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                if (!behaviours[i].MoveNext())
                    behaviours.RemoveAt(i--);
            }
        }

        public void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ApplyBehaviours();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color);
        }

        public static Enemy CreateSeeker(Vector2 position, Room room)
        {
            Enemy enemy = new Enemy(Art.enemy, position, room);
            enemy.AddBehaviour(enemy.FollowPlayer());

            return enemy;
        }
    }
}
