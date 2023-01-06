using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public static class Art
    {
        public static Texture2D[] tileset1;

        public static Texture2D player;
        public static Texture2D enemy;

        public static void Load(ContentManager content)
        {
            tileset1 = new Texture2D[] { content.Load<Texture2D>("MapTiles/murHautGauche"), content.Load<Texture2D>("MapTiles/murHautDroite"), content.Load<Texture2D>("MapTiles/murBasDroite"), content.Load<Texture2D>("MapTiles/murBasGauche"), content.Load<Texture2D>("MapTiles/murHaut"), content.Load<Texture2D>("MapTiles/murDroite"), content.Load<Texture2D>("MapTiles/murBas"), content.Load<Texture2D>("MapTiles/murGauche"), content.Load<Texture2D>("MapTiles/herbe"), content.Load<Texture2D>("MapTiles/porte"), content.Load<Texture2D>("MapTiles/coinHautGauche"), content.Load<Texture2D>("MapTiles/coinHautDroite"), content.Load<Texture2D>("MapTiles/coinBasDroite"), content.Load<Texture2D>("MapTiles/coinBasGauche") };

            player = content.Load<Texture2D>("perso");
            enemy = content.Load<Texture2D>("CubeEnemy");
        }
    }
}
