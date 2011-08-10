using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace VideoPlayer
{
    public partial class DShowPlayer : PlayerControl
    {
        public DShowPlayer()
        {
            InitializeComponent();
            g1 = splitContainer1.Panel2.CreateGraphics();
        }


        private readonly Image _trackleft = Resources.PANEL_Left;
        private readonly Image _trackright = Resources.PANEL_Right;
        private readonly Image _trackmiddle = Resources.PANEL_Fill;
        private readonly Image _trackthumb = Resources.TRACK_Thumb;
        private readonly Image _soundthumb = Resources.SOUND_Thumb;
        private Rectangle _thumbarea = new Rectangle(2, 3, Resources.TRACK_Thumb.Width, Resources.TRACK_Thumb.Height);
        private Rectangle _sndarea = new Rectangle(0, 15, Resources.SOUND_Thumb.Width, Resources.SOUND_Thumb.Height);
        private double _soundvolume = 0.5;

        private bool _thumbselected;
        private bool _soundselected;

        private Graphics g1;


        void screen_SizeChanged(object sender, System.EventArgs e)
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
                //if (_movie != null)
                //{
                //    _movie.Audio.Volume = ConvertVolume(_soundvolume);
                //}
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
                    SetPosition( DShowSupport.GetDuration() * trackpercentage);
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

        public override void Init()
        {
            DShowSupport.InitPlayer(screen.Handle);
        }

        public override void Uninit()
        {
            DShowSupport.UninitPlayer();
        }

        public override void Pause()
        {
            PlayState ps = DShowSupport.GetPlayState();
            if (ps == PlayState.Running)
            {
                DShowSupport.TogglePause();
            }
        }

        public override void OpenVideo(string filename)
        {
            DShowSupport.PlayMovie(filename);
        }

        public override void Play()
        {
            PlayState ps = DShowSupport.GetPlayState();
            if (ps == PlayState.Paused || ps == PlayState.Stopped)
            {
                DShowSupport.TogglePause();
            }
        }

        public override void TogglePause()
        {
            DShowSupport.TogglePause(); 
        }

        protected override double GetPosition()
        {
            return DShowSupport.GetPlayerPos();
        }

        protected override void SetPosition(double pos)
        {
          //  throw new NotImplementedException();
        }

        protected override bool IsPaused()
        {
            return false;
        }

        protected override bool IsMediaOpened()
        {
            return true;
        }

        protected override double GetDuration()
        {
            return DShowSupport.GetDuration();
        }
    }

    class DShowSupport
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
    }
    enum PlayState { Stopped = 0, Paused = 1, Running = 2, Init = 3 }
}
