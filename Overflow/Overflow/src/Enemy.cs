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

        private string _type;

        private Texture2D _texture;
        private float _rotation;
        private float _defaultRotation;
        private Color _color;

        private Vector2 _position;
        private Vector2 _origin = new Vector2 (7, 7);

        private Vector2 _initPositionEnemy;
        private Vector2 _direction;
        private Vector2 _velocity;
        private int _speed;
        float deltaTime;

        private Tile _previousTile;
        private Tile _currentTile;

        private Vector2[] _path;
        private bool _calculPath = true;

        private bool _isExpired;

        private List<IEnumerator<int>> behaviours = new List<IEnumerator<int>>();

        public Enemy(Room room, string type, Texture2D texture, Vector2 position)
        {
            _type = type;
            Texture = texture;
            Color = Color.White;
            Position = position;
            Room = room;
            Direction = Vector2.Zero;
            Speed = 35;
            IsExpired = false;
        }
        public Enemy(Room room, string type, Texture2D texture, Vector2 position, float defaultRotation)
        {
            _type = type;
            Texture = texture;
            _defaultRotation = defaultRotation;
            Color = Color.White;
            Position = position;
            Room = room;
            _currentTile = Room.GetTile(position);
            Direction = Vector2.Zero;
            Speed = 35;
            IsExpired = false;
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public float Rotation
        {
            get { return _rotation + _defaultRotation; }
            set { _rotation = value; }
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

        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
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

        public bool CalculPath
        {
            get { return _calculPath; }
            set { _calculPath = value; }
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

        public float DistanceToPlayer
        {
            get { return Vector2.Distance(Position, Player.Position); }
        }

        public bool ViewOnPlayer
        {
            get { return Room.LineOfView(Room, CenteredPosition, Player.CenteredPosition); }
        }

        public void IsHurt()
        {
            IsExpired = true;
        }

        IEnumerable<int> FollowPlayer()
        {
            while (true)
            {
                if (Path.Length > 0)
                {
                    Direction = (Path[0] - Position);
                }
                else
                {
                    Direction = (Player.CenteredPosition - CenteredPosition);
                }
                Velocity = Vector2.Normalize(Direction) * _speed * deltaTime;

                float distanceToPlayer = DistanceToPlayer;
                InitPositionEnemy = Position;
                _position.X += Velocity.X;
                if (CheckCollision(Room.Obstacles) || !Room.InsideRoom(Position))
                {
                    Velocity = new Vector2(0, Math.Sign(Velocity.Y) * Speed * deltaTime);
                }
                else
                {
                    foreach(Enemy enemy in Room.Enemies)
                    {
                        float distanceToEnemy = Vector2.Distance(Position, enemy.Position);
                        if (enemy._type == "Seeker" && enemy != this && distanceToEnemy < 10 && distanceToPlayer > enemy.DistanceToPlayer)
                        {
                            Velocity = new Vector2(0, Math.Sign(Velocity.Y) * Speed * deltaTime);
                        }
                    }
                }
                _position.X = InitPositionEnemy.X;
                
                _position.Y += Velocity.Y;
                if (CheckCollision(Room.Obstacles) || !Room.InsideRoom(Position))
                {
                    Velocity = new Vector2(Math.Sign(Velocity.X) * Speed * deltaTime, 0);
                }
                else
                {
                    foreach (Enemy enemy in Room.Enemies)
                    {
                        float distanceToEnemy = Vector2.Distance(Position, enemy.Position);
                        if (enemy._type == "Seeker" && enemy != this && distanceToEnemy < 10 && distanceToPlayer > enemy.DistanceToPlayer)
                        {
                            Velocity = new Vector2(Math.Sign(Velocity.X) * Speed * deltaTime, 0);
                        }
                    }
                }
                _position.Y = InitPositionEnemy.Y;

                Rotation = (float)(Math.Atan2(Direction.Y, Direction.X));
                _position += Velocity;

                if(_previousTile != _currentTile)
                {
                    _calculPath = true;
                }

                yield return 0;
            }
        }

        IEnumerable<int> FollowPlayerThenShoot()
        {
            while (true)
            {
                if (DistanceToPlayer > 100 || !ViewOnPlayer)
                {
                    if (Path.Length > 0)
                    {
                        Direction = (Path[0] - Position);
                    }
                    else
                    {
                        Direction = (Player.CenteredPosition - CenteredPosition);
                    }
                    Velocity = Vector2.Normalize(Direction) * _speed * deltaTime;

                    float distanceToPlayer = DistanceToPlayer;
                    InitPositionEnemy = Position;
                    _position.X += Velocity.X;
                    if (CheckCollision(Room.Obstacles) || !Room.InsideRoom(Position))
                    {
                        Velocity = new Vector2(0, Math.Sign(Velocity.Y) * Speed * deltaTime);
                    }
                    else
                    {
                        foreach (Enemy enemy in Room.Enemies)
                        {
                            float distanceToEnemy = Vector2.Distance(Position, enemy.Position);
                            if (enemy != this && distanceToEnemy < 10 && distanceToPlayer > enemy.DistanceToPlayer)
                            {
                                Velocity = new Vector2(0, Math.Sign(Velocity.Y) * Speed * deltaTime);
                            }
                        }
                    }
                    _position.X = InitPositionEnemy.X;

                    _position.Y += Velocity.Y;
                    if (CheckCollision(Room.Obstacles) || !Room.InsideRoom(Position))
                    {
                        Velocity = new Vector2(Math.Sign(Velocity.X) * Speed * deltaTime, 0);
                    }
                    else
                    {
                        foreach (Enemy enemy in Room.Enemies)
                        {
                            float distanceToEnemy = Vector2.Distance(Position, enemy.Position);
                            if (enemy != this && distanceToEnemy < 10 && distanceToPlayer > enemy.DistanceToPlayer)
                            {
                                Velocity = new Vector2(Math.Sign(Velocity.X) * Speed * deltaTime, 0);
                            }
                        }
                    }
                    _position.Y = InitPositionEnemy.Y;

                    _position += Velocity;

                    if (_previousTile != _currentTile)
                    {
                        CalculPath = true;
                    }
                }

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

        public void Shoot()
        {
            Room.Projectiles.Add(new Projectile(Art.checkedCase, Position, Vector2.Normalize(Player.Position - Position), Room));
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
            _previousTile = Room.GetTile(Position);
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_calculPath)
            {
                Shoot();
                Room.LineOfView(Room, Position, Player.Position);
                Path = PathFinding.FindPath((CenteredPosition - Room.Position) / 20, (Player.CenteredPosition - Room.Position) / 20, Room);
                _calculPath = false;
            }
            ApplyBehaviours();
            _currentTile = _previousTile;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)(Position.X + Origin.X), (int)(Position.Y + Origin.Y), Texture.Width, Texture.Height), null, Color, Rotation, Origin, SpriteEffects.None, 0f);
            /*foreach (Vector2 position in PathFinding.FindPath((Position + new Vector2(Texture.Width / 2, Texture.Height / 2) - Room.Position) / 20, (Player.Position + new Vector2(Player.Texture.Width / 2, Player.Texture.Height / 2) - Room.Position) / 20, Room))
            {
                spriteBatch.Draw(Art.tilesetLevel1.TopDoor, position, Color.Red);
            }*/
        }

        public static Enemy CreateSeeker(Texture2D texture, Vector2 position, Room room)
        {
            Enemy enemy;
            switch (texture)
            {
                case Texture2D value when value == Art.enemysetLevel1.Seekers[0]:
                    enemy = new Enemy(room, "Seeker", texture, position, (float)(Math.PI * -0.25f));
                    enemy.AddBehaviour(enemy.FollowPlayer());
                    return enemy;
            }
            enemy = new Enemy(room, "Seeker", texture, position);
            enemy.AddBehaviour(enemy.FollowPlayer());

            return enemy;
        }

        public static Enemy CreateArcher(Texture2D texture, Vector2 position, Room room)
        {
            Enemy enemy;
            enemy = new Enemy(room, "Archer", texture, position);
            enemy.AddBehaviour(enemy.FollowPlayerThenShoot());

            return enemy;
        }
    }
}
