using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overflow.src
{
    public abstract class Component
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spritebatch);

        public abstract void Update(GameTime gameTime);
    }
}
