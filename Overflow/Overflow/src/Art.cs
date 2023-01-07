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
        public static TileSet tileset1;

        public static Texture2D player;
        public static Texture2D enemy;

        public static void Load(ContentManager content)
        {
            //{ content.Load<Texture2D>("TileSets/Level1/TopLeftWall"), content.Load<Texture2D>("TileSets/Level1/TopRightWall"), content.Load<Texture2D>("TileSets/Level1/BottomRightWall"), content.Load<Texture2D>("TileSets/Level1/BottomLeftWall"), content.Load<Texture2D>("TileSets/Level1/TopWall"), content.Load<Texture2D>("TileSets/Level1/RightWall"), content.Load<Texture2D>("TileSets/Level1/BottomWall"), content.Load<Texture2D>("TileSets/Level1/LeftWall"), content.Load<Texture2D>("TileSets/Level1/Terrain"), content.Load<Texture2D>("MapTiles/porte"), content.Load<Texture2D>("TileSets/Level1/TopLeftCorner"), content.Load<Texture2D>("TileSets/Level1/TopRightCorner"), content.Load<Texture2D>("TileSets/Level1/BottomRightCorner"), content.Load<Texture2D>("TileSets/Level1/BottomLeftCorner") }
            tileset1 = new TileSet(20, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/RightWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/LeftWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopLeftWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopRightWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomRightWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomLeftWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopLeftCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopRightCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomRightCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomLeftCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/Terrain") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/Door") }) ;

            player = content.Load<Texture2D>("perso");
            enemy = content.Load<Texture2D>("CubeEnemy");
        }
    }
}
