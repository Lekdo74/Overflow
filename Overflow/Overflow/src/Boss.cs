using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public static class Boss
    {
        private static int _health;

        private static Vector2 _position;

        private static int _currentAttack;
        private static int _numberOfAttacks;
        private static float _timeBetweenAttacksOne;
        private static float _timeBeforeNextAttack;

        private static AnimatedSprite _bossSprite;
        private static int _width;
        private static int _height;
        private static int _offSetX;
        private static int _offSetY;

        private static int _speed;

        public static int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public static Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public static int NumberOfAttacks
        {
            get { return _numberOfAttacks; }
            set { _numberOfAttacks = value; }
        }
        public static float TimeBetweenAttacksOne
        {
            get { return _timeBetweenAttacksOne; }
            set { _timeBetweenAttacksOne = value; }
        }
        public static float TimeBeforeNextAttack
        {
            get { return _timeBeforeNextAttack; }
            set { _timeBeforeNextAttack = value; }
        }

        public static AnimatedSprite BossSprite
        {
            get { return _bossSprite; }
            set { _bossSprite = value; }
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

        public static int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public static Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X + OffSetX, (int)Position.Y + OffSetY, Width, Height); }
        }
    }
}
