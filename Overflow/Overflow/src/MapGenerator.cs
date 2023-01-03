using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public static class MapGenerator
    {
        private static Random random = new Random();

        public static Rectangle[] GenerateMap(int nbRooms, Vector2 size)
        {
            Rectangle[] rooms = new Rectangle[nbRooms];
            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i] = MakeRoom(size);
                Console.WriteLine(rooms[i]);
            }

            return rooms;
        }

        private static Rectangle MakeRoom(Vector2 size)
        {
            return new Rectangle(0, 0, random.Next((int)size.X, (int)size.Y), random.Next((int)size.X, (int)size.Y));
        }
    }
}
