using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public static class Settings
    {
        public static Vector2[] availableResolutions = new Vector2[] { new Vector2(480, 270), new Vector2(960, 540), new Vector2(1440, 810), new Vector2(1920, 1080), new Vector2(3840, 2160) };
        public static string[] availableResolutionsStrings = new string[] { "480x270", "960x540", "1440x810", "1920x1080", "3840x2160" };

        public static int nativeWidthResolution = 480;
        public static int nativeHeightResolution = 270;

        public static int launchWidthResolution = 960;
        public static int launchHeightResolution = 540;

        public static int currentResolution = 1;
        public static int currentWidthResolution = (int)availableResolutions[currentResolution].X;
        public static int currentHeightResolution = (int)availableResolutions[currentResolution].Y;

        public static bool fullscreen = false;

        public static int currentFps = 3;
        public static int[] availableFps = new int[] { 30, 60, 120, 144, 240, 360 };
        public static int FPS = availableFps[currentFps];
    }
}
