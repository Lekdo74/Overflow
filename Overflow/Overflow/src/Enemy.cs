using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
        private Vector2 _initPositionEnemy;
        private Vector2 _direction;
        private Vector2 _velocity;
        private Vector2[] _path;
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

        public Vector2 CenteredPosition
        {
            get { return Position + new Vector2(Texture.Width / 2, Texture.Height / 2); }
        }

        public Vector2 InitPositionEnemy
        {
            get { return _initPositionEnemy; }
            set { _initPositionEnemy = value; }
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

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Vector2[] Path
        {
            get { return _path; }
            set { _path = value; }
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
                Path = PathFinding.FindPath((CenteredPosition - Room.Position) / 20, (Player.CenteredPosition - Room.Position) / 20, Room);
                if (Path.Length > 0)
                {
                    Direction = (Path[0] - Position);
                }
                else
                {
                    Direction = (Player.CenteredPosition - CenteredPosition);
                }
                Velocity = Vector2.Normalize(Direction) * _speed * deltaTime;

                InitPositionEnemy = Position;
                _position.X += Velocity.X;
                if (CheckCollision(Room.Obstacles) || !Room.InsideRoom(Position))
                {
                    Velocity = new Vector2(0, Math.Sign(Velocity.Y) * Speed * deltaTime);
                }
                _position.X = InitPositionEnemy.X;
                
                _position.Y += Velocity.Y;
                if (CheckCollision(Room.Obstacles) || !Room.InsideRoom(Position))
                {
                    Velocity = new Vector2(Math.Sign(Velocity.X) * Speed * deltaTime, 0);
                }
                _position.Y = InitPositionEnemy.Y;

                _position += Velocity;
                
                yield return 0;
            }
        }
        private bool CheckCollision(List<Rectangle> obstacles)
        {
            foreach (Rectangle obstacle in obstacles)
            {
                if (Rectangle.Intersects(obstacle))
                {
                    return true;
                }
            }
            return false;
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
            /*foreach (Vector2 position in PathFinding.FindPath((Position + new Vector2(Texture.Width / 2, Texture.Height / 2) - Room.Position) / 20, (Player.Position + new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2) - Room.Position) / 20, Room))
            {
                spriteBatch.Draw(Art.tileset1[8], position, Color.Red);
            }*/
        }

        public static Enemy CreateSeeker(Vector2 position, Room room)
        {
            Enemy enemy = new Enemy(Art.enemy, position, room);
            enemy.AddBehaviour(enemy.FollowPlayer());

            return enemy;
        }
    }
}
