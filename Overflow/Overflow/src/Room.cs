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
        private int _roomType;

        private Vector2 _spawnPoint;
        private Vector2[] _spawnPoints; // doors
        private List<Vector2> _spawnPointsEnemies;
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

        private List<Enemy> _enemies;

        public Room(string[] room, Texture2D[] tileSet, bool[] doors, int roomType)
        {
            _room = room;
            Size = new Vector2(room[0].Length, room.Length);
            Doors = doors;
            _roomType = roomType;

            SpawnPoints = new Vector2[4];
            Obstacles = new List<Rectangle>();
            _tileSet = tileSet;
            Position = new Vector2((Settings.nativeWidthResolution - Size.X * _tileSet[0].Width) / 2, (Settings.nativeHeightResolution - Size.Y * _tileSet[0].Height) / 2);
            Position = CalculateStartPosition();

            xMin = Position.X;
            yMin = Position.Y;
            xMax = Position.X + (Size.X) * _tileSet[0].Width;
            yMax = Position.Y + (Size.Y) * _tileSet[0].Height;

            _tiles = BuildRoom();

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

        private Vector2 CalculateStartPosition()
        {
            float x = 0;
            float y = 0;
            if (Doors[1])
            {
                x = Settings.nativeWidthResolution - Size.X * _tileSet[0].Width;
            }
            if (Doors[2])
            {
                y = Settings.nativeHeightResolution - Size.Y * _tileSet[0].Height;
            }
            if(Doors[1] && Doors[3])
            {
                x = (Settings.nativeWidthResolution - Size.X * _tileSet[0].Width) / 2;
            }
            if(Doors[1] && Doors[2])
            {
                y = (Settings.nativeHeightResolution - Size.Y * _tileSet[0].Height) / 2;
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
                        tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet[8], "Grass");
                    }
                    else if(character == "x")
                    {
                        tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileSet[8], "Grass");
                        SpawnPoint = new Vector2(20 * i, 20 * j) + Position;
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
            return _tiles[((int)position.X - (int)Position.X + Player.Texture.Width / 2) / _tileSet[0].Width, ((int)position.Y - (int)Position.Y + Player.Texture.Height / 2) / _tileSet[0].Height];
        }

        public bool InsideRoom(Vector2 position)
        {
            if (position.Y < yMin || position.X > xMax || position.Y > yMax || position.X < xMin)
            {
                return false;
            }
            return true;
        }
        public bool InsideRoom()
        {
            if (Player.Position.Y < yMin || Player.Position.X + Player.Texture.Width > xMax || Player.Position.Y + Player.Texture.Height > yMax || Player.Position.X < xMin)
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

        public override void Update(GameTime gameTime)
        {
            foreach(Enemy enemy in _enemies)
            {
                enemy.Update(gameTime);
            }
        }
    }
}
