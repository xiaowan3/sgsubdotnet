using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoPlayer
{
    public interface ISGSPlayer
    {
        void Init();
        void Uninit();

        void Pause();
        void OpenVideo(string filename);
        void Play();
        void TogglePause();

        double CurrentPosition{get; set; }
         bool Paused { get; }
         bool MediaOpened { get; }
         double Duration { get; }
    }
}
