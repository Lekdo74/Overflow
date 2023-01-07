using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Overflow.src
{
    public class Room
    {
        private string[] _room;
        private Vector2 _size;
        private bool[] _doors;
        private int _roomType;

        private Vector2 _spawnPoint;
        private Vector2[] _spawnPoints; // doors
        private List<Vector2> _spawnPointsEnemies;
        private List<Rectangle> _obstacles;
        private Vector2 _position;

        private Tile[,] _tiles;
        private TileSet _tileSet;

        private float xMin;
        private float yMin;
        private float xMax;
        private float yMax;

        string[] terrain;
        string[] walls;

        private List<Enemy> _enemies;

        public Room(string[] room, TileSet tileSet, bool[] doors, int roomType)
        {
            _room = room;
            Size = new Vector2(room[0].Length, room.Length);
            Doors = doors;
            _roomType = roomType;

            SpawnPoints = new Vector2[4];
            Obstacles = new List<Rectangle>();
            _tileSet = tileSet;
            Position = new Vector2((Settings.nativeWidthResolution - Size.X * _tileSet.TileSize) / 2, (Settings.nativeHeightResolution - Size.Y * _tileSet.TileSize) / 2);
            Position = CalculateStartPosition();

            xMin = Position.X;
            yMin = Position.Y;
            xMax = Position.X + (Size.X) * _tileSet.TileSize;
            yMax = Position.Y + (Size.Y) * _tileSet.TileSize;

            Tiles = BuildRoom();

            _enemies = new List<Enemy>();
            if(_roomType == 2)
            {
                _enemies = CreateEnemies();
            }
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

        public Vector2 SpawnPoint
        {
            get { return _spawnPoint; }
            set { _spawnPoint = value; }
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

        public Tile[,] Tiles
        {
            get { return _tiles; }
            set { _tiles = value; }
        }

        public List<Enemy> Enemies
        {
            get { return _enemies; }
            set { _enemies = value; }
        }

        private Vector2 CalculateStartPosition()
        {
            float x = 0;
            float y = 0;
            if (Doors[1])
            {
                x = Settings.nativeWidthResolution - Size.X * _tileSet.TileSize;
            }
            if (Doors[2])
            {
                y = Settings.nativeHeightResolution - Size.Y * _tileSet.TileSize;
            }
            if(Doors[1] && Doors[3])
            {
                x = (Settings.nativeWidthResolution - Size.X * _tileSet.TileSize) / 2;
            }
            if(Doors[1] && Doors[2])
            {
                y = (Settings.nativeHeightResolution - Size.Y * _tileSet.TileSize) / 2;
            }
            return new Vector2(x, y);
        }

        private Tile[,] BuildRoom()
        {
            Tile[,] tiles = new Tile[(int)Size.X, (int)Size.Y];
            for (int j = 0; j < Size.Y; j++)
            {
                for (int i = 0; i < Size.X; i++)
                {
                    string character = _room[j][i].ToString();
                    if (character == " ")
                    {
                        if(j == 0)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet.Terrain, "Grass", i, j);
                        }
                        else if (_room[j - 1][i].ToString() == "L")
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet.TerrainWithWallBorderLeft, "Grass", i, j);
                        }
                        else if (_room[j - 1][i].ToString() == "⅃")
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet.TerrainWithWallBorderRight, "Grass", i, j);
                        }
                        else if (PremadeRooms.walls.Contains(_room[j - 1][i].ToString()))
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet.TerrainWithWall, "Grass", i, j);
                        }
                        else
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet.Terrain, "Grass", i, j);
                        }
                    }
                    else if(character == "x")
                    {
                        tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet.Terrain, "Grass", i, j);
                        SpawnPoint = new Vector2(20 * i, 20 * j) + Position;
                    }
                    else if(character == "|" || character == "-" || character == "." || character == "Γ" || character == "⅂" || character == "⅃" || character == "L")
                    {
                        tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), SelectWall(character, new int[] {i, j}), "Wall", i, j);
                    }
                    else if (character == "o")
                    {
                        if(j == 0)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet.TopDoor, "DoorTop", i, j);
                            SpawnPoints[0] = new Vector2(20 * i + 20 * 0.5f, 20 * 1f) + Position;
                        }
                        else if(i == Size.X - 1)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet.RightDoor, "DoorRight", i, j);
                            SpawnPoints[1] = new Vector2(20 * i + 20 * 0f, 20 * j + 20 * 0.5f) + Position;
                        }
                        else if(j == Size.Y - 1)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet.BottomDoor, "DoorBottom", i, j);
                            SpawnPoints[2] = new Vector2(20 * i + 20 * 0.5f, 20 * j + 20 * 0f) + Position;
                        }
                        else if(i == 0)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet.LeftDoor, "DoorLeft", i, j);
                            SpawnPoints[3] = new Vector2(20 * 1f, 20 * j + 20 * 0.5f) + Position;
                        }
                    }
                }
            }

            _spawnPointsEnemies = new List<Vector2>();
            foreach (Tile tile in tiles)
            {
                if(tile != null)
                {
                    tile.Position = new Vector2(tile.Position.X + Position.X, tile.Position.Y + Position.Y);

                    if (tile.Type == "Wall")
                    {
                        Obstacles.Add(new Rectangle((int)tile.Position.X, (int)tile.Position.Y, tile.Texture.Width, tile.Texture.Height));
                    }

                    if (tile.Type == "Grass")
                    {
                        bool spawnPointEnemy = true;
                        for(int i = 0; i < Doors.Length; i++)
                        {
                            if(Doors[i] != false)
                            {
                                if (Vector2.Distance(SpawnPoints[i], tile.Position + new Vector2(tile.Texture.Width / 2, tile.Texture.Height / 2)) < 90)
                                {
                                    spawnPointEnemy = false;
                                    break;
                                }
                            }
                        }
                        if (spawnPointEnemy)
                        {
                            _spawnPointsEnemies.Add(tile.Position);
                        }
                    }
                }
            }

            return tiles;
        }

        private Texture2D SelectWall(string character, int[] position)
        {
            if (character == "Γ")
            {
                return _tileSet.TopLeftWall;
            }
            else if (character == "⅂")
            {
                return _tileSet.TopRightWall;
            }
            else if (character == "⅃")
            {
                return _tileSet.BottomRightWall;
            }
            else if (character == "L")
            {
                return _tileSet.BottomLeftWall;
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
            return null;
        }
        private Texture2D SelectHorizontalWall(int[] position)
        {
            terrain = new string[] { " " };
            //cotés
            if (position[1] == 0)
            {
                return _tileSet.TopWall;
            }
            else if (position[1] == Size.Y - 1)
            {
                return _tileSet.BottomWall;
            }
            //pas coté
            else if (terrain.Contains(_room[position[1] + 1][position[0]].ToString()))
            {
                return _tileSet.TopWall;
            }
            else if(terrain.Contains(_room[position[1] - 1][position[0]].ToString()))
            {
                return _tileSet.BottomWall;
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
                return _tileSet.LeftWall;
            }
            else if (position[0] == Size.X - 1)
            {
                return _tileSet.RightWall;
            }
            //pas coté
            else if (terrain.Contains(_room[position[1]][position[0] + 1].ToString()))
            {
                return _tileSet.LeftWall;
            }
            else if (terrain.Contains(_room[position[1]][position[0] - 1].ToString()))
            {
                return _tileSet.RightWall;
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
                return _tileSet.BottomRightCorner;
            }
            else if (position[0] == Size.X - 1 && position[1] == 0)
            {
                return _tileSet.BottomLeftCorner;
            }
            else if (position[0] == Size.X - 1 && position[1] == Size.Y - 1)
            {
                return _tileSet.TopLeftCorner;
            }
            else if (position[0] == 0 && position[1] == Size.Y - 1)
            {
                return _tileSet.TopRightCorner;
            }
            else if(position[0] == 0)
            {
                if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileSet.BottomRightCorner;
                }
                else if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileSet.TopRightCorner;
                }
            }
            //cotés
            else if (position[1] == 0)
            {
                if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileSet.BottomRightCorner;
                }
                else if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileSet.BottomLeftCorner;
                }
            }
            else if (position[0] == Size.X - 1)
            {
                if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileSet.BottomLeftCorner;
                }
                else if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileSet.TopLeftCorner;
                }
            }
            else if (position[1] == Size.Y - 1)
            {
                if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileSet.TopRightCorner;
                }
                else if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileSet.TopLeftCorner;
                }
            }
            //ni coin ni coté
            else if(walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
            {
                return _tileSet.TopRightCorner;
            }
            else if(walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
            {
                return _tileSet.BottomRightCorner;
            }
            else if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
            {
                return _tileSet.BottomLeftCorner;
            }
            else if(walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
            {
                return _tileSet.TopLeftCorner;
            }
            Console.WriteLine((position[0], position[1]));
            throw new Exception("Une tile corner n'est pas définie");
        }

        private List<Enemy> CreateEnemies()
        {
            Random random = new Random();
            List<Enemy> enemies = new List<Enemy>();
            for(int i = 0; i < 3; i++)
            {
                int currentEnemySpawnPoint = random.Next(0, _spawnPointsEnemies.Count - 1);
                enemies.Add(Enemy.CreateSeeker(_spawnPointsEnemies[currentEnemySpawnPoint], this));
                _spawnPointsEnemies.RemoveAt(currentEnemySpawnPoint);
            }
            return enemies;
        }

        public Tile GetTile(Vector2 position)
        {
            return Tiles[((int)position.X - (int)Position.X + Player.Texture.Width / 2) / _tileSet.TileSize, ((int)position.Y - (int)Position.Y + Player.Texture.Height / 2) / _tileSet.TileSize];
        }

        public bool InsideRoom(Vector2 position)
        {
            if (position.Y < yMin || position.X > xMax || position.Y > yMax || position.X < xMin)
            {
                return false;
            }
            return true;
        }

        public List<Tile> GetNeighbours(Tile tile)
        {
            List<Tile> neighbours = new List<Tile>();

            for(int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if((x == 0 && y == 0) || tile == null)
                    {
                        continue;
                    }
                    int currentX = tile.MapX + x;
                    int currentY = tile.MapY + y;

                    if (currentX >= 0 && currentX < Size.X && currentY >= 0 && currentY < Size.Y && _tiles[currentX, currentY] != null)
                    {
                        neighbours.Add(Tiles[currentX, currentY]);
                    }
                }
            }
            return neighbours;
        }

        public void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            foreach (Tile tile in Tiles)
            {
                if(tile != null)
                {
                    tile.Draw(gameTime, spritebatch, Color.White);
                }
            }
            /* Position possible des ennemies
            foreach(Vector2 position in _spawnPointsEnemies)
            {
                spritebatch.Draw(_tileSet[9], position, Color.Red);
            }
            */

            foreach(Enemy enemy in _enemies)
            {
                enemy.Draw(gameTime, spritebatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(Enemy enemy in _enemies)
            {
                enemy.Update(gameTime);
            }
        }
    }
}
