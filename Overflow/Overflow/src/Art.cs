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
            tileset1 = new TileSet(20, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/RightWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/LeftWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopLeftWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopRightWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomRightWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomLeftWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopLeftCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopRightCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomRightCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomLeftCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/Terrain") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TerrainWithWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TerrainWithWallBorderLeft") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TerrainWithWallBorderRight") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopDoor") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/RightDoor") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomDoor") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/LeftDoor") }) ;

            player = content.Load<Texture2D>("perso");
            enemy = content.Load<Texture2D>("CubeEnemy");
        }
    }
}
