using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Overflow.src
{
    public class Map : Component
    {
        private int _roomNb;
        private Room[,] _rooms;
        private int[] _currentRoom;

        private Texture2D[] _tileSet;
        private int _mapSizeX;
        private int _mapSizeY;

        public Map(int roomNb, Texture2D[] tileSet)
        {
            _roomNb = roomNb;
            _tileSet = tileSet;
            _rooms = GenerateMap();
        }

        public int RoomNb
        {
            get
            {
                return _roomNb;
            }
        }

        public Room[,] Rooms
        {
            get
            {
                return _rooms;
            }
            set
            {
                _rooms = value;
            }
        }

        public int[] CurrentRoom
        {
            get
            {
                return _currentRoom;
            }
            set
            {
                _currentRoom = value;
            }
        }

        private Room[,] GenerateMap()
        {
            Random random = new Random();
            List<int[]> roomCoordinates = new List<int[]>();
            roomCoordinates.Add(new int[] { 0, 0 });
            int iCurrentRoom = 0;

            while (roomCoordinates.Count < _roomNb)
            {
                while (RoomsAvailable(roomCoordinates, iCurrentRoom).Length == 0)
                {
                    iCurrentRoom = random.Next(0, roomCoordinates.Count);
                }
                int[][] roomsAvailable = RoomsAvailable(roomCoordinates, iCurrentRoom);
                roomCoordinates.Add(roomsAvailable[random.Next(0, roomsAvailable.Length)]);
                iCurrentRoom = roomCoordinates.Count - 1;
            }

            int xMin = roomCoordinates[0][0];
            int yMin = roomCoordinates[0][1];
            int xMax = roomCoordinates[0][0];
            int yMax = roomCoordinates[0][1];

            foreach (int[] coordinates in roomCoordinates)
            {
                if (coordinates[0] < xMin)
                {
                    xMin = coordinates[0];
                }
                else if (coordinates[0] > xMax)
                {
                    xMax = coordinates[0];
                }
                if (coordinates[1] < yMin)
                {
                    yMin = coordinates[1];
                }
                else if (coordinates[1] > yMax)
                {
                    yMax = coordinates[1];
                }
            }
            _mapSizeX = Math.Abs(xMax - xMin + 1);
            _mapSizeY = Math.Abs(yMax - yMin + 1);

            int[,] map = new int[_mapSizeX, _mapSizeY];
            foreach (int[] coordinates in roomCoordinates)
            {
                map[coordinates[0] - xMin, coordinates[1] - yMin] = 2;
            }
            map[roomCoordinates[0][0] - xMin, roomCoordinates[0][1] - yMin] = 1;
            CurrentRoom = new int[] { roomCoordinates[0][0] - xMin, roomCoordinates[0][1] - yMin };
            map[roomCoordinates[roomCoordinates.Count - 1][0] - xMin, roomCoordinates[roomCoordinates.Count - 1][1] - yMin] = 3;

            for (int j = 0; j < map.GetLength(1); j++)
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    Console.Write(map[i, j] + " ");
                }
                Console.WriteLine();
            }

            Room[,] rooms = new Room[_mapSizeX, _mapSizeY];
            foreach (int[] coordinates in roomCoordinates)
            {
                int x = coordinates[0] - xMin;
                int y = coordinates[1] - yMin;
                bool[] doors = new bool[4];

                if (x > 0)
                {
                    if (map[x - 1, y] != 0)
                    {
                        doors[3] = true;
                    }
                }
                if (x < _mapSizeX - 1)
                {
                    if (map[x + 1, y] != 0)
                    {
                        doors[1] = true;
                    }
                }
                if (y > 0)
                {
                    if (map[x, y - 1] != 0)
                    {
                        doors[0] = true;
                    }
                }
                if (y < _mapSizeY - 1)
                {
                    if (map[x, y + 1] != 0)
                    {
                        doors[2] = true;
                    }
                }

                rooms[x, y] = new Room(new Vector2(13, 11), _tileSet, doors);
            }
            Console.WriteLine((CurrentRoom[0], CurrentRoom[1]));

            return rooms;
        }
        private int[][] RoomsAvailable(List<int[]> roomCoordinates, int iCurrentRoom)
        {
            List<int[]> roomsAvailable = new List<int[]>();
            int[] top = new int[] { roomCoordinates[iCurrentRoom][0], roomCoordinates[iCurrentRoom][1] - 1 };
            int[] right = new int[] { roomCoordinates[iCurrentRoom][0] + 1, roomCoordinates[iCurrentRoom][1] };
            int[] bottom = new int[] { roomCoordinates[iCurrentRoom][0], roomCoordinates[iCurrentRoom][1] + 1 };
            int[] left = new int[] { roomCoordinates[iCurrentRoom][0] - 1, roomCoordinates[iCurrentRoom][1] };
            if (!Contains(roomCoordinates, top))
            {
                roomsAvailable.Add(top);
            }
            if (!Contains(roomCoordinates, right))
            {
                roomsAvailable.Add(right);
            }
            if (!Contains(roomCoordinates, bottom))
            {
                roomsAvailable.Add(bottom);
            }
            if (!Contains(roomCoordinates, left))
            {
                roomsAvailable.Add(left);
            }
            return roomsAvailable.ToArray();
        }

        private bool Contains(List<int[]> roomCoordinates, int[] coordinates)
        {
            foreach (int[] c in roomCoordinates)
            {
                if (c[0] == coordinates[0] && c[1] == coordinates[1])
                {
                    return true;
                }
            }
            return false;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spritebatch)
        {
            _rooms[CurrentRoom[0], CurrentRoom[1]].Draw(gameTime, spritebatch);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
