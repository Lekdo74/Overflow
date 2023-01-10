﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
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
        private static Random random = new Random();

        private Room _room;

        private string _type;

        private Texture2D _texture;
        private float _rotation;
        private float _defaultRotation;
        private Color _color = Color.White;

        private Vector2 _position;
        private Vector2 _origin = new Vector2 (7, 7);

        private Vector2 _initPositionEnemy;
        private Vector2 _direction;
        private Vector2 _velocity;

        private int _health;
        private int _speed;
        private int _attackNumber;
        float _deltaTime;

        private Vector2 _knockbackDirection;
        private float _knockbackDuration = 0.14f;
        private float _knockbackTimeRemaining;
        private int _knockbackSpeed = 170;

        private float _defaultTimeBetweenShots;
        private float _currentTimeBetweenShots;
        private float _timeSinceLastShot;

        private Tile _previousTile;
        private Tile _currentTile;

        private Vector2[] _path;
        private bool _calculPath = true;

        private bool _isExpired;

        private List<IEnumerator<int>> behaviours = new List<IEnumerator<int>>();

        public Enemy(Room room, string type, Vector2 position, int health)
        {
            _type = type;
            Position = position;
            Health = health;
            Room = room;
            Direction = Vector2.Zero;
            IsExpired = false;
        }
        public Enemy(Room room, string type, Texture2D texture, Vector2 position, int health)
        {
            _type = type;
            Texture = texture;
            Position = position;
            Health = health;
            Room = room;
            Direction = Vector2.Zero;
            IsExpired = false;
        }
        public Enemy(Room room, string type, Texture2D texture, Vector2 position, float defaultRotation, int health)
        {
            _type = type;
            Texture = texture;
            _defaultRotation = defaultRotation;
            Position = position;
            Health = health;
            Room = room;
            _currentTile = Room.GetTile(position);
            Direction = Vector2.Zero;
            IsExpired = false;
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
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
            get
            {
                if (!(Type == "Boss"))
                    return Position + new Vector2(Texture.Width / 2, Texture.Height / 2);
                return Position + new Vector2(Boss.OffSetX + Boss.Width / 2,Boss.OffSetY + Boss.Height / 2);
            }
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

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 KnockbackDirection
        {
            get { return _knockbackDirection; }
            set { _knockbackDirection = value; }
        }
        public float KnockbackDuration
        {
            get { return _knockbackDuration; }
            set { _knockbackDuration = value; }
        }
        public float KnockbackTimeRemaining
        {
            get { return _knockbackTimeRemaining; }
            set { _knockbackTimeRemaining = value; }
        }
        public int KnockbackSpeed
        {
            get { return _knockbackSpeed; }
            set { _knockbackSpeed = value; }
        }

        public  int AttackNumber
        {
            get { return _attackNumber; }
            set { _attackNumber = value; }
        }

        public float DefaultTimeBetweenShots
        {
            get { return _defaultTimeBetweenShots; }
            set { _defaultTimeBetweenShots = value; }
        }

        public float CurrentTimeBetweenShots
        {
            get { return _currentTimeBetweenShots; }
            set { _currentTimeBetweenShots = value; }
        }

        public float TimeSinceLastShot
        {
            get { return _timeSinceLastShot; }
            set { _timeSinceLastShot = value; }
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
                if(!(Type == "Boss"))
                    return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
                return Boss.Rectangle;
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
                Velocity = Vector2.Normalize(Direction) * _speed * _deltaTime;

                float distanceToPlayer = DistanceToPlayer;
                InitPositionEnemy = Position;
                _position.X += Velocity.X;
                if (CheckCollision() || !Room.InsideRoom(Position))
                {
                    Velocity = new Vector2(0, Math.Sign(Velocity.Y) * Speed * _deltaTime);
                }
                else
                {
                    foreach(Enemy enemy in Room.Enemies)
                    {
                        float distanceToEnemy = Vector2.Distance(Position, enemy.Position);
                        if (enemy._type == "Seeker" && enemy != this && distanceToEnemy < 10 && distanceToPlayer > enemy.DistanceToPlayer)
                        {
                            Velocity = new Vector2(0, Math.Sign(Velocity.Y) * Speed * _deltaTime);
                        }
                    }
                }
                _position.X = InitPositionEnemy.X;
                
                _position.Y += Velocity.Y;
                if (CheckCollision() || !Room.InsideRoom(Position))
                {
                    Velocity = new Vector2(Math.Sign(Velocity.X) * Speed * _deltaTime, 0);
                }
                else
                {
                    foreach (Enemy enemy in Room.Enemies)
                    {
                        float distanceToEnemy = Vector2.Distance(Position, enemy.Position);
                        if (enemy._type == "Seeker" && enemy != this && distanceToEnemy < 10 && distanceToPlayer > enemy.DistanceToPlayer)
                        {
                            Velocity = new Vector2(Math.Sign(Velocity.X) * Speed * _deltaTime, 0);
                        }
                    }
                }
                _position.Y = InitPositionEnemy.Y;

                Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
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
                    Velocity = Vector2.Normalize(Direction) * _speed * _deltaTime;

                    float distanceToPlayer = DistanceToPlayer;
                    InitPositionEnemy = Position;
                    _position.X += Velocity.X;
                    if (CheckCollision() || !Room.InsideRoom(Position))
                    {
                        Velocity = new Vector2(0, Math.Sign(Velocity.Y) * Speed * _deltaTime);
                    }
                    else
                    {
                        foreach (Enemy enemy in Room.Enemies)
                        {
                            float distanceToEnemy = Vector2.Distance(Position, enemy.Position);
                            if (enemy != this && distanceToEnemy < 10 && distanceToPlayer > enemy.DistanceToPlayer)
                            {
                                Velocity = new Vector2(0, Math.Sign(Velocity.Y) * Speed * _deltaTime);
                            }
                        }
                    }
                    _position.X = InitPositionEnemy.X;

                    _position.Y += Velocity.Y;
                    if (CheckCollision() || !Room.InsideRoom(Position))
                    {
                        Velocity = new Vector2(Math.Sign(Velocity.X) * Speed * _deltaTime, 0);
                    }
                    else
                    {
                        foreach (Enemy enemy in Room.Enemies)
                        {
                            float distanceToEnemy = Vector2.Distance(Position, enemy.Position);
                            if (enemy != this && distanceToEnemy < 10 && distanceToPlayer > enemy.DistanceToPlayer)
                            {
                                Velocity = new Vector2(Math.Sign(Velocity.X) * Speed * _deltaTime, 0);
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
                else if(TimeSinceLastShot > CurrentTimeBetweenShots)
                {
                    CurrentTimeBetweenShots = random.Next((int)(DefaultTimeBetweenShots * 0.8 * 100), (int)(DefaultTimeBetweenShots * 1.5 * 100)) / 100f;
                    TimeSinceLastShot = 0f;
                    Shoot(Art.laser);
                }

                yield return 0;
            }
        }

        IEnumerable<int> BossState()
        {
            while (true)
            {
                if (Boss.TimeBeforeNextAttack <= 0)
                {
                    BossAttackOne(Art.laser);
                    Boss.TimeBeforeNextAttack = Boss.TimeBetweenAttacksOne;
                }

                yield return 0;
            }
        }

        private bool CheckCollision()
        {
            foreach (Rectangle obstacle in Room.Obstacles)
            {
                if (Rectangle.Intersects(obstacle))
                {
                    return true;
                }
            }
            return false;
        }

        public void Shoot(Texture2D projectile)
        {
            Room.Projectiles.Add(new Projectile(projectile, CenteredPosition, Vector2.Normalize(Player.Position - Position), 100, Room));
        }

        public void BossAttackOne(Texture2D projectile)
        {
            int step = 10;
            for (int j = 1; j < Room.Size.Y * 20 / step; j++)
            {
                Room.Projectiles.Add(new Projectile(projectile, new Vector2(Room.Size.X * 20, j * 20), Vector2.Normalize(new Vector2(-1, 0)), 100, Room));
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
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _deltaTime = deltaTime;

            if (KnockbackTimeRemaining > 0)
            {
                KnockbackTimeRemaining -= deltaTime;
            }
            if(Type != "Boss")
            {
                if (KnockbackTimeRemaining <= 0)
                {
                    _previousTile = Room.GetTile(Position);
                    deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    TimeSinceLastShot += deltaTime;
                    if (_calculPath)
                    {
                        Room.LineOfView(Room, Position, Player.Position);
                        Path = PathFinding.FindPath((CenteredPosition - Room.Position) / 20, (Player.CenteredPosition - Room.Position) / 20, Room);
                        _calculPath = false;
                    }
                    ApplyBehaviours();
                }
                else
                {
                    Vector2 initPosition = Position;
                    Vector2 velocity = KnockbackDirection * KnockbackSpeed * deltaTime;

                    _position.X += velocity.X;
                    if (CheckCollision())
                        _position.X = initPosition.X;

                    _position.Y += velocity.Y;
                    if (CheckCollision())
                        _position.Y = initPosition.Y;
                }
            }
            else
            {
                if (Boss.TimeBeforeNextAttack > 0)
                {
                    Boss.TimeBeforeNextAttack -= deltaTime;
                }
                ApplyBehaviours();
            }
            
            _currentTile = _previousTile;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Type != "Boss")
                spriteBatch.Draw(Texture, new Rectangle((int)(Position.X + Origin.X), (int)(Position.Y + Origin.Y), Texture.Width, Texture.Height), null, Color, Rotation, Origin, SpriteEffects.None, 0f);
            else
            {
                spriteBatch.Draw(Boss.BossSprite, Position);
            }
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
                    enemy = new Enemy(room, "Seeker", texture, position, (float)(Math.PI * -0.25f), 3);
                    enemy.Speed = 40;
                    enemy.AddBehaviour(enemy.FollowPlayer());
                    return enemy;
            }
            enemy = new Enemy(room, "Seeker", texture, position, 3);
            enemy.Speed = 40;
            enemy.AddBehaviour(enemy.FollowPlayer());

            return enemy;
        }

        public static Enemy CreateArcher(Texture2D texture, Vector2 position, Room room)
        {
            Enemy enemy;
            enemy = new Enemy(room, "Archer", texture, position, 2);
            enemy.Speed = 35;
            enemy.DefaultTimeBetweenShots = 3.5f;
            enemy.CurrentTimeBetweenShots = random.Next((int)(enemy.DefaultTimeBetweenShots * 0.7 * 100), (int)(enemy.DefaultTimeBetweenShots * 1.5 * 100)) / 100f;
            enemy.AddBehaviour(enemy.FollowPlayerThenShoot());

            return enemy;
        }

        public static Enemy CreateBoss(Vector2 position, Room room)
        {
            Enemy enemy;
            enemy = new Enemy(room, "Boss", position, 5);
            Boss.Position = position;
            enemy.Speed = 35;
            enemy.AddBehaviour(enemy.BossState());

            return enemy;
        }
    }
}
