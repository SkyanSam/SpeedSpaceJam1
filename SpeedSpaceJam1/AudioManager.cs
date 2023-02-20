using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
namespace SpeedSpaceJam1
{
    public static class AudioManager
    {
        static Music audioMain;
        static Music audioHunt;
        static Music audioSaw;
        static Music audioStomp;
        static Music audioBird;
        public static float huntVolume = 0f;
        public static float sawVolume = 0f;
        public static float stompVolume = 0f;
        public static float birdVolume = 0f;
        public static bool isHunt = true;
        public static bool isSaw = true;
        public static bool isStomp = true;
        public static bool isBird = true;
        public static void Start()
        {
            Raylib.InitAudioDevice();

            audioMain = Raylib.LoadMusicStream(Globals.resPath + "audio-main.wav");
            audioHunt = Raylib.LoadMusicStream(Globals.resPath + "audio-hunt.wav");
            audioSaw = Raylib.LoadMusicStream(Globals.resPath + "audio-saw.wav");
            audioStomp = Raylib.LoadMusicStream(Globals.resPath + "audio-stomp.wav");
            audioBird = Raylib.LoadMusicStream(Globals.resPath + "audio-bird.wav");

            Raylib.PlayMusicStream(audioMain);
            Raylib.PlayMusicStream(audioHunt);
            Raylib.PlayMusicStream(audioSaw);
            Raylib.PlayMusicStream(audioStomp);
            Raylib.PlayMusicStream(audioBird);
            Raylib.SetMusicVolume(audioMain, 1f);
        }
        public static void Update()
        {
            Raylib.UpdateMusicStream(audioMain);
            UpdateAudio(isSaw, ref sawVolume, ref audioSaw);
            UpdateAudio(isBird, ref birdVolume, ref audioBird);
            UpdateAudio(isStomp, ref stompVolume, ref audioStomp);
            UpdateAudio(isHunt, ref huntVolume, ref audioHunt);
        }
        public static void UpdateAudio(bool doUpdateVolume, ref float volume, ref Music music)
        {
            if (doUpdateVolume) volume += Raylib.GetFrameTime();
            else volume -= Raylib.GetFrameTime();
            if (volume > 1f) volume = 1f;
            if (volume < 0f) volume = 0f;
            Raylib.SetMusicVolume(music, volume);
            Raylib.UpdateMusicStream(music);
        }
    }
}
