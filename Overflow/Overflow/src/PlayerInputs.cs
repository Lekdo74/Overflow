using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Overflow.src
{
    public class PlayerInputs
    {
        public static Vector2 MousePosition()
        {
            return new Vector2(Mouse.GetState().X * Settings.nativeWidthResolution / Settings.currentWidthResolution, Mouse.GetState().Y * Settings.nativeHeightResolution / Settings.currentHeightResolution);
        }

        public static Vector2 MousePosition(out MouseState mouseState)
        {
            mouseState = Mouse.GetState();
            return new Vector2(mouseState.X * Settings.nativeWidthResolution / Settings.currentWidthResolution, mouseState.Y * Settings.nativeHeightResolution / Settings.currentHeightResolution);
        }
        public static Vector2 GetPlayerDirection(KeyboardState _keyboardState)
        {
            Vector2 direction = Vector2.Zero;
            if (_keyboardState.IsKeyDown(Keys.Right) && !_keyboardState.IsKeyDown(Keys.Left))
            {
                direction.X += 1;
            }
            else if (_keyboardState.IsKeyDown(Keys.Left) && !_keyboardState.IsKeyDown(Keys.Right))
            {
                direction.X += -1;
            }
            if (_keyboardState.IsKeyDown(Keys.Down) && !_keyboardState.IsKeyDown(Keys.Up))
            {
                direction.Y += 1;
            }
            else if (_keyboardState.IsKeyDown(Keys.Up) && !_keyboardState.IsKeyDown(Keys.Down))
            {
                direction.Y -= 1;
            }
            if (direction.X != 0 && direction.Y != 0)
                direction.Normalize();
            return direction;
        }
    }
}
