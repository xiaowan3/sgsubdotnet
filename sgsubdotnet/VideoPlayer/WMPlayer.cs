using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMPLib;

namespace VideoPlayer
{
    public class WMPlayer : AxWMPLib.AxWindowsMediaPlayer, ISGSPlayer
    {
        public void Init()
        {
          
        }

        public void Uninit()
        {
       
        }

        public void Pause()
        {
            base.Ctlcontrols.pause();
        }

        public void OpenVideo(string filename)
        {
            base.URL = filename;
            base.Ctlcontrols.play();
        }

        public void Play()
        {
            if (base.currentMedia != null) base.Ctlcontrols.play();
        }

        public void Step()
        {
        }

        public void TogglePause()
        {
            if (base.currentMedia != null)
            {
                if (base.playState != WMPLib.WMPPlayState.wmppsPlaying) base.Ctlcontrols.play();
                else base.Ctlcontrols.pause();
            }
        }

        public double CurrentPosition
        {
            get { return base.Ctlcontrols.currentPosition; }
            set { base.Ctlcontrols.currentPosition = value; }
        }

        public bool Paused
        {
            get { return base.playState == WMPPlayState.wmppsPaused; }
        }

        public bool MediaOpened
        {
            get { return base.currentMedia != null; }
        }

        public bool CanStep
        {
            get { return false; }
        }

        public double Duration
        {
            get { return base.currentMedia.duration; }
        }
    }
}
