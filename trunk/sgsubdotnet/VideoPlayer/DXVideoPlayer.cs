using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;

namespace VideoPlayer
{
    public partial class DXVideoPlayer :UserControl,ISGSPlayer
    {
        private Video _movie;
        public DXVideoPlayer()
        {
            InitializeComponent();
            g1 = splitContainer1.Panel2.CreateGraphics();
            _mediaOpened = false;
        }

        private bool _mediaOpened;
        private readonly Image _trackleft = Resources.PANEL_Left;
        private readonly Image _trackright = Resources.PANEL_Right;
        private readonly Image _trackmiddle = Resources.PANEL_Fill;
        private readonly Image _trackthumb = Resources.TRACK_Thumb;
        private readonly Image _soundthumb = Resources.SOUND_Thumb;
        private Rectangle _thumbarea = new Rectangle(2, 3, Resources.TRACK_Thumb.Width, Resources.TRACK_Thumb.Height);
        private Rectangle _sndarea = new Rectangle(0, 15, Resources.SOUND_Thumb.Width, Resources.SOUND_Thumb.Height);
        private double _soundvolume = 0.5;

        private double _aspectRatio = 1;
    

        private Graphics g1;

        public string Filename { get; private set; }

        public void TogglePause()
        {
            if (_movie != null)
            {
                if (_movie.Paused)
                {
                    _movie.Play();
                }
                else
                {
                    _movie.Pause();
                }
            }
        }

        public double CurrentPosition
        {
            get
            {
                if (_movie != null) return _movie.CurrentPosition;
                return 0;
            }
            set
            {
                if (_movie != null)
                {
                    if (value >= 0 && value < _movie.Duration) _movie.CurrentPosition = value;
                }
            }
        }

        public bool Paused
        {
            get
            {
                if (_movie != null) return _movie.Paused;
                return false;
            }
        }

        public bool MediaOpened
        {
            get { return _mediaOpened; }
        }

        public double Duration
        {
            get
            {
                if (_movie != null) return _movie.Duration;
                return 0;
            }
        }


        public void Init()
        {
        }

        public void Uninit()
        {
        }

        public void Pause()
        {
            if (_movie != null)
            {
                _movie.Pause();
            }
        }

        public void OpenVideo(string filename)
        {
            if (_movie != null)
            {
                _movie.Dispose();
                _movie = null;
            }
            try
            {
                _movie = new Video(filename);
                _movie.Owner = screen;
                Filename = filename;
                _aspectRatio = _movie.Size.Width / (double)_movie.Size.Height;
                ResizeScreen(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);
                _movie.Audio.Volume = ConvertVolume(_soundvolume);
                Filename.Substring(Filename.LastIndexOf('\\') + 1);
                _mediaOpened = true;
            }
            catch (Exception)
            {
                Filename = "";
                _mediaOpened = false;
                throw;
            }
        }

        private void ResizeScreen(int width, int height)
        {
            if ((double)width / height > _aspectRatio)
            {
                screen.Height = height;
                screen.Width = (int)(height * _aspectRatio);
                screen.Location = new Point(
                    (splitContainer1.Panel1.Width - screen.Width) / 2, 0);
            }
            else
            {
                screen.Width = width;
                screen.Height = (int)(width / _aspectRatio);
                screen.Location = new Point(
                    0, (splitContainer1.Panel1.Height - screen.Height) / 2);
            }

        }

        public void Play()
        {
            if (_movie != null)
            {
                _movie.Play();
            }
        }

        private void splitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            if (_movie != null)
            {
                ResizeScreen(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            RedrawTrackbar();
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


        private bool _thumbselected;
        private bool _soundselected;

        private void splitContainer1_Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle trackBarRect = new Rectangle(_thumbarea.Width / 2, 3, Width - _thumbarea.Width, _thumbarea.Height);

            if (trackBarRect.Contains(e.Location))
            {
                _thumbselected = true;
                playTimer.Enabled = false;
            }
            if(_sndarea .Contains(e.Location))
            {
                _soundselected = true;
            }
        }

        private void splitContainer1_Panel2_MouseUp(object sender, MouseEventArgs e)
        {
            if (_thumbselected)
            {
                double trackpercentage = (double)(e.X-_thumbarea.Width / 2)/ (splitContainer1.Panel2.Width - _thumbarea.Width);
                if (_movie != null)
                {
                    _movie.CurrentPosition = _movie.Duration * trackpercentage;
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

        private void splitContainer1_Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle trackBarRect = new Rectangle(_thumbarea.Width/2, 3, Width - _thumbarea.Width, _thumbarea.Height);
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
                if (_movie != null)
                {
                    _movie.Audio.Volume = ConvertVolume(_soundvolume); 
                }
                RedrawTrackbar();
            }
            if (_movie != null && trackBarRect.Contains(e.Location))
            {
                double trackpercentage = (double)(e.X - _thumbarea.Width / 2) / (splitContainer1.Panel2.Width - _thumbarea.Width);
                msglabel.Text = translateTime(_movie.Duration * trackpercentage);
            }
        }

        private int ConvertVolume(double volume)
        {
            int v = -(int)(Math.Pow(10, (1 - volume))) * 1000 + 1000;
            if (v >= 0) v = 0;
            if (v <= -10000) v = -10000;
            return v;
        }


        private void playTimer_Tick(object sender, EventArgs e)
        {
            if (_movie != null)
            {
                _thumbarea.X = (int)((splitContainer1.Panel2.Width - _thumbarea.Width) * (_movie.CurrentPosition / _movie.Duration));
                msglabel.Text = string.Format("{0}/{1}", translateTime(_movie.CurrentPosition), translateTime(_movie.Duration));
                RedrawTrackbar();
                
            }
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
    }
}
