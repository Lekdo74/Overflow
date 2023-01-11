using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Overflow.src
{
    public class Room
    {
        private static Random random = new Random();

        private static List<string> _doorsTypes = new List<string> { "DoorTop", "DoorRight", "DoorBottom", "DoorLeft" };

        private string[] _room;
        private Vector2 _size;
        private bool[] _doors;
        private int _roomType;
        private int[] _enemyNb;

        private Vector2 _spawnPoint; // start position if the player spawn in the room
        private Vector2[] _spawnPoints; // doors
        private List<Vector2> _spawnPointsEnemies;
        private List<Rectangle> _obstacles;
        private Vector2 _position;

        private Tile[,] _tiles;
        private TileSet _tileset;

        private EnemySet _enemyset;

        private Song _backgroundMusic;

        private float xMin;
        private float yMin;
        private float xMax;
        private float yMax;

        string[] terrain;
        string[] walls;

        private List<Enemy> _enemies;
        private List<Projectile> _projectiles;

        public Room(string[] room, bool[] doors, int roomType, int[] enemyNb, TileSet tileSet, EnemySet enemyset, Song backgroundMusic)
        {
            _room = room;
            Size = new Vector2(room[0].Length, room.Length);
            Doors = doors;
            _roomType = roomType;
            _enemyNb = enemyNb;

            SpawnPoints = new Vector2[4];
            Obstacles = new List<Rectangle>();

            _tileset = tileSet;

            _enemyset = enemyset;

            if (roomType == 3)
                _backgroundMusic = Sound.boss;
            else
                _backgroundMusic = backgroundMusic;

            Position = new Vector2((Settings.nativeWidthResolution - Size.X * _tileset.TileSize) / 2, (Settings.nativeHeightResolution - Size.Y * _tileset.TileSize) / 2);
            Position = CalculateStartPosition();

            xMin = Position.X;
            yMin = Position.Y;
            xMax = Position.X + (Size.X) * _tileset.TileSize;
            yMax = Position.Y + (Size.Y) * _tileset.TileSize;

            Tiles = BuildRoom();

            _enemies = new List<Enemy>();
            if(_roomType == 2)
            {
                _enemies = CreateEnemies();
            }
            else if(_roomType == 3)
            {
                _enemies.Add(CreateBoss());
            }

            _projectiles = new List<Projectile>();
        }

        public static List<string> DoorsTypes
        {
            get { return _doorsTypes; }
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

        public int RoomType
        {
            get { return _roomType; }
            set { _roomType = value; }
        }

        public Song BackgroundMusic
        {
            get { return _backgroundMusic; }
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

        public List<Projectile> Projectiles
        {
            get { return _projectiles; }
            set { _projectiles = value; }
        }

        private Vector2 CalculateStartPosition()
        {
            float x = 0;
            float y = 0;
            if (Doors[1])
            {
                x = Settings.nativeWidthResolution - Size.X * _tileset.TileSize;
            }
            if (Doors[2])
            {
                y = Settings.nativeHeightResolution - Size.Y * _tileset.TileSize;
            }
            if(Doors[1] && Doors[3])
            {
                x = (Settings.nativeWidthResolution - Size.X * _tileset.TileSize) / 2;
            }
            if(Doors[1] && Doors[2])
            {
                y = (Settings.nativeHeightResolution - Size.Y * _tileset.TileSize) / 2;
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
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileset.Terrain, "Grass", i, j);
                        }
                        else if (_room[j - 1][i].ToString() == "L")
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileset.TerrainWithWallBorderLeft, "Grass", i, j);
                        }
                        else if (_room[j - 1][i].ToString() == "⅃")
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileset.TerrainWithWallBorderRight, "Grass", i, j);
                        }
                        else if (PremadeRooms.walls.Contains(_room[j - 1][i].ToString()))
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileset.TerrainWithWall, "Grass", i, j);
                        }
                        else
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileset.Terrain, "Grass", i, j);
                        }
                    }
                    else if (character == "F")
                    {
                        tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileset.DarkTerrain, "Grass", i, j);
                    }
                    else if(character == "x")
                    {
                        tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileset.Terrain, "Grass", i, j);
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
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileset.TopDoor, "DoorTop", i, j);
                            SpawnPoints[0] = new Vector2(20 * i + 20 * 0.5f, 20 * 1f) + Position;
                        }
                        else if(i == Size.X - 1)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileset.RightDoor, "DoorRight", i, j);
                            SpawnPoints[1] = new Vector2(20 * i + 20 * 0f, 20 * j + 20 * 0.5f) + Position;
                        }
                        else if(j == Size.Y - 1)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileset.BottomDoor, "DoorBottom", i, j);
                            SpawnPoints[2] = new Vector2(20 * i + 20 * 0.5f, 20 * j + 20 * 0f) + Position;
                        }
                        else if(i == 0)
                        {
                            tiles[i, j] = new Tile(new Vector2(20 * i, 20 * j), _tileset.LeftDoor, "DoorLeft", i, j);
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
                return _tileset.TopLeftWall;
            }
            else if (character == "⅂")
            {
                return _tileset.TopRightWall;
            }
            else if (character == "⅃")
            {
                return _tileset.BottomRightWall;
            }
            else if (character == "L")
            {
                return _tileset.BottomLeftWall;
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
                return _tileset.TopWall;
            }
            else if (position[1] == Size.Y - 1)
            {
                return _tileset.BottomWall;
            }
            //pas coté
            else if (terrain.Contains(_room[position[1] + 1][position[0]].ToString()))
            {
                return _tileset.TopWall;
            }
            else if(terrain.Contains(_room[position[1] - 1][position[0]].ToString()))
            {
                return _tileset.BottomWall;
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
                return _tileset.LeftWall;
            }
            else if (position[0] == Size.X - 1)
            {
                return _tileset.RightWall;
            }
            //pas coté
            else if (terrain.Contains(_room[position[1]][position[0] + 1].ToString()))
            {
                return _tileset.LeftWall;
            }
            else if (terrain.Contains(_room[position[1]][position[0] - 1].ToString()))
            {
                return _tileset.RightWall;
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
                return _tileset.BottomRightCorner;
            }
            else if (position[0] == Size.X - 1 && position[1] == 0)
            {
                return _tileset.BottomLeftCorner;
            }
            else if (position[0] == Size.X - 1 && position[1] == Size.Y - 1)
            {
                return _tileset.TopLeftCorner;
            }
            else if (position[0] == 0 && position[1] == Size.Y - 1)
            {
                return _tileset.TopRightCorner;
            }
            else if(position[0] == 0)
            {
                if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileset.BottomRightCorner;
                }
                else if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileset.TopRightCorner;
                }
            }
            //cotés
            else if (position[1] == 0)
            {
                if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileset.BottomRightCorner;
                }
                else if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileset.BottomLeftCorner;
                }
            }
            else if (position[0] == Size.X - 1)
            {
                if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileset.BottomLeftCorner;
                }
                else if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileset.TopLeftCorner;
                }
            }
            else if (position[1] == Size.Y - 1)
            {
                if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
                {
                    return _tileset.TopRightCorner;
                }
                else if (walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
                {
                    return _tileset.TopLeftCorner;
                }
            }
            //ni coin ni coté
            else if(walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
            {
                return _tileset.TopRightCorner;
            }
            else if(walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] + 1].ToString()))
            {
                return _tileset.BottomRightCorner;
            }
            else if (walls.Contains(_room[position[1] + 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
            {
                return _tileset.BottomLeftCorner;
            }
            else if(walls.Contains(_room[position[1] - 1][position[0]].ToString()) && walls.Contains(_room[position[1]][position[0] - 1].ToString()))
            {
                return _tileset.TopLeftCorner;
            }
            Console.WriteLine((position[0], position[1]));
            throw new Exception("Une tile corner n'est pas définie");
        }

        private List<Enemy> CreateEnemies()
        {
            List<Enemy> enemies = new List<Enemy>();
            for(int i = 0; i < random.Next(_enemyNb[0], _enemyNb[1]); i++)
            {
                int currentEnemySpawnPoint = random.Next(0, _spawnPointsEnemies.Count - 1);

                int randomEnemy = random.Next(0, 2);
                if(randomEnemy == 0)
                {
                    enemies.Add(Enemy.CreateSeeker(_enemyset.Seeker, _spawnPointsEnemies[currentEnemySpawnPoint], this));
                }
                else if(randomEnemy == 1)
                {
                    enemies.Add(Enemy.CreateArcher(_enemyset.Archer, _spawnPointsEnemies[currentEnemySpawnPoint], this));
                }
                
                _spawnPointsEnemies.RemoveAt(currentEnemySpawnPoint);
            }
            return enemies;
        }

        private Enemy CreateBoss()
        {
            return Enemy.CreateBoss(SpawnPoint, this);
        }

        public Tile GetTile(Vector2 position)
        {
            return Tiles[((int)position.X - (int)Position.X) / _tileset.TileSize, ((int)position.Y - (int)Position.Y) / _tileset.TileSize];
        }

        public Tile GetPlayerTile()
        {
            return Tiles[((int)Player.Position.X - (int)Position.X + Player.Texture.Width / 2) / _tileset.TileSize, ((int)Player.Position.Y - (int)Position.Y + Player.Texture.Height / 2) / _tileset.TileSize];
        }

        public Tile GetRandomTerrainTileInRoom()
        {
            List<Tile> availableTiles = new List<Tile>();
            foreach(Tile tile in Tiles)
            {
                if (tile != null && tile.Type == "Grass")
                    availableTiles.Add(tile);
            }
            if (availableTiles.Count == 0)
                return null;
            return availableTiles[random.Next(0, availableTiles.Count)];
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

        public bool LineOfView(Room room, Vector2 startPosition, Vector2 targetPosition)
        {
            Vector2 direction = Vector2.Normalize(targetPosition - startPosition);
            Vector2 currentLocation = startPosition;
            Tile currentTile;
            int step = _tileset.TileSize / 10;

            while (room.InsideRoom(currentLocation))
            {
                if(direction.X < 0)
                {
                    if(currentLocation.X < targetPosition.X)
                    {
                        return true;
                    }
                }
                else if(direction.X > 0)
                {
                    if(currentLocation.X > targetPosition.X)
                    {
                        return true;
                    }
                }
                if (direction.Y < 0)
                {
                    if (currentLocation.Y < targetPosition.Y)
                    {
                        return true;
                    }
                }
                else if (direction.Y > 0)
                {
                    if (currentLocation.Y > targetPosition.Y)
                    {
                        return true;
                    }
                }

                currentLocation += direction * step;

                currentTile = GetTile(currentLocation);
                if (currentTile == null || currentTile.Type == "Wall")
                {
                    return false;
                }
            }
            return true;
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

            foreach (Projectile projectile in Projectiles)
            {
                projectile.Draw(spritebatch);
            }
        }

        public void Update(GameTime gameTime, Main game)
        {
            foreach(Enemy enemy in _enemies)
            {
                enemy.Update(gameTime);
            }

            List<Projectile> projectilesOutOfRoom = new List<Projectile>();
            foreach(Projectile projectile in Projectiles)
            {
                projectile.Update(gameTime);
                if (projectile.IsExpired)
                    projectilesOutOfRoom.Add(projectile);
            }
            foreach(Projectile projectile in projectilesOutOfRoom)
            {
                Projectiles.Remove(projectile);
            }

            List<Enemy> enemiesToDelete = new List<Enemy>(); ;
            if (PlayerSlash.RemainingAnimationTime > 0)
            {
                Rectangle slashRectangle = PlayerSlash.Rectangle;
                foreach (Enemy enemy in Enemies)
                {
                    if (enemy.AttackNumber != Player.AttackNumber && enemy.Rectangle.Intersects(slashRectangle))
                    {
                        if (enemy.Health > 1)
                        {
                            enemy.AttackNumber = Player.AttackNumber;
                            enemy.Health -= 1;
                            enemy.KnockbackDirection = Vector2.Normalize(enemy.CenteredPosition - Player.CenteredPosition);
                            enemy.KnockbackTimeRemaining = enemy.KnockbackDuration;
                        }
                        else
                        {
                            enemiesToDelete.Add(enemy);
                            if (enemy.Type == "Boss")
                            {
                                game.LoadEndScreen();
                            }
                        }
                    }
                }
            }
            foreach(Enemy enemy in enemiesToDelete)
            {
                Enemies.Remove(enemy);
            }
        }
    }
}
