using Microsoft.Xna.Framework;
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
                return Boss.Position + new Vector2(Boss.OffSetX + Boss.Width / 2,Boss.OffSetY + Boss.Height / 2);
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
                Vector2 initPosition = Boss.Position;

                if (KnockbackTimeRemaining <= 0)
                {
                    Direction = (Player.CenteredPosition - Boss.CenteredPosition);
                    Velocity = Vector2.Normalize(Direction) * _speed * _deltaTime;
                }
                else
                {
                    Velocity = KnockbackDirection * KnockbackSpeed * _deltaTime;
                }

                if (Velocity.X < 0)
                    Boss.Direction = false;
                else if (Velocity.X > 0)
                    Boss.Direction = true;

                if(Boss.AttackAnimationTimeRemaining <= 0)
                {
                    Boss.AttackAnimation = false;
                    Boss.Position += Velocity;
                }
                else
                {
                    Boss.AttackAnimation = true;
                    if(KnockbackTimeRemaining > 0)
                    {
                        Boss.Position += Velocity;
                    }
                }
                    

                if (Boss.TimeBeforeNextAttack <= 0)
                {
                    Boss.CurrentAnimation = random.Next(1, 3);
                    if (Boss.Direction)
                    {
                        switch (Boss.CurrentAnimation)
                        {
                            case 1:
                                Boss.BossSprite.Play("attackOneRight");
                                break;
                            case 2:
                                Boss.BossSprite.Play("attackTwoRight");
                                break;
                        }
                    }
                    else
                    {
                        switch (Boss.CurrentAnimation)
                        {
                            case 1:
                                Boss.BossSprite.Play("attackOneLeft");
                                break;
                            case 2:
                                Boss.BossSprite.Play("attackTwoLeft");
                                break;
                        }
                    }
                    Boss.AttackAnimation = true;
                    switch (Boss.CurrentAnimation)
                    {
                        case 1:
                            Boss.AttackAnimationTimeRemaining = Boss.AttackOneAnimationDuration;
                            Boss.AttackAnimationTimeRemainingBeforeAttackFrame = Boss.AttackOneAnimationDurationBeforeAttackFrame;
                            break;
                        case 2:
                            Boss.AttackAnimationTimeRemaining = Boss.AttackTwoAnimationDuration;
                            Boss.AttackAnimationTimeRemainingBeforeAttackFrame = Boss.AttackTwoAnimationDurationBeforeAttackFrame;
                            break;
                    }
                    Boss.TimeBeforeNextAttack = Boss.TimeBetweenAttacks;
                }
                if(Boss.AttackAnimationTimeRemainingBeforeAttackFrame <= 0 && Boss.AttackAnimation)
                {
                    switch (Boss.CurrentAnimation)
                    {
                        case 1:
                            Boss.AttackAnimationTimeRemainingBeforeAttackFrame = Boss.AttackOneAnimationDuration;
                            BossAttackTwo(Art.bouleRouge);
                            break;
                        case 2:
                            Boss.AttackAnimationTimeRemainingBeforeAttackFrame = Boss.AttackTwoAnimationDuration;
                            BossAttackTwo(Art.bouleRouge);
                            break;
                    }
                }

                if (_previousTile != _currentTile)
                {
                    CalculPath = true;
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
            int randomNumber = random.Next(0, 4);
            int speed = 100;
            switch (randomNumber)
            {
                case 0:
                    int offSetY = -8;
                    for (int i = 1; i < Room.Size.X; i++)
                    {
                        if (i % 2 == 0)
                            Room.Projectiles.Add(new Projectile(projectile, new Vector2(i * 20, offSetY), Vector2.Normalize(new Vector2(0, 1)), speed, Room));
                        else
                            Room.Projectiles.Add(new Projectile(projectile, new Vector2(i * 20, offSetY - 20), Vector2.Normalize(new Vector2(0, 1)), speed, Room));
                    }
                    return;
                case 1:
                    for (int j = 1; j < Room.Size.Y; j++)
                    {
                        if (j % 2 == 0)
                            Room.Projectiles.Add(new Projectile(projectile, new Vector2(Room.Size.X * 20 + 20, j * 20), Vector2.Normalize(new Vector2(-1, 0)), speed + 50, Room));
                        else
                        {
                            Room.Projectiles.Add(new Projectile(projectile, new Vector2(Room.Size.X * 20, j * 20), Vector2.Normalize(new Vector2(-1, 0)), speed + 50, Room));
                            Room.Projectiles.Add(new Projectile(projectile, new Vector2(Room.Size.X * 20 + 40, j * 20), Vector2.Normalize(new Vector2(-1, 0)), speed + 50, Room));
                        }
                    }
                    return;
                case 2:
                    for (int i = 1; i < Room.Size.X; i++)
                    {
                        if (i % 2 == 0)
                            Room.Projectiles.Add(new Projectile(projectile, new Vector2(i * 20, Room.Size.Y * 20), Vector2.Normalize(new Vector2(0, -1)), speed, Room));
                        else
                            Room.Projectiles.Add(new Projectile(projectile, new Vector2(i * 20, Room.Size.Y * 20 + 20), Vector2.Normalize(new Vector2(0, -1)), speed, Room));
                    }
                    return;
                case 3:
                    int offSetX = -8;
                    for (int j = 1; j < Room.Size.Y; j++)
                    {
                        if (j % 2 == 0)
                            Room.Projectiles.Add(new Projectile(projectile, new Vector2(offSetX - 20, j * 20), Vector2.Normalize(new Vector2(1, 0)), speed + 50, Room));
                        else
                        {
                            Room.Projectiles.Add(new Projectile(projectile, new Vector2(offSetX, j * 20), Vector2.Normalize(new Vector2(1, 0)), speed + 50, Room));
                            Room.Projectiles.Add(new Projectile(projectile, new Vector2(offSetX - 40, j * 20), Vector2.Normalize(new Vector2(1, 0)), speed + 50, Room));
                        }
                    }
                    return;
            }
        }

        public void BossAttackTwo(Texture2D projectile)
        {
            int randomNumber = random.Next(0, 1);
            int step = 8;
            float rotation;
            Vector2 direction;
            int speed = 60;
            switch (randomNumber)
            {
                case 0:
                    rotation = 0;
                    int offSetX = -8;
                    int offSetY = -8;
                    while(rotation < 90)
                    {
                        rotation += step;

                        direction = Vector2.Normalize(new Vector2((float)Math.Cos(-rotation * Math.PI / 180), (float)Math.Sin(rotation * Math.PI / 180)));
                        Room.Projectiles.Add(new Projectile(projectile, new Vector2(offSetX, offSetY), direction, speed + 50, Room));

                        direction = Vector2.Normalize(new Vector2((float)Math.Cos(rotation * Math.PI / 180), (float)Math.Sin(-rotation * Math.PI / 180)));
                        Room.Projectiles.Add(new Projectile(projectile, new Vector2(offSetX,Room.Size.Y * 20 - offSetY), direction, speed + 50, Room));
                    }
                    return;
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
            _previousTile = Room.GetTile(Position);

            if (KnockbackTimeRemaining > 0)
            {
                KnockbackTimeRemaining -= deltaTime;
            }

            if(Boss.AttackAnimationTimeRemaining > 0)
            {
                Boss.AttackAnimationTimeRemaining -= deltaTime;
            }

            if(Boss.AttackAnimationTimeRemainingBeforeAttackFrame > 0)
            {
                Boss.AttackAnimationTimeRemainingBeforeAttackFrame -= deltaTime;
            }


            if(Type != "Boss")
            {
                if (KnockbackTimeRemaining <= 0)
                {
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
                if (Boss.AttackAnimationTimeRemaining <= 0)
                {
                    if (Velocity.X < 0)
                    {
                        Boss.BossSprite.Play("walkLeft");
                    }
                    else if (Velocity.X > 0)
                    {
                        Boss.BossSprite.Play("walkRight");
                    }
                }
                Boss.BossSprite.Update(deltaTime);

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
                spriteBatch.Draw(Boss.BossSprite, Boss.Position);
            }
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
            Boss.Room = room;
            enemy.Speed = 20;
            enemy.AddBehaviour(enemy.BossState());

            return enemy;
        }
    }
}
