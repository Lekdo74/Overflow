using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public static class PlayerHealthBar
    {
        private static Vector2 _position = new Vector2(5, Settings.nativeHeightResolution - Art.healthBar[0].Height - 5);

        private static Texture2D Texture
        {
            get { return Art.healthBar[Player.Health - 1]; }
        }

        public static void Update(GameTime gameTime)
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (Player.Health > 0)
                spriteBatch.Draw(Texture, _position, Color.White);
        }
    }
}
