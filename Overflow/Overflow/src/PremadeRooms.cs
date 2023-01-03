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
        top right bottom left
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

         .-.
         | |  Γ ⅂
         .-.  L ⅃
         */

        private static Random random = new Random();

        public static Room Room(bool[] doors, Texture2D[] tileSet)
        {
            if (doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    ".-------------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "o             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".-------------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    ".-------------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".------o------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    ".-------------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "o             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".------o------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    ".-------------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             o",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".-------------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    ".-------------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "o             o",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".-------------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    ".-------------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             o",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".------o------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    ".-------------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "o             o",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".------o------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "ee.----o---.eee",
                    "e.⅃        L.ee",
                    ".⅃          L.e",
                    "|            |e",
                    "|           Γ.e",
                    "|           |ee",
                    "|           L-.",
                    "|             |",
                    "|             |",
                    "|            Γ.",
                    ".-⅂         Γ.e",
                    "ee.⅂        |ee",
                    "eee.--------.ee"},
                    new string[] {
                    "eeeeee.--o-----.eee",
                    "eeeee.⅃        L.ee",
                    "e.---⅃          L.e",
                    "e|               |e",
                    "e.⅂             Γ.e",
                    "ee.⅂            |ee",
                    ".--⅃            L-.",
                    "|                 |",
                    "|                 |",
                    "|                Γ.",
                    ".-⅂             Γ.e",
                    "ee.⅂  Γ---⅂     |ee",
                    "eee.--.eee.-----.ee"}
                };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    ".------o------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "o             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".-------------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    ".------o------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".------o------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    ".------o------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "o             |",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".------o------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    ".------o------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             o",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".-------------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    ".------o------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "o             o",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".-------------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    ".------o------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "|             o",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".------o------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    ".------o------.",
                    "|             |",
                    "|             |",
                    "|             |",
                    "o             o",
                    "|             |",
                    "|             |",
                    "|             |",
                    ".------o------."
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet);
            }

            throw new Exception("Il n'existe pas de salle préfaite pour cette combinaison de porte");
        }
    }
}
