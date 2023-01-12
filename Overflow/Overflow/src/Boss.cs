using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Overflow.src
{
    public static class Boss
    {
        private static Room _room;

        private static bool _direction;
        private static Vector2 _position;

        private static float _timeBetweenAttacks;
        private static float _timeBeforeNextAttack;

        private static float _timeBetweenPassiveAttacks;
        private static float _timeBeforeNextPassiveAttack;

        private static bool _attackAnimation;
        private static int _currentAnimation;

        private static float _attackOneAnimationDuration;
        private static float _attackOneAnimationDurationBeforeAttackFrame;

        private static float _attackTwoAnimationDuration;
        private static float _attackTwoAnimationDurationBeforeAttackFrame;

        private static float _attackAnimationTimeRemaining;
        private static float _attackAnimationTimeRemainingBeforeAttackFrame;

        private static AnimatedSprite _bossSprite;
        private static Texture2D _healthTexture;

        private static int _width;
        private static int _height;
        private static int _offSetX;
        private static int _offSetY;

        private static List<Projectile> _projectilesFollowingPlayer = new List<Projectile>();

        public static Room Room
        {
            get { return _room; }
            set { _room = value; }
        }

        public static bool Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public static Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public static Vector2 CenteredPosition
        {
            get { return Position + new Vector2(OffSetX + Width / 2, OffSetY + Height / 2); }
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

        public static float TimeBetweenPassiveAttacks
        {
            get { return _timeBetweenPassiveAttacks; }
            set { _timeBetweenPassiveAttacks = value; }
        }

        public static float TimeBeforeNextPassiveAttack
        {
            get { return _timeBeforeNextPassiveAttack; }
            set { _timeBeforeNextPassiveAttack = value; }
        }

        public static bool AttackAnimation
        {
            get { return _attackAnimation; }
            set { _attackAnimation = value; }
        }

        public static int CurrentAnimation
        {
            get { return _currentAnimation; }
            set { _currentAnimation = value; }
        }

        public static float AttackOneAnimationDuration
        {
            get { return _attackOneAnimationDuration; }
            set { _attackOneAnimationDuration = value; }
        }

        public static float AttackAnimationTimeRemaining
        {
            get { return _attackAnimationTimeRemaining; }
            set { _attackAnimationTimeRemaining = value; }
        }

        public static float AttackOneAnimationDurationBeforeAttackFrame
        {
            get { return _attackOneAnimationDurationBeforeAttackFrame; }
            set { _attackOneAnimationDurationBeforeAttackFrame = value; }
        }

        public static float AttackAnimationTimeRemainingBeforeAttackFrame
        {
            get { return _attackAnimationTimeRemainingBeforeAttackFrame; }
            set { _attackAnimationTimeRemainingBeforeAttackFrame = value; }
        }

        public static float AttackTwoAnimationDuration
        {
            get { return _attackTwoAnimationDuration; }
            set { _attackTwoAnimationDuration = value; }
        }

        public static float AttackTwoAnimationDurationBeforeAttackFrame
        {
            get { return _attackTwoAnimationDurationBeforeAttackFrame; }
            set { _attackTwoAnimationDurationBeforeAttackFrame = value; }
        }

        public static AnimatedSprite BossSprite
        {
            get { return _bossSprite; }
            set { _bossSprite = value; }
        }

        public static Texture2D HealthTexture
        {
            get { return _healthTexture; }
            set { _healthTexture = value; }
        }

        public static int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public static int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public static int OffSetX
        {
            get { return _offSetX; }
            set { _offSetX = value; }
        }

        public static int OffSetY
        {
            get { return _offSetY; }
            set { _offSetY = value; }
        }

        public static List<Projectile> ProjectilesFollowingPlayer
        {
            get { return _projectilesFollowingPlayer; }
            set { _projectilesFollowingPlayer = value; }
        }

        public static Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X + OffSetX, (int)Position.Y + OffSetY, Width, Height); }
        }

        public static void InitializeBoss()
        {
            TimeBetweenAttacks = 2.5f;
            TimeBeforeNextAttack = 1f;
            TimeBetweenPassiveAttacks = 4f;
            TimeBeforeNextPassiveAttack = TimeBetweenPassiveAttacks;
            AttackAnimation = false;
            AttackOneAnimationDuration = 1.56f;
            AttackOneAnimationDurationBeforeAttackFrame = 1.2f;
            AttackTwoAnimationDuration = 1.2f;
            AttackTwoAnimationDurationBeforeAttackFrame = 0.96f;
            _attackAnimationTimeRemaining = 0;
            BossSprite = new AnimatedSprite(Art.bossSpriteSheet);

            Width = 32;
            Height = 32;
            OffSetX = 16;
            OffSetY = 16;
            BossSprite.Origin = Vector2.Zero;
        }

        public static Vector2 FindTileToRoamTo(float distance)
        {
            Tile tile = Room.GetRandomTerrainTileInRoom();
            while(Vector2.Distance(tile.Position, Position) < distance)
                tile = Room.GetRandomTerrainTileInRoom();
            return tile.Position;
        }
    }
}
