using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Overflow.src
{
    public class Room : Component
    {
        private string[] _room;
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

        public Room(string[] room, Texture2D[] tileSet)
        {
            _room = room;
            Size = new Vector2(room[0].Length, room.Length);
            Doors = new bool[] {false, false, false, false};
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
            Console.WriteLine(Size);
            for (int j = 0; j < Size.Y; j++)
            {
                for (int i = 0; i < Size.X; i++)
                {
                    string character = _room[j][i].ToString();
                    if (character == " ")
                    {
                        tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet[8], "Grass");
                    }
                    else if(character == "|" || character == "-" || character == ".")
                    {
                        tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), SelectWall(character, new int[] {i, j}), "Wall");
                    }
                    else if (character == "o")
                    {
                        tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet[9], "Door");
                    }
                }
            }

            foreach (Tile tile in tiles)
            {
                if(tile != null)
                {
                    tile.Position = new Vector2(tile.Position.X + Position.X, tile.Position.Y + Position.Y);

                    if (tile.Type == "Wall")
                    {
                        Obstacles.Add(new Rectangle((int)tile.Position.X, (int)tile.Position.Y, tile.Texture.Width, tile.Texture.Height));
                    }
                }
            }

            return tiles;
        }

        private Texture2D SelectWall(string character, int[] position)
        {
            if(character == "-")
            {
                return _tileSet[4];
            }
            else if (character == "|")
            {
                return _tileSet[5];
            }
            else if (character == ".")
            {

            }
            return _tileSet[0];
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
                if(tile != null)
                {
                    tile.Draw(gameTime, spritebatch);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
