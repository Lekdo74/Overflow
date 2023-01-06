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
         | |  Γ ⌉
         .-.  L ⅃
          ⋏
         < >
         */

        private static Random random = new Random();

        public static Room Room(bool[] doors, Texture2D[] tileSet, int roomType)
        {
            if (doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eeee.---.eeeeee",
                    ".---⅃   L-.eeee",
                    "|         L.eee",
                    ".⅂         L-.e",
                    ".⅃           L.",
                    "o      x      |",
                    "|             |",
                    "|             |",
                    ".⅂   Γ-⅂      |",
                    "e.⅂  |e.⅂  Γ--.",
                    "ee.--.ee.--.eee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeee.---.eeeeee",
                    ".---⅃   L.e.-.e",
                    "|        L-⅃ |e",
                    ".⅂           L.",
                    "e|            |",
                    ".⅃      x     |",
                    "|             |",
                    "|             |",
                    ".⅂ Γ⅂         |",
                    "e.-..⅂     Γ--.",
                    "eeeee.-o---.eee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eeee.---.eee.--.eeee",
                    ".---⅃   L.e.⅃  |eeee",
                    "|        L-⅃   L.eee",
                    ".⅂              L-.e",
                    ".⅃                L.",
                    "|                  |",
                    "o         x        |",
                    "|                  |",
                    "|                  |",
                    "|                 Γ.",
                    ".⅂ Γ⅂            Γ.e",
                    "e.-..---⅂  Γ-⅂ Γ-.ee",
                    "eeeeeeee.-o.e.-.eeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeee.-----.eeee",
                    ".---⅃     L-.ee",
                    "|           L.e",
                    ".⅂           L.",
                    "e|      x     |",
                    ".⅃            o",
                    "|             |",
                    "|             |",
                    ".⅂   Γ-⅂      |",
                    "e.⅂  |e.⅂  Γ--.",
                    "ee.--.ee.--.eee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eee.----.eee.--.eeee",
                    ".--⅃    L.e.⅃  L-.ee",
                    "|        L-⅃     L.e",
                    ".⅂                L.",
                    ".⅃                 |",
                    "|         x        |",
                    "o                  o",
                    "|                  |",
                    "|                  |",
                    ".⅂      Γ-⅂        |",
                    "e.⅂     |e.⅂    Γ--.",
                    "ee.-----.ee.----.eee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeee.-.eee.---.eeee",
                    ".---⅃ L.e.⅃   L-.ee",
                    "|      L-⅃      L.e",
                    ".⅂               L.",
                    "e|                |",
                    ".⅃                o",
                    "|         x       |",
                    "|                 |",
                    ".⅂ Γ⅂             |",
                    "e.-..⅂         Γ--.",
                    "eeeee|        Γ.eee",
                    "eeeee.---o----.eeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eeee.-.eeeeee.---.eeee",
                    ".---⅃ L.eeee.⅃   L-.ee",
                    "|      L.ee.⅃      L.e",
                    ".⅂      L..⅃        L.",
                    ".⅃       L⅃          |",
                    "|                    o",
                    "o                    |",
                    "|           x        |",
                    ".⅂ Γ⅂                |",
                    "e.-..⅂            Γ--.",
                    "eeeee|           Γ.eee",
                    "eeeee.-----o-----.eeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "ee.----o---.eee",
                    "e.⅃        L.ee",
                    ".⅃          L.e",
                    "|            |e",
                    ".⅂          Γ.e",
                    "e.⅂         |ee",
                    "e.⅃    x    L-.",
                    "e|            |",
                    "e|           Γ.",
                    "e.⅂         Γ.e",
                    "ee.⅂  Γ-⅂   |ee",
                    "eee.--.e.---.ee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eee.---o-.ee.---.",
                    "ee.⅃     L--⅃   |",
                    "ee|            Γ.",
                    "ee|           Γ.e",
                    "e.⅃    Γ⅂     |ee",
                    ".⅃    Γ..⅂    L.e",
                    "|    Γ.e.⅃     L.",
                    "o    |e.⅃       |",
                    "|    L-⅃    x   |",
                    "|               |",
                    ".⅂            Γ-.",
                    "e.⅂     Γ-⅂   |ee",
                    "ee.-----.e.---.ee"},
                    };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "ee.----o---.eee",
                    "e.⅃        L.ee",
                    ".⅃          L.e",
                    "|            |e",
                    "|           Γ.e",
                    "|           |ee",
                    "|      x    L-.",
                    "|             |",
                    "|             |",
                    "|            Γ.",
                    ".-⅂         Γ.e",
                    "ee.⅂        |ee",
                    "eee.---o----.ee"},
                    new string[] {
                    "eeeee.--o---.eee",
                    "eeee.⅃      L.ee",
                    ".---⅃        L.e",
                    "|             |e",
                    ".⅂           Γ.e",
                    "e.⅂          |ee",
                    ".-⅃          L-.",
                    "|       x      |",
                    "|              |",
                    "|             Γ.",
                    ".⅂           Γ.e",
                    "e.⅂  Γ⅂ Γ⅂   |ee",
                    "ee.--..o..---.ee"} };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eeee.--o--.e.---.e",
                    ".---⅃     L-⅃   L.",
                    "|                |",
                    ".⅂               |",
                    "e|       Γ-⅂  x  |",
                    "e|      Γ.e|     |",
                    ".⅃      L..⅃   Γ-.",
                    "o        L⅃   Γ.ee",
                    "|             |eee",
                    ".⅂   Γ-⅂      |eee",
                    "e.⅂  |e.⅂  Γ--.eee",
                    "ee.--.ee.o-.eeeeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeee.--o--.eeeee",
                    ".---⅃     L-.eee",
                    "|           L.ee",
                    ".⅂           L.e",
                    "e|     Γ-⅂    |e",
                    ".⅃    Γ.e.⅂   L.",
                    "|     L.ee.⅂   |",
                    "|      L-..⅃   o",
                    "|   x    L⅃    |",
                    "|              |",
                    ".⅂   Γ⅂      Γ-.",
                    "e.⅂  |.⅂   Γ-.ee",
                    "ee.--.e.---.eeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeeeeeee.-o----.e.-.ee",
                    "eeee.-.ee.⅃      L-⅃ |ee",
                    "eee.⅃ L--⅃           |ee",
                    "e.-⅃        Γ-⅂      |ee",
                    ".⅃          L-⅃     Γ.ee",
                    "|      Γ⅂           L.ee",
                    "|      |.⅂           L-.",
                    "o     Γ..⅃    Γ--⅂     |",
                    "|    Γ.e⅃     L.e.⅂    o",
                    ".-⅂  |e|       |ee.⅂   |",
                    "ee.--.e|   x   L.ee.⅂ Γ.",
                    "eeeeeee.⅂       |eee.-.e",
                    "eeeeeeee.⅂     Γ.eeeeeee",
                    "eeeeeeeee.-----.eeeeeeee"
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eee.----o-----.eeeee",
                    "ee.⅃          L.eeee",
                    "e.⅃         x  |eeee",
                    "e|      Γ⅂    Γ.eeee",
                    "e.⅂    Γ..⅂  Γ..---.",
                    "e.⅃   Γ.ee.--..⅃   |",
                    ".⅃   Γ.eeeeeee|    o",
                    "|    L..---.e.⅃    |",
                    "|     L⅃   L-⅃    Γ.",
                    ".⅂                |e",
                    "e.-⅂             Γ.e",
                    "eee.⅂     Γ-⅂  Γ-.ee",
                    "eeee.---o-.e.--.eeee"
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeeee.----o--.eeeeeeee",
                    "eeeeee.⅃       L.eeeeeee",
                    "eeeee.⅃         L---.eee",
                    "eeee.⅃              L-.e",
                    "ee.-⅃        Γ-⅂      L.",
                    ".-⅃         Γ.e.-⅂     |",
                    "|     Γ--⅂  L..--⅃     o",
                    "o     L--⅃   L⅃        |",
                    "|                  Γ⅂  |",
                    ".⅂           x   Γ-..--.",
                    "e.⅂    Γ⅂       Γ.eeeeee",
                    "ee.⅂  Γ..-⅂    Γ.eeeeeee",
                    "eee.--.eee.-o--.eeeeeeee",
                    },
                    new string[] {
                    ".-----------o----------.",
                    "|                      |",
                    "|   Γ---⅂      Γ---⅂   |",
                    "|   L---⅃      L---⅃   |",
                    "|                      |",
                    "|                      |",
                    "o          x           o",
                    "|                      |",
                    "|   Γ--------------⅂   |",
                    "|   L--------------⅃   |",
                    "|                      |",
                    "|                      |",
                    ".---------o------------.",
                } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors, roomType);
            }

            throw new Exception("Il n'existe pas de salle préfaite pour cette combinaison de porte");
        }
    }
}
