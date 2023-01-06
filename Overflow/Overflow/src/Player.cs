using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Overflow.src
{
    public static class Player
    {
        private static Vector2 _position;
        private static Texture2D _texture;
        private static int _speed;

        private static bool _canPassThroughDoor;
        private static Tile _previousTile;
        private static Tile _currentTile;

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
                return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
            }
        }
        public static void Update(GameTime gameTime, Room room)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 initPositionPlayer = Position;
            Vector2 velocity = PlayerInputs.GetPlayerDirection(PlayerInputs.KeyBoardState) * Speed * deltaTime;

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
        }

        public static void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, Rectangle, Color.White);
        }

        private static bool CheckCollision(List<Rectangle> obstacles)
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
    }
}
