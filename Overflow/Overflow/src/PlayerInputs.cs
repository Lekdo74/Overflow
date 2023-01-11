using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Overflow.src
{
    public static class PlayerInputs
    {
        private static KeyboardState _keyBoardState;
        private static MouseState _mouseState;

        public static KeyboardState KeyBoardState
        {
            get { return _keyBoardState; }
            set { _keyBoardState = value; }
        }

        public static MouseState MouseState
        {
            get { return _mouseState; }
            set { _mouseState = value; }
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
            if ((_keyboardState.IsKeyDown(Keys.D)) && !(_keyboardState.IsKeyDown(Keys.Q) || _keyboardState.IsKeyDown(Keys.A)))
            {
                direction.X += 1;
            }
            else if ((_keyboardState.IsKeyDown(Keys.Q) || _keyboardState.IsKeyDown(Keys.A)) && !(_keyboardState.IsKeyDown(Keys.D)))
            {
                direction.X += -1;
            }
            if ((_keyboardState.IsKeyDown(Keys.S)) && !(_keyboardState.IsKeyDown(Keys.Z) || _keyboardState.IsKeyDown(Keys.W)))
            {
                direction.Y += 1;
            }
            else if ((_keyboardState.IsKeyDown(Keys.Z) || _keyboardState.IsKeyDown(Keys.W)) && !(_keyboardState.IsKeyDown(Keys.S)))
            {
                direction.Y -= 1;
            }
            if (direction.X != 0 && direction.Y != 0)
                direction.Normalize();
            return direction;
        }

        public static string GetAnimation(Vector2 oldDirection, Vector2 newDirection, ref Texture2D currentDashTexture)
        {
            if(newDirection == Vector2.Zero)
            {
                if (oldDirection == Vector2.Zero)
                {
                    currentDashTexture = Player.CurrentDashTexture;
                    return Player.CurrentAnimation;
                }
                else if (oldDirection.Y < 0 || Player.CurrentAnimation == "leftBack" || Player.CurrentAnimation == "rightBack")
                {
                    currentDashTexture = Art.dashEffect[0];
                    Player.CurrentAnimation = "idleUp";
                    return "idleUp";
                }
                else if (oldDirection.X < 0)
                {
                    currentDashTexture = Art.dashEffect[4];
                    Player.CurrentAnimation = "idleLeft";
                    return "idleLeft";
                }
                else if(oldDirection.X > 0)
                {
                    currentDashTexture = Art.dashEffect[2];
                    Player.CurrentAnimation = "idleRight";
                    return "idleRight";
                }
                else
                {
                    currentDashTexture = Art.dashEffect[3];
                    Player.CurrentAnimation = "idleDown";
                    return "idleDown";
                }
            }
            else if (newDirection.X == 0)
            {
                if(newDirection.Y < 0)
                {
                    currentDashTexture = Art.dashEffect[0];
                    Player.CurrentAnimation = "up";
                    return "up";
                }
                else if (newDirection.Y > 0)
                {
                    currentDashTexture = Art.dashEffect[3];
                    Player.CurrentAnimation = "down";
                    return "down";
                }
            }
            else if(newDirection.X  < 0)
            {
                if(newDirection.Y < 0)
                {
                    currentDashTexture = Art.dashEffect[5];
                    Player.CurrentAnimation = "leftBack";
                    return "leftBack";
                }
                else if (newDirection.Y >= 0)
                {
                    currentDashTexture = Art.dashEffect[4];
                    Player.CurrentAnimation = "leftFront";
                    return "leftFront";
                }
            }
            else
            {
                if (newDirection.Y < 0)
                {
                    currentDashTexture = Art.dashEffect[1];
                    Player.CurrentAnimation = "rightBack";
                    return "rightBack";
                }
                else if (newDirection.Y >= 0)
                {
                    currentDashTexture = Art.dashEffect[2];
                    Player.CurrentAnimation = "rightFront";
                    return "rightFront";
                }
            }
            return null;
        }
    }
}
