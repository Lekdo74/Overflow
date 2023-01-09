using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public static class PlayerSlash
    {
        private static AnimatedSprite _slash = new AnimatedSprite(Art.slashSpriteSheet);
        private static float _remainingAnimationTime;

        public static AnimatedSprite Slash
        {
            get { return _slash; }
            set { _slash = value; }
        }

        public static float RemainingAnimationTime
        {
            get { return _remainingAnimationTime; }
            set { _remainingAnimationTime = value; }
        }

        public static void Update(float deltaTime)
        {
            if(RemainingAnimationTime > 0)
                Slash.Update(deltaTime);
        }

        public static void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (RemainingAnimationTime > 0)
                spriteBatch.Draw(Slash, position);
        }
    }
}
