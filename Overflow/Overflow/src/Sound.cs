using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

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

        private static float[] backgroundMusicVolumeEqualizer = new float[] { 0.7f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f };

        public static SoundEffect buttonSoundEffect;
        public static SoundEffect changeRoom;
        public static SoundEffect damageToEnemy;
        public static SoundEffect damageToPlayer;
        public static SoundEffect rangeEnemyAttack;
        public static SoundEffect bossAttack1;
        public static SoundEffect bossAttack2;
        public static SoundEffect dashPlayer;


        private static float[] soundEffectVolumeEqualizer = new float[] { 1f, 0.5f, 1f, 0.6f, 1f, 1f, 1f, 0.38f };

        public static void Load(ContentManager content)
        {
            MediaPlayer.IsRepeating = true;
            menu = content.Load<Song>("Musics/Menu");
            tutorial = content.Load<Song>("Musics/Tutorial");
            level1 = content.Load<Song>("Musics/Level1");
            level2 = content.Load<Song>("Musics/Level2");
            boss = content.Load<Song>("Musics/Boss");
            ending = content.Load<Song>("Musics/Ending");
            buttonSoundEffect = content.Load<SoundEffect>("SoundEffects/ButtonSoundEffect");
            changeRoom = content.Load<SoundEffect>("SoundEffects/changeRoom");
            damageToEnemy = content.Load<SoundEffect>("SoundEffects/damageToEnemy");
            damageToPlayer = content.Load<SoundEffect>("SoundEffects/damageToPlayer");
            rangeEnemyAttack = content.Load<SoundEffect>("SoundEffects/rangeEnemyAttack");
            bossAttack1 = content.Load<SoundEffect>("SoundEffects/bossAttack1");
            bossAttack2 = content.Load<SoundEffect>("SoundEffects/bossAttack2");
            dashPlayer = content.Load<SoundEffect>("SoundEffects/dashSoundEffect");
        }

        public static void ChangeBackgroundMusic(Song backgroundMusic)
        {
            if (backgroundMusic == currentSong)
                return;
            MediaPlayer.Play(backgroundMusic);
            currentSong = backgroundMusic;
            ApplyVolumeEqualizeBackgroudMusic(currentSong);
        }

        public static void ApplyVolumeEqualizeBackgroudMusic(Song backgroundMusic)
        {
            switch (backgroundMusic)
            {
                case Song value when value == menu:
                    MediaPlayer.Volume = backgroundMusicVolumeEqualizer[0] * Settings.soundMusicVolume;
                    break;

                case Song value when value == tutorial:
                    MediaPlayer.Volume = backgroundMusicVolumeEqualizer[1] * Settings.soundMusicVolume;
                    break;

                case Song value when value == level1:
                    MediaPlayer.Volume = backgroundMusicVolumeEqualizer[2] * Settings.soundMusicVolume;
                    break;

                case Song value when value == level2:
                    MediaPlayer.Volume = backgroundMusicVolumeEqualizer[3] * Settings.soundMusicVolume;
                    break;

                case Song value when value == boss:
                    MediaPlayer.Volume = backgroundMusicVolumeEqualizer[4] * Settings.soundMusicVolume;
                    break;

                case Song value when value == ending:
                    MediaPlayer.Volume = backgroundMusicVolumeEqualizer[5] * Settings.soundMusicVolume;
                    break;
            }
        }

        public static void PlaySound(SoundEffect soundEffect)
        {          
            soundEffect.Play(getVolumeEqualizerSoundEffect(soundEffect), 0, 0);
        }

        public static float getVolumeEqualizerSoundEffect(SoundEffect soundEffect)
        {
            switch (soundEffect)
            {
                case SoundEffect value when value == buttonSoundEffect:
                    return soundEffectVolumeEqualizer[0] * Settings.soundSoundEffectVolume;
                case SoundEffect value when value == changeRoom:
                    return soundEffectVolumeEqualizer[1] * Settings.soundSoundEffectVolume;
                case SoundEffect value when value == damageToEnemy:
                    return soundEffectVolumeEqualizer[2] * Settings.soundSoundEffectVolume;
                case SoundEffect value when value == damageToPlayer:
                    return soundEffectVolumeEqualizer[3] * Settings.soundSoundEffectVolume;
                case SoundEffect value when value == rangeEnemyAttack:
                    return soundEffectVolumeEqualizer[4] * Settings.soundSoundEffectVolume;
                case SoundEffect value when value == bossAttack1:
                    return soundEffectVolumeEqualizer[5] * Settings.soundSoundEffectVolume;
                case SoundEffect value when value == bossAttack2:
                    return soundEffectVolumeEqualizer[6] * Settings.soundSoundEffectVolume;
                case SoundEffect value when value == dashPlayer:
                    return soundEffectVolumeEqualizer[7] * Settings.soundSoundEffectVolume;
            }
            return 1;
        }
    }
}
