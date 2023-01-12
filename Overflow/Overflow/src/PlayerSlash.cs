using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;

namespace Overflow.src
{
    public static class PlayerSlash
    {
        private static AnimatedSprite _slash = new AnimatedSprite(Art.slashSpriteSheet);
        private static float _remainingAnimationTime;

        private static Vector2 _offSetPosition;

        private static Vector2 _positionTop = new Vector2(0, -6);
        private static Vector2 _positionTopRight = new Vector2(3, -6);
        private static Vector2 _positionRight = new Vector2(2, 0);
        private static Vector2 _positionBottomRight = new Vector2(3, 6);
        private static Vector2 _positionBottom = new Vector2(0, 6);
        private static Vector2 _positionBottomLeft = new Vector2(-3, 5);
        private static Vector2 _positionLeft = new Vector2(-2, 0);
        private static Vector2 _positionTopLeft = new Vector2(-3, -6);

        public static AnimatedSprite Slash
        {
            get { return _slash; }
            set { _slash = value; }
        }

        public static float RemainingAnimationTime
        {
            get { return _remainingAnimationTime; }
            set { _remainingAnimationTime = value; }
        }

        public static Vector2 Direction
        {
            get { return Vector2.Normalize(PlayerInputs.MousePosition() - Player.CenteredPosition); }
        }

        public static Vector2 Position
        {
            get { return Player.CenteredPosition; }
        }

        public static Vector2 OffSetPosition
        {
            get
            {
                float rotation = RotationDegrees;
                if (rotation > 336.5f || rotation <= 22.5f)
                    _offSetPosition = PositionRight;
                else if (rotation > 22.5f && rotation <= 67.5f)
                    _offSetPosition = PositionBottomRight;
                else if (rotation > 67.5f && rotation <= 112.5f)
                    _offSetPosition = PositionBottom;
                else if (rotation > 112.5f && rotation <= 155.5f)
                    _offSetPosition = PositionBottomLeft;
                else if (rotation > 155.5f && rotation <= 202.5f)
                    _offSetPosition = PositionLeft;
                else if (rotation > 202.5f && rotation <= 247.5f)
                    _offSetPosition = PositionTopLeft;
                else if (rotation > 247.5f && rotation <= 292.5f)
                    _offSetPosition = PositionTop;
                else
                    _offSetPosition = PositionTopRight;
                return _offSetPosition;
            }
        }

        public static Rectangle Rectangle
        {
            get { return new Rectangle((int)(Position.X + OffSetPosition.X - Slash.Origin.X), (int)(Position.Y + OffSetPosition.Y - Slash.Origin.Y), 24, 24); }
        }

        public static float Rotation
        {
            get { return (float)Math.Atan2(Direction.Y, Direction.X); }
        }

        public static float RotationDegrees
        {
            get
            {
                if (Rotation < 0)
                {
                    return (float)(360 + (Rotation * (180 / Math.PI)));
                }
                else
                    return (float)(Rotation * (180 / Math.PI));
            }
        }

        public static Vector2 PositionTop
        {
            get { return _positionTop; }
            set { _positionTop = value; }
        }
        public static Vector2 PositionTopRight
        {
            get { return _positionTopRight; }
            set { _positionTopRight = value; }
        }
        public static Vector2 PositionRight
        {
            get { return _positionRight; }
            set { _positionRight = value; }
        }
        public static Vector2 PositionBottomRight
        {
            get { return _positionBottomRight; }
            set { _positionBottomRight = value; }
        }
        public static Vector2 PositionBottom
        {
            get { return _positionBottom; }
            set { _positionBottom = value; }
        }
        public static Vector2 PositionBottomLeft
        {
            get { return _positionBottomLeft; }
            set { _positionBottomLeft = value; }
        }
        public static Vector2 PositionLeft
        {
            get { return _positionLeft; }
            set { _positionLeft = value; }
        }
        public static Vector2 PositionTopLeft
        {
            get { return _positionTopLeft; }
            set { _positionTopLeft = value; }
        }

        public static void Update(float deltaTime)
        {
            if(RemainingAnimationTime > 0)
                Slash.Update(deltaTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (RemainingAnimationTime > 0)
            {
                spriteBatch.Draw(Slash, Position + OffSetPosition, Rotation);
            }
        }
    }
}
