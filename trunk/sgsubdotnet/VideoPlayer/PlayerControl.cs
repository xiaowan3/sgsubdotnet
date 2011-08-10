using System.Windows.Forms;

namespace VideoPlayer
{
    public abstract class PlayerControl : UserControl
    {
        public abstract void Init();
        public abstract void Uninit();

        public abstract void Pause();
        public abstract void OpenVideo(string filename);
        public abstract void Play();
        protected abstract double GetPosition();
        protected abstract void SetPosition(double pos);
        protected abstract bool IsPaused();
        protected abstract bool IsMediaOpened();
        protected abstract double GetDuration();

        public double CurrentPosition
        {
            get { return GetPosition(); }
            set { SetPosition(value); }
        }
        public bool Paused { get { return IsPaused(); } }
        public bool MediaOpened { get { return IsMediaOpened(); } }
        public double Duration{get { return GetDuration(); }}

    }
}
