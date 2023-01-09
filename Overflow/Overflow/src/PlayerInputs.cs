using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Overflow.src
{
    public static class PlayerInputs
    {
        private static KeyboardState _keyBoardState;

        public static KeyboardState KeyBoardState
        {
            get { return _keyBoardState; }
            set { _keyBoardState = value; }
        }

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

        public static string GetAnimation(Vector2 oldDirection, Vector2 newDirection)
        {
            if(newDirection == Vector2.Zero)
            {
                if (oldDirection == Vector2.Zero)
                    return Player.CurrentAnimation;
                else if (oldDirection.Y < 0 || Player.CurrentAnimation == "leftBack" || Player.CurrentAnimation == "rightBack")
                {
                    Player.CurrentAnimation = "idleUp";
                    return "idleUp";
                }
                else if (oldDirection.X < 0)
                {
                    Player.CurrentAnimation = "idleLeft";
                    return "idleLeft";
                }
                else if(oldDirection.X > 0)
                {
                    Player.CurrentAnimation = "idleRight";
                    return "idleRight";
                }
                else
                {
                    Player.CurrentAnimation = "idleDown";
                    return "idleDown";
                }
            }
            else if (newDirection.X == 0)
            {
                if(newDirection.Y < 0)
                {
                    Player.CurrentAnimation = "up";
                    return "up";
                }
                else if (newDirection.Y > 0)
                {
                    Player.CurrentAnimation = "down";
                    return "down";
                }
            }
            else if(newDirection.X  < 0)
            {
                if(newDirection.Y < 0)
                {
                    Player.CurrentAnimation = "leftBack";
                    return "leftBack";
                }
                else if (newDirection.Y >= 0)
                {
                    Player.CurrentAnimation = "leftFront";
                    return "leftFront";
                }
            }
            else
            {
                if (newDirection.Y < 0)
                {
                    Player.CurrentAnimation = "rightBack";
                    return "rightBack";
                }
                else if (newDirection.Y >= 0)
                {
                    Player.CurrentAnimation = "rightFront";
                    return "rightFront";
                }
            }
            return null;
        }
    }
}
