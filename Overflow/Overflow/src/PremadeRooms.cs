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

        public static Room Room(bool[] doors, Texture2D[] tileSet)
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
                    "o             |",
                    "|             |",
                    "|             |",
                    ".⅂   Γ-⅂      |",
                    "e.⅂  |e.⅂  Γ--.",
                    "ee.--.ee.--.eee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                    ".⅃            |",
                    "|             |",
                    "|             |",
                    ".⅂ Γ⅂         |",
                    "e.-..⅂     Γ--.",
                    "eeeee.-o---.eee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                    "o                  |",
                    "|                  |",
                    "|                  |",
                    "|                 Γ.",
                    ".⅂ Γ⅂            Γ.e",
                    "e.-..---⅂  Γ-⅂ Γ-.ee",
                    "eeeeeeee.-o.e.-.eeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeee.-----.eeee",
                    ".---⅃     L-.ee",
                    "|           L.e",
                    ".⅂           L.",
                    "e|            |",
                    ".⅃            o",
                    "|             |",
                    "|             |",
                    ".⅂   Γ-⅂      |",
                    "e.⅂  |e.⅂  Γ--.",
                    "ee.--.ee.--.eee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                    "|                  |",
                    "o                  o",
                    "|                  |",
                    "|                  |",
                    ".⅂      Γ-⅂        |",
                    "e.⅂     |e.⅂    Γ--.",
                    "ee.-----.ee.----.eee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                    "|                 |",
                    "|                 |",
                    ".⅂ Γ⅂             |",
                    "e.-..⅂         Γ--.",
                    "eeeee|        Γ.eee",
                    "eeeee.---o----.eeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                    "|                    |",
                    ".⅂ Γ⅂                |",
                    "e.-..⅂            Γ--.",
                    "eeeee|           Γ.eee",
                    "eeeee.-----o-----.eeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                    "e.⅃         L-.",
                    "e|            |",
                    "e|           Γ.",
                    "e.⅂         Γ.e",
                    "ee.⅂  Γ-⅂   |ee",
                    "eee.--.e.---.ee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                    "|    L-⅃        |",
                    "|               |",
                    ".⅂            Γ-.",
                    "e.⅂     Γ-⅂   |ee",
                    "ee.-----.e.---.ee"},
                    };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                    "|           L-.",
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
                    "|              |",
                    "|              |",
                    "|             Γ.",
                    ".⅂           Γ.e",
                    "e.⅂  Γ⅂ Γ⅂   |ee",
                    "ee.--..o..---.ee"} };
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
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
                return new Room(rooms[random.Next(0, rooms.Length)], tileSet, doors);
            }

            throw new Exception("Il n'existe pas de salle préfaite pour cette combinaison de porte");
        }
    }
}
