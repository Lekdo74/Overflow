using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public static class PremadeRooms
    {/*
        left top bottom right
        0000
        0001
        0010
        0011
        0100
        0101
        0110
        0111
        1000
        1001
        1010
        1011
        1100
        1101
        1110
        1111
        */
        /*
         Γ-⅂
         l |
         L_⅃
         */

        private static Random random = new Random();

        public static Room Room(bool[] doors, Texture2D[] tileSet)
        {
            if(doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "aΓ--o---⅂",
                    "Γ       |",
                    "l       |",
                    "l       |",
                    "l       |",
                    "l       |",
                    "L_______⅃"},
                    new string[] {
                    "Γ---o---⅂",
                    "l       |",
                    "l       |",
                    "L_______⅃"}
                };
                return new Room(rooms[random.Next(0, rooms.GetLength(0))], tileSet);
            }
            return new Room(new string[] { "a", "b" }, tileSet);
        }
    }
}
