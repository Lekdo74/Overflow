using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public static class Art
    {
        public static Texture2D backgroundMainMenu;
        public static Texture2D backgroundSettingsMenu;

        //UI
        public static SpriteFont font;
        public static Texture2D leftArrow;
        public static Texture2D rightArrow;
        public static Texture2D emptyCase;
        public static Texture2D checkedCase;
        public static Texture2D[] buttons;
        public static Texture2D[] volumeBar;
        public static Texture2D[] healthBar;

        public static TileSet tilesetLevel1;
        public static EnemySet enemysetLevel1;
        public static Texture2D laser;
        public static Texture2D bouleRouge;

        public static SpriteSheet bossSpriteSheet;

        public static Texture2D player;
        public static SpriteSheet playerSpriteSheet;
        public static SpriteSheet slashSpriteSheet;
        public static Texture2D[] dashEffect;

        public static void Load(ContentManager content)
        {
            backgroundMainMenu = content.Load<Texture2D>("BackgroundMenus/MainMenu");
            backgroundSettingsMenu = content.Load<Texture2D>("BackgroundMenus/SettingsMenu");

            font = content.Load<SpriteFont>("UI/Font/Font");
            leftArrow = content.Load<Texture2D>("UI/LeftArrow");
            rightArrow = content.Load<Texture2D>("UI/RightArrow");
            emptyCase = content.Load<Texture2D>("UI/EmptyCase");
            checkedCase = content.Load<Texture2D>("UI/CheckedCase");
            buttons = new Texture2D[] { content.Load<Texture2D>("UI/DifferentButtonSizes/Button1"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button2"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button3"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button4"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button5"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button6"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button7"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button8"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button9"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button10"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button11"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button12"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button13"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button14"), content.Load<Texture2D>("UI/DifferentButtonSizes/Button15") };
            volumeBar = new Texture2D[] { content.Load<Texture2D>("UI/VolumeBar/VolumeBar1"), content.Load<Texture2D>("UI/VolumeBar/VolumeBar2"), content.Load<Texture2D>("UI/VolumeBar/VolumeBar3"), content.Load<Texture2D>("UI/VolumeBar/VolumeBar4"), content.Load<Texture2D>("UI/VolumeBar/VolumeBar5"), content.Load<Texture2D>("UI/VolumeBar/VolumeBar6"), content.Load<Texture2D>("UI/VolumeBar/VolumeBar7"), content.Load<Texture2D>("UI/VolumeBar/VolumeBar8"), content.Load<Texture2D>("UI/VolumeBar/VolumeBar9"), content.Load<Texture2D>("UI/VolumeBar/VolumeBar10"), content.Load<Texture2D>("UI/VolumeBar/VolumeBar11") };
            healthBar = new Texture2D[] { content.Load<Texture2D>("UI/HealthBar/HealthBar1"), content.Load<Texture2D>("UI/HealthBar/HealthBar2"), content.Load<Texture2D>("UI/HealthBar/HealthBar3"), content.Load<Texture2D>("UI/HealthBar/HealthBar4"), content.Load<Texture2D>("UI/HealthBar/HealthBar5"), content.Load<Texture2D>("UI/HealthBar/HealthBar6"), content.Load<Texture2D>("UI/HealthBar/HealthBar7"), content.Load<Texture2D>("UI/HealthBar/HealthBar8"), content.Load<Texture2D>("UI/HealthBar/HealthBar9"), content.Load<Texture2D>("UI/HealthBar/HealthBar10") };

            tilesetLevel1 = new TileSet(20, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/RightWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/LeftWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopLeftWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopRightWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomRightWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomLeftWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopLeftCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopRightCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomRightCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomLeftCorner") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/Terrain"), content.Load<Texture2D>("TileSets/Level1/Terrain2"), content.Load<Texture2D>("TileSets/Level1/Terrain3"), content.Load<Texture2D>("TileSets/Level1/Terrain4"), content.Load<Texture2D>("TileSets/Level1/Terrain5"), content.Load<Texture2D>("TileSets/Level1/Terrain6"), content.Load<Texture2D>("TileSets/Level1/Terrain7"), content.Load<Texture2D>("TileSets/Level1/Terrain8"), content.Load<Texture2D>("TileSets/Level1/Terrain9") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TerrainWithWall") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TerrainWithWallBorderLeft") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TerrainWithWallBorderRight") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TopDoor") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/RightDoor") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/BottomDoor") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/LeftDoor") }, new Texture2D[] { content.Load<Texture2D>("TileSets/Level1/TerrainFonce") }) ;
            enemysetLevel1 = new EnemySet(new Texture2D[] { content.Load<Texture2D>("EnemySets/Level1/SeekerTentacles") }, new Texture2D[] { content.Load<Texture2D>("EnemySets/Level1/ArcherLaser") });
            laser = content.Load<Texture2D>("Projectiles/Laser");
            bouleRouge = content.Load<Texture2D>("Projectiles/bouleRouge");

            bossSpriteSheet = content.Load<SpriteSheet>("Boss/BossAnimations.sf", new JsonContentLoader());

            player = content.Load<Texture2D>("Character/IdleLeft");
            playerSpriteSheet = content.Load<SpriteSheet>("Character/CharacterAnimations.sf", new JsonContentLoader());
            slashSpriteSheet = content.Load<SpriteSheet>("Character/Slash/SlashAnimations.sf", new JsonContentLoader());
            dashEffect = new Texture2D[] { content.Load<Texture2D>("Character/DashEffect/DashUp"), content.Load<Texture2D>("Character/DashEffect/DashRightBack"), content.Load<Texture2D>("Character/DashEffect/DashRightFront"), content.Load<Texture2D>("Character/DashEffect/DashDown"), content.Load<Texture2D>("Character/DashEffect/DashLeftFront"), content.Load<Texture2D>("Character/DashEffect/DashLeftBack") };
        }
    }
}
