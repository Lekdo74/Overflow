using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Overflow.src
{
    public class Room : Component
    {
        private Vector2 _size;
        private bool[] _doors;
        private Vector2[] _spawnPoints;
        private List<Rectangle> _obstacles;
        private Vector2 _position;

        private Tile[,] _tiles;
        private Texture2D[] _tileSet;

        private float xMin;
        private float yMin;
        private float xMax;
        private float yMax;

        //private Enemy[] enemies

        public Room(Vector2 size, Texture2D[] tileSet, bool[] doors)
        {
            Size = size;
            Doors = doors;
            SpawnPoints = new Vector2[4];
            Obstacles = new List<Rectangle>();
            _tileSet = tileSet;
            Position = new Vector2((Settings.nativeWidthResolution - Size.X * _tileSet[0].Width) / 2, (Settings.nativeHeightResolution - Size.Y * _tileSet[0].Height) / 2);

            xMin = Position.X;
            yMin = Position.Y;
            xMax = Position.X + (Size.X) * _tileSet[0].Width;
            yMax = Position.Y + (Size.Y) * _tileSet[0].Height;

            _tiles = BuildRoom();
        }

        public Vector2 Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public bool[] Doors
        {
            get
            {
                return _doors;
            }
            set
            {
                _doors = value;
            }
        }

        public Vector2[] SpawnPoints
        {
            get
            {
                return _spawnPoints;
            }
            set
            {
                _spawnPoints = value;
            }
        }

        public List<Rectangle> Obstacles
        {
            get
            {
                return _obstacles;
            }
            set
            {
                _obstacles = value;
            }
        }

        public Vector2 Position
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

        private Tile[,] BuildRoom()
        {
            Tile[,] tiles = new Tile[(int)Size.X, (int)Size.Y];
            for (int j = 1; j < Size.X - 1; j++)
            {
                for (int i = 1; i < Size.Y - 1; i++)
                {
                    tiles[j, i] = new Tile(new Vector2(20 * j, 20 * i), _tileSet[8], "Grass");
                }
            }

            for (int j = 1; j < Size.X - 1; j++)
            {
                tiles[j, 0] = new Tile(new Vector2(20 * j, 0), _tileSet[4], "Wall");
            }
            for (int j = 1; j < Size.X - 1; j++)
            {
                tiles[j, (int)Size.Y - 1] = new Tile(new Vector2(20 * j, 20 * (Size.Y - 1)), _tileSet[6], "Wall");
            }
            for (int i = 1; i < Size.Y - 1; i++)
            {
                tiles[0, i] = new Tile(new Vector2(0, 20 * i), _tileSet[7], "Wall");
            }
            for (int i = 1; i < Size.Y - 1; i++)
            {
                tiles[(int)Size.X - 1, i] = new Tile(new Vector2(20 * (Size.X - 1), 20 * i), _tileSet[5], "Wall");
            }

            tiles[0, 0] = new Tile(new Vector2(0, 0), _tileSet[0], "Wall");
            tiles[(int)Size.X - 1, 0] = new Tile(new Vector2(20 * (Size.X - 1), 0), _tileSet[1], "Wall");
            tiles[(int)Size.X - 1, (int)Size.Y - 1] = new Tile(new Vector2(20 * (Size.X - 1), 20 * (Size.Y - 1)), _tileSet[2], "Wall");
            tiles[0, (int)Size.Y - 1] = new Tile(new Vector2(0, 20 * (Size.Y - 1)), _tileSet[3], "Wall");

            if (Doors[0])
            {
                tiles[(int)Size.X / 2, 0] = new Tile(new Vector2(20 * ((int)Size.X / 2), 0), _tileSet[9], "DoorTop");
                SpawnPoints[0] = new Vector2(20 * ((int)Size.X / 2) + 20 * 0.5f, 20 * 0.5f) + Position;
            }
            if (Doors[1])
            {
                tiles[(int)Size.X - 1, (int)Size.Y / 2] = new Tile(new Vector2(20 * (Size.X - 1), 20 * ((int)Size.Y / 2)), _tileSet[9], "DoorRight");
                SpawnPoints[1] = new Vector2(20 * ((int)Size.X - 1) + 20 * 0.5f, 20 * ((int)Size.Y / 2) + 20 * 0.5f) + Position;
            }
            if (Doors[2])
            {
                tiles[(int)Size.X / 2, (int)Size.Y - 1] = new Tile(new Vector2(20 * ((int)Size.X / 2), 20 * (Size.Y - 1)), _tileSet[9], "DoorBottom");
                SpawnPoints[2] = new Vector2(20 * ((int)Size.X / 2) + 20 * 0.5f, 20 * ((int)Size.Y - 1) + 20 * 0.5f) + Position;
            }
            if (Doors[3])
            {
                tiles[0, (int)Size.Y / 2] = new Tile(new Vector2(0, 20 * ((int)Size.Y / 2)), _tileSet[9], "DoorLeft");
                SpawnPoints[3] = new Vector2(20 * 0.5f, 20 * ((int)Size.Y / 2) + 20 * 0.5f) + Position;
            }

            foreach (Tile tile in tiles)
            {
                tile.Position = new Vector2(tile.Position.X + Position.X, tile.Position.Y + Position.Y);

                if (tile.Type == "Wall")
                {
                    Obstacles.Add(new Rectangle((int)tile.Position.X, (int)tile.Position.Y, tile.Texture.Width, tile.Texture.Height));
                }
            }

            return tiles;
        }

        public Tile GetTile(Vector2 position)
        {
            return _tiles[((int)position.X - (int)Position.X) / _tileSet[0].Width, ((int)position.Y - (int)Position.Y) / _tileSet[0].Height];
        }
        public Tile GetTile(Vector2 position, Player player)
        {
            return _tiles[((int)position.X - (int)Position.X + player.Texture.Width / 2) / _tileSet[0].Width, ((int)position.Y - (int)Position.Y + player.Texture.Height / 2) / _tileSet[0].Height];
        }

        public bool InsideRoom(Vector2 position)
        {
            if (position.Y < yMin || position.X > xMax || position.Y > yMax || position.X < xMin)
            {
                return false;
            }
            return true;
        }
        public bool InsideRoom(Player player)
        {
            if (player.Position.Y < yMin || player.Position.X + player.Texture.Width > xMax || player.Position.Y + player.Texture.Height > yMax || player.Position.X < xMin)
            {
                return false;
            }
            return true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            foreach (Tile tile in _tiles)
            {
                tile.Draw(gameTime, spritebatch);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
