using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

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

        string[] terrain;
        string[] walls;

        //private Enemy[] enemies

        public Room(string[] room, Texture2D[] tileSet, bool[] doors)
        {
            _room = room;
            Size = new Vector2(room[0].Length, room.Length);
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
                    else if(character == "|" || character == "-" || character == "." || character == "Γ" || character == "⅂" || character == "⅃" || character == "L")
                    {
                        tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), SelectWall(character, new int[] {i, j}), "Wall");
                    }
                    else if (character == "o")
                    {
                        if(j == 0)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet[9], "DoorTop");
                            SpawnPoints[0] = new Vector2(20 * i + 20 * 0.5f, 20 * 0.5f) + Position;
                        }
                        else if(i == Size.X - 1)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet[9], "DoorRight");
                            SpawnPoints[1] = new Vector2(20 * i + 20 * 0.5f, 20 * j + 20 * 0.5f) + Position;
                        }
                        else if(j == Size.Y - 1)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet[9], "DoorBottom");
                            SpawnPoints[2] = new Vector2(20 * i + 20 * 0.5f, 20 * j + 20 * 0.5f) + Position;
                        }
                        else if(i == 0)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet[9], "DoorLeft");
                            SpawnPoints[3] = new Vector2(20 * 0.5f, 20 * j + 20 * 0.5f) + Position;
                        }
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
            if (character == "Γ")
            {
                return _tileSet[0];
            }
            else if (character == "⅂")
            {
                return _tileSet[1];
            }
            else if (character == "⅃")
            {
                return _tileSet[2];
            }
            else if (character == "L")
            {
                return _tileSet[3];
            }
            else if (character == "-")
            {
                return SelectHorizontalWall(position);
            }
            else if (character == "|")
            {
                return SelectVerticalWall(position);
            }
            else if (character == ".")
            {
                return SelectCorner(position);
            }
            return _tileSet[9];
        }
        private Texture2D SelectHorizontalWall(int[] position)
        {
            terrain = new string[] { " " };
            //cotés
            if (position[1] == 0)
            {
                return _tileSet[4];
            }
            else if (position[1] == Size.Y - 1)
            {
                return _tileSet[6];
            }
            //pas coté
            else if (terrain.Contains(_room[position[1] + 1][position[0]].ToString()))
            {
                return _tileSet[4];
            }
            else if(terrain.Contains(_room[position[1] - 1][position[0]].ToString()))
            {
                return _tileSet[6];
            }
            Console.WriteLine((position[0], position[1]));
            throw new Exception("Une tile mur horizontal n'est pas définie");
        }
        private Texture2D SelectVerticalWall(int[] position)
        {
            terrain = new string[] { " " };
            //cotés
            if (position[0] == 0)
            {
                return _tileSet[7];
            }
            else if (position[0] == Size.X - 1)
            {
                return _tileSet[5];
            }
            //pas coté
            else if (terrain.Contains(_room[position[1]][position[0] + 1].ToString()))
            {
                return _tileSet[7];
            }
            else if (terrain.Contains(_room[position[1]][position[0] - 1].ToString()))
            {
                return _tileSet[5];
            }
            Console.WriteLine((position[0], position[1]));
            throw new Exception("Une tile mur vertical n'est pas définie");
        }
        private Texture2D SelectCorner(int[] position)
        {
            //coins
            walls = new string[] { "-", "|", "Γ", "⅂", "⅃", "L", "o" };
            if (position[0] == 0 && position[1] == 0)
            {
                return _tileSet[12];
            }
            else if (position[0] == Size.X - 1 && position[1] == 0)
            {
                return _tileSet[13];
            }
            else if (position[0] == Size.X - 1 && position[1] == Size.Y - 1)
            {
                return _tileSet[10];
            }
            else if (position[0] == 0 && position[1] == Size.Y - 1)
            {
                return _tileSet[11];
            }
            else if(position[0] == 0)
            {
                if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileSet[12];
                }
                else if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileSet[11];
                }
            }
            //cotés
            else if (position[1] == 0)
            {
                if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileSet[12];
                }
                else if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileSet[13];
                }
            }
            else if (position[0] == Size.X - 1)
            {
                if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileSet[13];
                }
                else if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileSet[10];
                }
            }
            else if (position[1] == Size.Y - 1)
            {
                if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileSet[11];
                }
                else if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileSet[10];
                }
            }
            //ni coin ni coté
            else if(walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
            {
                return _tileSet[11];
            }
            else if(walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
            {
                return _tileSet[12];
            }
            else if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
            {
                return _tileSet[13];
            }
            else if(walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
            {
                return _tileSet[10];
            }
            Console.WriteLine((position[0], position[1]));
            throw new Exception("Une tile corner n'est pas définie");
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
