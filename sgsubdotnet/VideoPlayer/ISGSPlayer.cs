using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGS.VideoPlayer
{
    public interface ISGSPlayer
    {
        void Init();
        void Uninit();

        void Pause();
        void OpenVideo(string filename);
        void Play();
        void Step();
        void TogglePause();

        double CurrentPosition{get; set; }
        bool Paused { get; }
        bool MediaOpened { get; }
        bool CanStep { get; }
        double Duration { get; }
    }
}
