using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SGS.VideoPlayer
{
    public partial class DShowPlayer : UserControl, ISGSPlayer
    {
        public DShowPlayer()
        {
            InitializeComponent();
            g1 = splitContainer1.Panel2.CreateGraphics();
        }


        private readonly Image _trackleft = Resources.PANEL_Left;
        private readonly Image _trackright = Resources.PANEL_DSRight;
        private readonly Image _trackmiddle = Resources.PANEL_Fill;
        private readonly Image _trackthumb = Resources.TRACK_Thumb;
        private readonly Image _soundthumb = Resources.SOUND_Thumb;
        private Rectangle _thumbarea = new Rectangle(2, 3, Resources.TRACK_Thumb.Width, Resources.TRACK_Thumb.Height);
        private Rectangle _sndarea = new Rectangle(0, 15, Resources.SOUND_Thumb.Width, Resources.SOUND_Thumb.Height);
        private double _soundvolume = 0.5;

        private bool _thumbselected;
        private bool _soundselected;

        private Graphics g1;


        void screen_SizeChanged(object sender, EventArgs e)
        {
            DShowSupport.Resize();
        }


        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            RedrawTrackbar();
        }

        private void splitContainer1_Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle trackBarRect = new Rectangle(_thumbarea.Width / 2, 3, Width - _thumbarea.Width, _thumbarea.Height);
            if (_thumbselected)
            {
                int x = e.X - _thumbarea.Width / 2;
                x = x < 0 ? 0 : x > splitContainer1.Panel2.Width - _thumbarea.Width ? splitContainer1.Panel2.Width - _thumbarea.Width : x;
                _thumbarea.X = x;
                RedrawTrackbar();
            }
            if (_soundselected)
            {
                int sndmin = splitContainer1.Panel2.Width - 68;
                _soundvolume = (e.X - sndmin) / 58.0;
                _soundvolume = _soundvolume < 0 ? 0 : _soundvolume > 1 ? 1 : _soundvolume;
                _sndarea.X = splitContainer1.Panel2.Width - 71 + (int)(58 * _soundvolume);
                DShowSupport.SetVolume(_soundvolume);
                RedrawTrackbar();
            }
            PlayState ps = DShowSupport.GetPlayState();
            if (ps == PlayState.Running || ps == PlayState.Paused && trackBarRect.Contains(e.Location))
            {
                double trackpercentage = (double)(e.X - _thumbarea.Width / 2) / (splitContainer1.Panel2.Width - _thumbarea.Width);
                msglabel.Text = translateTime(DShowSupport.GetDuration() * trackpercentage);
            }

        }

        private void splitContainer1_Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle trackBarRect = new Rectangle(_thumbarea.Width / 2, 3, Width - _thumbarea.Width, _thumbarea.Height);

            if (trackBarRect.Contains(e.Location))
            {
                _thumbselected = true;
                playTimer.Enabled = false;
            }
            if (_sndarea.Contains(e.Location))
            {
                _soundselected = true;
            }
        }

        private void splitContainer1_Panel2_MouseUp(object sender, MouseEventArgs e)
        {
            if (_thumbselected)
            {
                double trackpercentage = (double)(e.X - _thumbarea.Width / 2) / (splitContainer1.Panel2.Width - _thumbarea.Width);
                PlayState ps = DShowSupport.GetPlayState();
                if (ps == PlayState.Running || ps == PlayState.Paused)
                {
                    DShowSupport.Seek(DShowSupport.GetDuration() * trackpercentage);
                }
                else
                {
                    _thumbarea.X = 0;
                    RedrawTrackbar();
                }
            }

            _soundselected = false;
            _thumbselected = false;
            playTimer.Enabled = true;
        }

        private void playTimer_Tick(object sender, EventArgs e)
        {
            PlayState ps = DShowSupport.GetPlayState();
            if (ps == PlayState.Running || ps == PlayState.Paused)
            {
                double pos = DShowSupport.GetPlayerPos();
                double dur = DShowSupport.GetDuration();
                _thumbarea.X = (int)((splitContainer1.Panel2.Width - _thumbarea.Width) * (pos / dur));
                msglabel.Text = string.Format("{0}/{1}", translateTime(pos), translateTime(dur));
                RedrawTrackbar();
            }
            else
            {
                _thumbarea.X = 0;
                RedrawTrackbar();
            }
            DShowSupport.HandleGraphEvent();
        }

        private void RedrawTrackbar()
        {
            int gw = splitContainer1.Panel2.Width;

            BufferedGraphicsContext b = new BufferedGraphicsContext();
            BufferedGraphics bg = b.Allocate(g1, new Rectangle(0, 0, splitContainer1.Panel2.Width, splitContainer1.Panel2.Height));
            Graphics g = bg.Graphics;

            g.Clear(Color.Black);

            g.DrawImage(_trackleft, 0, 0);
            g.DrawImage(_trackmiddle, _trackleft.Width, 0, gw - _trackleft.Width, _trackmiddle.Height);
            g.DrawImage(_trackright, gw - _trackright.Width, 0);
            g.DrawImage(_trackthumb, _thumbarea);
            _sndarea.X = splitContainer1.Panel2.Width - 71 + (int)(58 * _soundvolume);
            g.DrawImage(_soundthumb, _sndarea);
            bg.Render(g1);

        }

        private static string translateTime(double time)
        {
            int sec = (int)Math.Floor(time);
            int h = sec / 3600;
            sec %= 3600;
            int m = sec / 60;
            sec %= 60;
            string msg = string.Format("{0}:{1}:{2}", h.ToString("D1"), m.ToString("D2"), sec.ToString("D2"));
            return msg;
        }

        public  void Init()
        {
            DShowSupport.InitPlayer(screen.Handle);
        }

        public  void Uninit()
        {
            DShowSupport.UninitPlayer();
        }

        public  void Pause()
        {
            PlayState ps = DShowSupport.GetPlayState();
            if (ps == PlayState.Running)
            {
                DShowSupport.TogglePause();
            }
        }

        public  void OpenVideo(string filename)
        {
            DShowSupport.PlayMovie(filename);
        }

        public  void Play()
        {
            PlayState ps = DShowSupport.GetPlayState();
            if (ps == PlayState.Paused || ps == PlayState.Stopped)
            {
                DShowSupport.TogglePause();
            }
        }

        public  void TogglePause()
        {
            DShowSupport.TogglePause(); 
        }

        public void Step()
        {
            DShowSupport.Step();
        }

        public double CurrentPosition
        {
            get { return DShowSupport.GetPlayerPos(); }
            set { DShowSupport.Seek(value); }
        }

        public bool Paused
        {
            get
            {
                PlayState ps = DShowSupport.GetPlayState();
                return ps == PlayState.Stopped || ps == PlayState.Paused;
            }
        }

        public bool MediaOpened
        {
            get
            {
                PlayState ps = DShowSupport.GetPlayState();
                return ps != PlayState.Init;
            }
        }

        public bool CanStep { get { return DShowSupport.CanStep(); } }

        public double Duration
        {
            get { return DShowSupport.GetDuration(); }
        }
    }

    static class DShowSupport
    {
        [DllImport("dshowplayer.dll")]
        public static extern bool InitPlayer(IntPtr hWnd);

        [DllImport("dshowplayer.dll", CharSet = CharSet.Unicode)]
        public static extern void PlayMovie([In] string filename);

        [DllImport("dshowplayer.dll")]
        public static extern void UninitPlayer();

        [DllImport("dshowplayer.dll")]
        public static extern Int32 HandleGraphEvent();

        [DllImport("dshowplayer.dll")]
        public static extern double GetPlayerPos();

        [DllImport("dshowplayer.dll")]
        public static extern double GetDuration();

        [DllImport("dshowplayer.dll", CharSet = CharSet.Unicode)]
        public static extern int GrabImage([In] string filename);

        [DllImport("dshowplayer.dll")]
        public static extern PlayState GetPlayState();

        [DllImport("dshowplayer.dll")]
        public static extern void Resize();

        [DllImport("dshowplayer.dll")]
        public static extern void TogglePause();

        [DllImport("dshowplayer.dll")]
        public static extern void Seek(double pos);

        [DllImport("dshowplayer.dll")]
        public static extern void SetVolume(double volume);

        [DllImport("dshowplayer.dll")]
        public static extern bool CanStep();

        [DllImport("dshowplayer.dll")]
        public static extern void Step();
    }
    enum PlayState { Stopped = 0, Paused = 1, Running = 2, Init = 3 }
}
