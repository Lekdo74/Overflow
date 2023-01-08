using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public static class Sound
    {
        public static Song currentSong;

        public static Song menu;
        public static Song tutorial;
        public static Song level1;
        public static Song level2;
        public static Song boss;
        public static Song ending;

        private static float[] volumeEqualizer = new float[] { 0.9f, 1f, 1f, 1f, 1f, 1f};

        public static void Load(ContentManager content)
        {
            MediaPlayer.IsRepeating = true;
            menu = content.Load<Song>("Musics/Menu");
            tutorial = content.Load<Song>("Musics/Tutorial");
            level1 = content.Load<Song>("Musics/Level1");
            level2 = content.Load<Song>("Musics/Level2");
            boss = content.Load<Song>("Musics/Boss");
            ending = content.Load<Song>("Musics/Ending");
        }

        public static void ChangeBackgroundMusic(Song backgroundMusic)
        {
            if (backgroundMusic == currentSong)
                return;
            MediaPlayer.Play(backgroundMusic);
            currentSong = backgroundMusic;
            ApplyVolumeEqualizer(currentSong);
        }

        public static void ApplyVolumeEqualizer(Song backgroundMusic)
        {
            switch (backgroundMusic)
            {
                case Song value when value == menu:
                    MediaPlayer.Volume = volumeEqualizer[0] * Settings.soundVolume;
                    break;

                case Song value when value == tutorial:
                    MediaPlayer.Volume = volumeEqualizer[1] * Settings.soundVolume;
                    break;

                case Song value when value == level1:
                    MediaPlayer.Volume = volumeEqualizer[2] * Settings.soundVolume;
                    break;

                case Song value when value == level2:
                    MediaPlayer.Volume = volumeEqualizer[3] * Settings.soundVolume;
                    break;

                case Song value when value == boss:
                    MediaPlayer.Volume = volumeEqualizer[4] * Settings.soundVolume;
                    break;

                case Song value when value == ending:
                    MediaPlayer.Volume = volumeEqualizer[5] * Settings.soundVolume;
                    break;
            }
        }
    }
}
