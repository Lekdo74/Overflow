using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public static class Art
    {
        public static Texture2D player;
        public static Texture2D enemy;

        public static void Load(ContentManager content)
        {
            player = content.Load<Texture2D>("perso");
            enemy = content.Load<Texture2D>("CubeEnemy");
        }
    }
}
