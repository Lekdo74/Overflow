using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

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

        public static List<string> walls = new List<string> { "Γ", "⌉", "⅃", "L", "-", "|", "." };
        public static List<string> terrains = new List<string> { " ", "o", "x" };

        private static Random random = new Random();

        public static Room RoomTuto(bool[] doors, int roomType, int[] enemyNb, TileSet tileSet, EnemySet enemyset, Song backgroungMusic)
        {
            if (roomType == 1)
            {
                string[][] room =
                {
                    new string[]
                    {
                        "eeeeeeeeeeeeeeeeeeeeeeee",
                        "eeeeeeeeeeeeeeeeeeeeeeee",
                        "eeeeeeeeee.-----.eeeeeee",
                        "eeeeeee.--⅃     L.eeeeee",
                        ".------⅃         |eeeeee",
                        "|              Γ-.eeeeee",
                        "o      x      Γ.eeeeeeee",
                        "|             |eeeeeeeee",
                        ".⅂           Γ.eeeeeeeee",
                        "e.⅂         Γ.eeeeeeeeee",
                        "ee.---------.eeeeeeeeeee",
                        "eeeeeeeeeeeeeeeeeeeeeeee",
                        "eeeeeeeeeeeeeeeeeeeeeeee",
                    }
                };
                return new Room(room[0], doors, 1, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (roomType == 2)
            {
                string[][] room =
                {
                    new string[]
                    {
                        "eeeeeeeeeeeeeeeeeeeeeeee",
                        "eeeeeeeeeeeeeeeeeeeeeeee",
                        "eeeeeee.------.eeeeeeeee",
                        "eeeeeee|      L.eeeeeeee",
                        ".------⅃       L-------.",
                        "|                      |",
                        "o           x          o",
                        "|                      |",
                        "|                      |",
                        ".⅂                    Γ.",
                        "e.-⅂        Γ---------.e",
                        "eee.⅂      Γ.eeeeeeeeeee",
                        "eeee.------.eeeeeeeeeeee",
                    }
                };
                return new Room(room[0], doors, 2, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (roomType == 3)
            {
                string[][] room =
                {
                    new string []
                    {
                        "eeeeeeeeeeeeeeeeeeeeeeee",
                        "eeeeeeeeeeeeeeeeeeeeeeee",
                        "eeeeeeeee.-------------.",
                        "eeeeeeee.⅃             |",
                        "eeeeeeee|              |",
                        "eeeeeeee|              |",
                        "eeeeeee.⅃       x      o",
                        "eeeeee.⅃               |",
                        "eeeeee|                |",
                        "eeeeee.⅂              Γ.",
                        "eeeeeee.--------------.e",
                        "eeeeeeeeeeeeeeeeeeeeeeee",
                        "eeeeeeeeeeeeeeeeeeeeeeee",
                    }
                };
                return new Room(room[0], doors, 2, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            throw new Exception("Cette salle n'est pas faite pour le tutoriel !");
        }

        public static Room BossRoom(bool[] doors, int roomType, int[] enemyNb, TileSet tileSet, EnemySet enemyset, Song backgroungMusic)
        {
            if (doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == true)
            {
                return new Room(new string[] {
                    "eeee.--------------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "o      FFFFFFFFF       |",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.--------------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == false)
            {
                return new Room(new string[] {
                    "eeee.--------------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "|      FFFFFFFFF       |",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.------o-------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == true)
            {
                return new Room(new string[] {
                    "eeee.--------------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "o      FFFFFFFFF       |",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.------o-------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == false)
            {
                return new Room(new string[] {
                    "eeee.--------------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "|      FFFFFFFFF       o",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.--------------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == true)
            {
                return new Room(new string[] {
                    "eeee.------o-------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "o      FFFFFFFFF       |",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.--------------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == false)
            {
                return new Room(new string[] {
                    "eeee.--------------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "|      FFFFFFFFF       o",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.------o-------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == true)
            {
                return new Room(new string[] {
                    "eeee.--------------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "o      FFFFFFFFF       o",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.------o-------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == false)
            {
                return new Room(new string[] {
                    "eeee.------o-------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "|      FFFFFFFFF       |",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.--------------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == true)
            {
                return new Room(new string[] {
                    "eeee.------o-------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "o      FFFFFFFFF       |",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.--------------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == false)
            {
                return new Room(new string[] {
                    "eeee.------o-------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "|      FFFFFFFFF       |",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.------o-------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == true)
            {
                return new Room(new string[] {
                    "eeee.------o-------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "o      FFFFFFFFF       |",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.------o-------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == false)
            {
                return new Room(new string[] {
                    "eeee.------o-------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "|      FFFFFFFFF       o",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.--------------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == true)
            {
                return new Room(new string[] {
                    "eeee.------o-------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "o      FFFFFFFFF       o",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.--------------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == false)
            {
                return new Room(new string[] {
                    "eeee.------o-------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "|      FFFFFFFFF       o",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.------o-------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == true)
            {
                return new Room(new string[] {
                    "eeee.------o-------.eeee",
                    "ee.-⅃              L-.ee",
                    "e.⅃      FFFFF       L.e",
                    ".⅃      FFFFFFF       L.",
                    "|      FF  F  FF       |",
                    "|      FF  F  FF       |",
                    "o      FFFFFFFFF       o",
                    "|       FFFxFFF        |",
                    "|       FFFFFFF        |",
                    ".⅂      FF F FF       Γ.",
                    "e.⅂      F F F       Γ.e",
                    "ee.-⅂              Γ-.ee",
                    "eeee.------o-------.eeee",
                    }, doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }

            throw new Exception("Il n'existe pas de salle de boss préfaite pour cette combinaison de porte");
        }



        public static Room Room(bool[] doors, int roomType, int[] enemyNb, TileSet tileSet, EnemySet enemyset, Song backgroungMusic)
        {
            if (doors[0] == false && doors[1] == false && doors[2] == false && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    "eeee.---.eeeeeeeeeeeeeee",
                    ".---⅃   L-.eeeeeeeeeeeee",
                    "|         L.eeeeeeeeeeee",
                    ".⅂         L-.eeeeeeeeee",
                    ".⅃           L.eeeeeeeee",
                    "o      x      |eeeeeeeee",
                    "|             |eeeeeeeee",
                    "|             |eeeeeeeee",
                    ".⅂   Γ-⅂      |eeeeeeeee",
                    "e.⅂  |e.⅂  Γ--.eeeeeeeee",
                    "ee.--.ee.--.eeeeeeeeeeee",
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    }, new string[] {
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    "eeee.---.eeeeeeeeeeeeeee",
                    ".---⅃   L-.eeeeeeeeeeeee",
                    "|         L.eee.--.eeeee",
                    ".⅂         L-..⅃  L.eeee",
                    ".⅃           L⅃    |eeee",
                    "o                  |eeee",
                    "|             x    |eeee",
                    "|                 Γ.eeee",
                    ".⅂   Γ-⅂         Γ.eeeee",
                    "e.⅂  |e.⅂  Γ-----.eeeeee",
                    "ee.--.ee.--.eeeeeeeeeeee",
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    "eeeeeeee.---.eeeeeeeeeee",
                    "eeee.---⅃   L.e.-.eeeeee",
                    "eeee|        L-⅃ |eeeeee",
                    "eeee.⅂           L.eeeee",
                    "eeeee|            |eeeee",
                    "eeee.⅃      x     |eeeee",
                    "eeee|             |eeeee",
                    "eeee|             |eeeee",
                    "eeee.⅂ Γ⅂         |eeeee",
                    "eeeee.-..⅂     Γ--.eeeee",
                    "eeeeeeeee.-o---.eeeeeeee",
                    }, new string[] {
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    "eeeeeee.--------.eeeeeee",
                    "eeeeee.⅃        L.eeeeee",
                    "eeeee.⅃          L.eeeee",
                    "eeeee|            |eeeee",
                    "eeee.⅃      x     L.eeee",
                    "eeee|              |eeee",
                    "eeee|             Γ.eeee",
                    "eeee.⅂ Γ⅂         |eeeee",
                    "eeeee.-..⅂     Γ--.eeeee",
                    "eeeeeeeee.-o---.eeeeeeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == false && doors[2] == true && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eeee.---.eee.---.eeeeeeee",
                    ".---⅃   L.e.⅃   |eeeeeeee",
                    "|        L-⅃    L.eeeeeee",
                    ".⅂               L-.eeeee",
                    ".⅃                 L.eeee",
                    "|                   |eeee",
                    "o         x         |eeee",
                    "|                   |eeee",
                    "|                   |eeee",
                    "|                  Γ.eeee",
                    ".⅂ Γ⅂             Γ.eeeee",
                    "e.-..---⅂   Γ-⅂ Γ-.eeeeee",
                    "eeeeeeee.-o-.e.-.eeeeeeee",
                    }, new string[] {
                    "eeee.---.eee.--.eeeeeeee",
                    ".---⅃   L.e.⅃  |eeeeeeee",
                    "|        L-⅃   L.eeeeeee",
                    ".⅂              L-.eeeee",
                    ".⅃                L.eeee",
                    "|                  L-.ee",
                    "o         x          L.e",
                    "|                     |e",
                    ".---⅂                 |e",
                    "eeee.--⅂            Γ-.e",
                    "eeeeeee|          Γ-.eee",
                    "eeeeeee.⅂   Γ-⅂ Γ-.eeeee",
                    "eeeeeeee.-o-.e.-.eeeeeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    "eeeeeeeeeeeee.-----.eeee",
                    "eeeeeeeee.---⅃     L-.ee",
                    "eeeeeeeee|           L.e",
                    "eeeeeeeee.⅂           L.",
                    "eeeeeeeeee|      x     |",
                    "eeeeeeeee.⅃            o",
                    "eeeeeeeee|             |",
                    "eeeeeeeee|             |",
                    "eeeeeeeee.⅂   Γ-⅂      |",
                    "eeeeeeeeee.⅂  |e.⅂  Γ--.",
                    "eeeeeeeeeee.--.ee.--.eee",
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == false && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eee.------.eee.----.eeee",
                    ".--⅃      L.e.⅃    L-.ee",
                    "|          L-⅃       L.e",
                    ".⅂                    L.",
                    ".⅃                     |",
                    "|           x          |",
                    "o                      o",
                    "|                      |",
                    "|                      |",
                    ".⅂        Γ-⅂          |",
                    "e.⅂       |e.⅂      Γ--.",
                    "ee.-------.ee.------.eee",
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    },  new string[] {
                    "eee.---.eeeeeee.---.eeee",
                    ".--⅃   L.eeeee.⅃   L-.ee",
                    "|       L.eee.⅃      L.e",
                    ".⅂       |eee|        L.",
                    ".⅃       |eee|         |",
                    "|      Γ-.eee|         |",
                    "o      |eeeee.⅂        o",
                    "|      L------⅃        |",
                    "|                     Γ.",
                    ".⅂                   Γ.e",
                    "e.-⅂             Γ---.ee.",
                    "eee.-------------.eeeeee",
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    }};
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    "eeeeeeeee.-.eee.---.eeee",
                    "eeeee.---⅃ L.e.⅃   L-.ee",
                    "eeeee|      L-⅃      L.e",
                    "eeeee.⅂               L.",
                    "eeeeee|                |",
                    "eeeee.⅃                o",
                    "eeeee|         x       |",
                    "eeeee|                 |",
                    "eeeee.⅂ Γ⅂             |",
                    "eeeeee.-..⅂         Γ--.",
                    "eeeeeeeeee|     Γ---.eee",
                    "eeeeeeeeee.-o---.eeeeeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == false && doors[1] == true && doors[2] == true && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    "eeeee.-.eeeeee.---.eeeee",
                    ".----⅃ L.eeee.⅃   L-.eee",
                    "|       L.ee.⅃      L.ee",
                    ".⅂       L..⅃        L-.",
                    ".⅃        L⅃           |",
                    "|                      |",
                    "o                      o",
                    "|           x          |",
                    ".-⅂ Γ⅂                Γ.",
                    "ee.-..⅂            Γ--.e",
                    "eeeeee|           Γ.eeee",
                    "eeeeee.-----o-----.eeeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeeee.----o---.eeeeeee",
                    "eeeeee.⅃        L.eeeeee",
                    "eeeee.⅃          L.eeeee",
                    "eeeee|            |eeeee",
                    "eeeee.⅂          Γ.eeeee",
                    "eeeeee.⅂         |eeeeee",
                    "eeeeee.⅃    x    L-.eeee",
                    "eeeeee|            |eeee",
                    "eeeeee|           Γ.eeee",
                    "eeeeee.⅂         Γ.eeeee",
                    "eeeeeee.⅂  Γ-⅂   |eeeeee",
                    "eeeeeeee.--.e.---.eeeeee",
                    "eeeeeeeeeeeeeeeeeeeeeeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == false && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeee.---o-.ee.---.eeee",
                    "eee.--⅃     L--⅃   |eeee",
                    "ee.⅃              Γ.eeee",
                    "ee|              Γ.eeeee",
                    "e.⅃     Γ⅂       |eeeeee",
                    ".⅃     Γ..⅂      L.eeeee",
                    "|     Γ.e.⅃       L.eeee",
                    "o     |e.⅃         |eeee",
                    "|     L-⅃    x     |eeee",
                    "|                  |eeee",
                    ".⅂               Γ-.eeee",
                    "e.⅂       Γ-⅂    |eeeeee",
                    "ee.-------.e.----.eeeeee"}
                    };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeeee.----o---.eeeeeee",
                    "eeeeee.⅃        L.eeeeee",
                    "eeeee.⅃          L.eeeee",
                    "eeeee|            |eeeee",
                    "eeeee|           Γ.eeeee",
                    "eeeee|           |eeeeee",
                    "eeeee|      x    L-.eeee",
                    "eeeee|             |eeee",
                    "eeeee|             |eeee",
                    "eeeee|            Γ.eeee",
                    "eeeee.-⅂         Γ.eeeee",
                    "eeeeeee.⅂        |eeeeee",
                    "eeeeeeee.---o----.eeeeee"},
                    new string[] {
                    "eeeeeeee.---o---.eeeeeee",
                    "eeeeeee.⅃       L-.eeeee",
                    "eee.---⅃          L.eeee",
                    "eee|               |eeee",
                    "eee.⅂             Γ.eeee",
                    "eeee.⅂            |eeeee",
                    "eee.-⅃            L-.eee",
                    "eee|        x       |eee",
                    "eee|                |eee",
                    "eee|               Γ.eee",
                    "eee.⅂             Γ.eeee",
                    "eeee.⅂  Γ⅂   Γ⅂   |eeeee",
                    "eeeee.--..-o-..---.eeeee"} };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == false && doors[2] == true && doors[3] == true)
            {
                string[][] rooms = {
                    new string[] {
                    "eeee.--.e.-o-----.eeeeee",
                    ".---⅃  L-⅃       L.eeeee",
                    "|                 |eeeee",
                    "|                 |eeeee",
                    ".⅂                |eeeee",
                    "e|       Γ-⅂  x   |eeeee",
                    ".⅃      Γ.e|      |eeeee",
                    "o       L..⅃    Γ-.eeeee",
                    "|        L⅃    Γ.eeeeeee",
                    "|              |eeeeeeee",
                    ".⅂   Γ-⅂       |eeeeeeee",
                    "e.⅂  |e.⅂   Γ--.eeeeeeee",
                    "ee.--.ee.-o-.eeeeeeeeeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == false && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeeeeee.-o------.eeeee",
                    "eeeeeeeee|        L-.eee",
                    "eeeeeeeee.--⅂       L.ee",
                    "eeeeeeee.---⅃        L.e",
                    "eeeeeeee|      Γ-⅂    |e",
                    "eeeeeeee.⅂    Γ.e.⅂   L.",
                    "eeeeeeeee|    L.ee.⅂   |",
                    "eeeeeeee.⅃     L-..⅃   o",
                    "eeeeeeee|   x    L⅃    |",
                    "eeeeeeee|              |",
                    "eeeeeeee.⅂   Γ⅂      Γ-.",
                    "eeeeeeeee.⅂  |.⅂   Γ-.ee",
                    "eeeeeeeeee.--.e.---.eeee",
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
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
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }
            else if (doors[0] == true && doors[1] == true && doors[2] == true && doors[3] == false)
            {
                string[][] rooms = {
                    new string[] {
                    "eeeeee.----o-----.eeeeee",
                    "eeeee.⅃          L.eeeee",
                    "eeee.⅃         x  |eeeee",
                    "eeee|      Γ⅂    Γ.e.-.e",
                    "eeee.⅂    Γ..⅂  Γ..-⅃ L.",
                    "eeee.⅃   Γ.ee.--..⅃    |",
                    "eee.⅃   Γ.eeeeeee|     o",
                    "eee|    L..---.e.⅃     |",
                    "eee|     L⅃   L-⅃     Γ.",
                    "eee.⅂                 |e",
                    "eeee.-⅂              Γ.e",
                    "eeeeee.⅂      Γ-⅂  Γ-.ee",
                    "eeeeeee.---o--.e.--.eeee"
                    } };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
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
                    "eeeeeeeee.-o-.eeeeeeeeee",
                    "eeee.----⅃   L-----.eeee",
                    "eeee|              |eeee",
                    "eeee|  Γ-⅂    Γ-⅂  |eeee",
                    ".---⅃  L-⅃    L-⅃  L---.",
                    "|                      |",
                    "o          x           o",
                    "|                      |",
                    ".---⅂  Γ--------⅂  Γ---.",
                    "eeee|  L--------⅃  |eeee",
                    "eeee|              |eeee",
                    "eeee.----⅂   Γ-----.eeee",
                    "eeeeeeeee.-o-.eeeeeeeeee",
                } };
                return new Room(rooms[random.Next(0, rooms.Length)], doors, roomType, enemyNb, tileSet, enemyset, backgroungMusic);
            }

            throw new Exception("Il n'existe pas de salle préfaite pour cette combinaison de porte");
        }
    }
}
