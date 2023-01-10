using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private static float _timeBetweenDashes;
        private static float _timeBeforeNextDash;
        private static float _dashDuration;
        private static float _dashTimeRemaining;
        private static int _dashSpeed;
        private static float _timeBetweenDashEffects;
        private static float _timeBeforeNextDashEffect;
        private static float _dashEffectDuration;
        private static List<GhostEffectDash> _ghostEffectDashes = new List<GhostEffectDash>();
        private static Texture2D _currentDashTexture;

        private static float _knockbackDuration;
        private static float _knockbackTimeRemaining;
        private static bool _isKnockbacked = true;
        private static int _knockbackSpeed;

        private static float _timeBetweenAttacks;
        private static float _timeBeforeNextAttack;

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

        public static float TimeBetweenDashes
        {
            get { return _timeBetweenDashes; }
            set { _timeBetweenDashes = value; }
        }

        public static float TimeBeforeNextDash
        {
            get { return _timeBeforeNextDash; }
            set { _timeBeforeNextDash = value; }
        }

        public static float DashDuration
        {
            get { return _dashDuration; }
            set { _dashDuration = value; }
        }
        public static float DashTimeRemaining
        {
            get { return _dashTimeRemaining; }
            set { _dashTimeRemaining = value; }
        }

        public static int DashSpeed
        {
            get { return _dashSpeed; }
            set { _dashSpeed = value; }
        }

        public static float TimeBetweenDashEffects
        {
            get { return _timeBetweenDashEffects; }
            set { _timeBetweenDashEffects = value; }
        }

        public static float TimeBeforeNextDashEffect
        {
            get { return _timeBeforeNextDashEffect; }
            set { _timeBeforeNextDashEffect = value; }
        }

        public static float DashEffectDuration
        {
            get { return _dashEffectDuration; }
            set { _dashEffectDuration = value; }
        }

        public static List<GhostEffectDash> GhostEffectDashes
        {
            get { return _ghostEffectDashes; }
            set { _ghostEffectDashes = value; }
        }

        public static Texture2D CurrentDashTexture
        {
            get { return _currentDashTexture; }
            set { _currentDashTexture = value; }
        }

        public static float KnockbackDuration
        {
            get { return _knockbackDuration; }
            set { _knockbackDuration = value; }
        }
        public static float KnockbackTimeRemaining
        {
            get { return _knockbackTimeRemaining; }
            set { _knockbackTimeRemaining = value; }
        }
        public static bool IsKnockbacked
        {
            get { return _isKnockbacked; }
            set { _isKnockbacked = value; }
        }
        public static int KnockbackSpeed
        {
            get { return _knockbackSpeed; }
            set { _knockbackSpeed = value; }
        }

        public static float TimeBetweenAttacks
        {
            get { return _timeBetweenAttacks; }
            set { _timeBetweenAttacks = value; }
        }
        public static float TimeBeforeNextAttack
        {
            get { return _timeBeforeNextAttack; }
            set { _timeBeforeNextAttack = value; }
        }

        public static Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
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
        
        public static int CheckDamage(Room currentRoom)
        {
            if(IFramesTimeRemaining <= 0)
            {
                foreach (Enemy enemy in currentRoom.Enemies)
                {
                    if (HitBox.Intersects(enemy.Rectangle))
                    {
                        if (enemy.Direction != Vector2.Zero)
                            NewPlayerDirection = Vector2.Normalize(enemy.Direction);
                        else
                            NewPlayerDirection = Vector2.Normalize(CenteredPosition - enemy.CenteredPosition);
                        return 1;
                    }
                }
                Projectile projectileThatTouched = null;
                foreach (Projectile projectile in currentRoom.Projectiles)
                {
                    if (HitBox.Intersects(projectile.Rectangle))
                    {
                        NewPlayerDirection = Vector2.Normalize(projectile.Direction);
                        projectileThatTouched = projectile;
                    }
                }
                if(projectileThatTouched != null)
                {
                    currentRoom.Projectiles.Remove(projectileThatTouched);
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
                    IsKnockbacked = false;
                    Health -= damage;
                    KnockbackTimeRemaining = KnockbackDuration;
                    IFramesTimeRemaining = IFramesDuration;
                    Console.WriteLine(Health);
                }
            }
        }

        public static void Update(GameTime gameTime, Room room)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(IFramesTimeRemaining > 0)
                IFramesTimeRemaining -= deltaTime;

            if (KnockbackTimeRemaining > 0)
            {
                KnockbackTimeRemaining -= deltaTime;
            }
            else if (!IsKnockbacked)
                IsKnockbacked = true;

            if (TimeBeforeNextAttack > 0)
                TimeBeforeNextAttack -= deltaTime;

            if (TimeBeforeNextDash > 0)
                TimeBeforeNextDash -= deltaTime;
            if (DashTimeRemaining > 0)
                DashTimeRemaining -= deltaTime;
            else
                Speed = 50;
            if (TimeBeforeNextDashEffect > 0)
                TimeBeforeNextDashEffect -= deltaTime;
            else if (DashTimeRemaining > 0)
            {
                GhostEffectDashes.Add(new GhostEffectDash(Position, DashEffectDuration));
                TimeBeforeNextDashEffect = TimeBetweenDashEffects;
            }

            Vector2 initPositionPlayer = Position;
            Vector2 velocity;
            if (IsKnockbacked)
            {
                _newPlayerDirection = PlayerInputs.GetPlayerDirection(PlayerInputs.KeyBoardState);
                velocity = _newPlayerDirection * Speed * deltaTime;
            }
            else
            {
                velocity = _newPlayerDirection * KnockbackSpeed * deltaTime;
            }

            _position.X += velocity.X;
            if (CheckCollision(room.Obstacles) || !room.InsideRoom(Position) || (room.Enemies.Count > 0 && Room.DoorsTypes.Contains(room.GetPlayerTile().Type)))
            {
                _position.X = initPositionPlayer.X;
            }
            _position.Y += velocity.Y;
            if (CheckCollision(room.Obstacles) || !room.InsideRoom(Position) || (room.Enemies.Count > 0 && Room.DoorsTypes.Contains(room.GetPlayerTile().Type)))
            {
                _position.Y = initPositionPlayer.Y;
            }

            _perso.Play(PlayerInputs.GetAnimation(_oldPlayerDirection, _newPlayerDirection, ref _currentDashTexture));

            if(DashTimeRemaining > 0)
                _perso.Update(deltaTime * 2);
            else
                _perso.Update(deltaTime);

            if (PlayerSlash.RemainingAnimationTime > 0)
                PlayerSlash.RemainingAnimationTime -= deltaTime;
            if (PlayerInputs.MouseState.LeftButton == ButtonState.Pressed && TimeBeforeNextAttack <= 0)
            {
                TimeBeforeNextAttack = TimeBetweenAttacks;
                PlayerSlash.RemainingAnimationTime = 0.35f;
                PlayerSlash.Slash.Play("slash");
            }
            PlayerSlash.Update(deltaTime);

            if (PlayerInputs.KeyBoardState.IsKeyDown(Keys.LeftShift) && TimeBeforeNextDash <= 0)
            {
                DashTimeRemaining = DashDuration;
                TimeBeforeNextDash = TimeBetweenDashes;
                IFramesTimeRemaining = DashDuration;
                Speed = DashSpeed;
            }

            List<GhostEffectDash> dashesOutOfRemainingTime = new List<GhostEffectDash>();
            foreach(GhostEffectDash ghostEffectDash in GhostEffectDashes)
            {
                if (ghostEffectDash.RemainingTime <= 0)
                    dashesOutOfRemainingTime.Add(ghostEffectDash);
                else
                    ghostEffectDash.RemainingTime -= deltaTime;
            }
            foreach (GhostEffectDash ghostEffectDash in dashesOutOfRemainingTime)
            {
                GhostEffectDashes.Remove(ghostEffectDash);
            }

            _oldPlayerDirection = _newPlayerDirection;
        }

        public static void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            spritebatch.Draw(_perso, Position);
            PlayerSlash.Draw(spritebatch);
            foreach (GhostEffectDash ghostEffectDash in GhostEffectDashes)
            {
                ghostEffectDash.Draw(spritebatch);
            }
        }
    }
}
