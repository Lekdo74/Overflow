using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;

namespace Overflow.src
{
    public static class Player
    {
        private static int _health;
        private static float _iFramesDuration;
        private static float _iFramesTimeRemaining;

        private static Vector2 _position;
        private static Texture2D _texture;

        private static string _currentAnimation;
        private static AnimatedSprite _perso;

        private static Vector2 _oldPlayerDirection;
        private static Vector2 _newPlayerDirection;
        private static int _speed;

        private static bool _canPassThroughDoor;
        private static Tile _previousTile;
        private static Tile _currentTile;

        public static int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public static float IFramesDuration
        {
            get { return _iFramesDuration; }
            set { _iFramesDuration = value; }
        }

        public static float IFramesTimeRemaining
        {
            get { return _iFramesTimeRemaining; }
            set { _iFramesTimeRemaining = value; }
        }

        public static Vector2 Position
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

        public static Vector2 CenteredPosition
        {
            get { return Position + new Vector2(Texture.Width / 2, Texture.Height / 2); }
        }

        public static int[] GridPosition(Room room)
        {
            return new int[] { (int)(_position.X + Texture.Width / 2 - room.Position.X) / 20, (int)(_position.Y + Texture.Height / 2 - room.Position.Y) / 20 };
        }

        public static Texture2D Texture
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

        public static AnimatedSprite Perso
        {
            get { return _perso; }
            set { _perso = value; }
        }

        public static string CurrentAnimation
        {
            get { return _currentAnimation; }
            set { _currentAnimation = value; }
        }

        public static int Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public static Vector2 OldPlayerDirection
        {
            get { return _oldPlayerDirection; }
            set { _oldPlayerDirection = value; }
        }

        public static Vector2 NewPlayerDirection
        {
            get { return _newPlayerDirection; }
            set { _newPlayerDirection = value; }
        }

        public static bool CanPassThroughDoor
        {
            get
            {
                return _canPassThroughDoor;
            }
            set
            {
                _canPassThroughDoor = value;
            }
        }

        public static Tile PreviousTile
        {
            get
            {
                return _previousTile;
            }
            set
            {
                _previousTile = value;
            }
        }

        public static Tile CurrentTile
        {
            get
            {
                return _currentTile;
            }
            set
            {
                _currentTile = value;
            }
        }

        public static Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public static Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)Position.X + 1, (int)Position.Y + 2, _texture.Width + 1, _texture.Height - 2);
            }
        }

        private static bool CheckCollision(List<Rectangle> obstacles)
        {
            foreach (Rectangle obstacle in obstacles)
            {
                if (HitBox.Intersects(obstacle))
                {
                    return true;
                }
            }
            return false;
        }
        
        public static int CheckDamage(List<Enemy> enemies, List<Projectile> projectiles)
        {
            foreach (Enemy enemy in enemies)
            {
                if (HitBox.Intersects(enemy.Rectangle))
                {
                    return 1;
                }
            }
            foreach (Projectile projectile in projectiles)
            {
                if (HitBox.Intersects(projectile.Rectangle))
                {
                    return 1;
                }
            }
            return 0;
        }

        public static void TakeDamage(int damage)
        {
            if(damage != 0)
            {
                if(IFramesTimeRemaining <= 0)
                {
                    Health -= damage;
                    IFramesTimeRemaining = IFramesDuration;
                    Console.WriteLine(Health);
                    if (Health <= 0)
                        Console.WriteLine("Dead.");
                }
            }
        }

        public static void Update(GameTime gameTime, Room room)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(IFramesTimeRemaining > 0)
                IFramesTimeRemaining -= deltaTime;

            Vector2 initPositionPlayer = Position;
            _newPlayerDirection = PlayerInputs.GetPlayerDirection(PlayerInputs.KeyBoardState);
            Vector2 velocity = _newPlayerDirection * Speed * deltaTime;

            _position.X += velocity.X;
            if (CheckCollision(room.Obstacles) || !room.InsideRoom(Position))
            {
                _position.X = initPositionPlayer.X;
            }
            _position.Y += velocity.Y;
            if (CheckCollision(room.Obstacles) || !room.InsideRoom(Position))
            {
                _position.Y = initPositionPlayer.Y;
            }

            _perso.Play(PlayerInputs.GetAnimation(_oldPlayerDirection, _newPlayerDirection));
            _perso.Update(gameTime);
            _oldPlayerDirection = _newPlayerDirection;
        }

        public static void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            spritebatch.Draw(_perso, Position);
            //spritebatch.Draw(Art.tilesetLevel1.TopDoor, Position, Color.Red);
            //spritebatch.Draw(Art.tilesetLevel1.TopDoor, CenteredPosition, Color.Green);
        }
    }
}
