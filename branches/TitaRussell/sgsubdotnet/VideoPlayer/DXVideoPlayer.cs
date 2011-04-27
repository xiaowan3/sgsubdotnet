using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;

namespace VideoPlayer
{
    public partial class DXVideoPlayer : UserControl
    {
        private Video m_movie = null;
        public DXVideoPlayer()
        {
            InitializeComponent();
            g1 = splitContainer1.Panel2.CreateGraphics();
            MediaOpened = false;

        }
        public bool Paused { get; set; }
        public bool MediaOpened { get; private set; }
        private string m_filename;
        private string m_videoname = "";
        private Image m_trackleft = Resources.PANEL_Left;
        private Image m_trackright = Resources.PANEL_Right;
        private Image m_trackmiddle = Resources.PANEL_Fill;
        private Image m_trackthumb = Resources.TRACK_Thumb;
        private Image m_soundthumb = Resources.SOUND_Thumb;
        private Rectangle m_thumbarea = new Rectangle(2, 3, Resources.TRACK_Thumb.Width, Resources.TRACK_Thumb.Height);
        private Rectangle m_sndarea = new Rectangle(0, 15, Resources.SOUND_Thumb.Width, Resources.SOUND_Thumb.Height);
        private double m_soundvolume = 0.5;

        private double m_aspectRatio = 1;


        private Graphics g1;


        public string Filename
        {
            get
            {
                return m_filename;
            }
        }

        public double Duration
        {
            get
            {
                if (m_movie != null) return m_movie.Duration;
                else return 0;
            }
        }

        public double CurrentPosition
        {
            get
            {
                if (m_movie != null) return m_movie.CurrentPosition;
                else return 0;
            }
            set
            {
                if (m_movie != null)
                {
                    if (value >= 0 && value < m_movie.Duration) m_movie.CurrentPosition = value;
                } 
            }
        }

        public void Pause()
        {
            if (m_movie != null)
            {
                m_movie.Pause();
                Paused = true;
            }
        }

        public void OpenVideo(string filename)
        {
            if (m_movie != null)
            {
                m_movie.Dispose();
                m_movie = null;
            }
            try
            {
                m_movie = new Video(filename);
                m_movie.Owner = screen;
                m_filename = filename;
                m_aspectRatio = (double)m_movie.Size.Width / (double)m_movie.Size.Height;
                resizeScreen(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);
                m_movie.Audio.Volume = ConvertVolume(m_soundvolume);
                m_videoname = m_filename.Substring(m_filename.LastIndexOf('\\') + 1);
                MediaOpened = true;
            }
            catch (Exception exception)
            {
                m_filename = "";
                m_videoname = "";
                MediaOpened = false;
                throw exception;
            }
        }

        private void resizeScreen(int width, int height)
        {
            if ((double)width / height > m_aspectRatio)
            {
                screen.Height = height;
                screen.Width = (int)(height * m_aspectRatio);
                screen.Location = new Point(
                    (splitContainer1.Panel1.Width - screen.Width) / 2, 0);
            }
            else
            {
                screen.Width = width;
                screen.Height = (int)(width / m_aspectRatio);
                screen.Location = new Point(
                    0, (splitContainer1.Panel1.Height - screen.Height) / 2);
            }

        }

        public void Play()
        {
            if (m_movie != null)
            {
                m_movie.Play();
                Paused = false;
            }
        }

        private void splitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            if (m_movie != null)
            {
                resizeScreen(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            redrawtrackbar();
        }

        private void redrawtrackbar()
        {
            int gw = splitContainer1.Panel2.Width;
            int gh = splitContainer1.Panel2.Height;

            BufferedGraphicsContext b;
            BufferedGraphics bg;
            Graphics g;

            b = new BufferedGraphicsContext();
            bg = b.Allocate(g1, new Rectangle(0, 0, splitContainer1.Panel2.Width, splitContainer1.Panel2.Height));
            g = bg.Graphics;

            g.Clear(Color.Black);

            g.DrawImage(m_trackleft, 0, 0);
            g.DrawImage(m_trackmiddle, m_trackleft.Width, 0, gw - m_trackleft.Width, m_trackmiddle.Height);
            g.DrawImage(m_trackright, gw - m_trackright.Width, 0);
            g.DrawImage(m_trackthumb, m_thumbarea);
            m_sndarea.X = splitContainer1.Panel2.Width - 71 + (int)(58 * m_soundvolume);
            g.DrawImage(m_soundthumb, m_sndarea);
            bg.Render(g1);

        }

        private bool m_thumbselected = false;
        private bool m_soundselected = false;

        private void splitContainer1_Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_thumbarea.Contains(e.Location))
            {
                m_thumbselected = true;
                playTimer.Enabled = false;
            }
            if(m_sndarea .Contains(e.Location))
            {
                m_soundselected = true;
            }
        }

        private void splitContainer1_Panel2_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_thumbselected)
            {
                double trackpercentage = (double)m_thumbarea.X / (splitContainer1.Panel2.Width - m_thumbarea.Width);
                if (m_movie != null)
                {
                    m_movie.CurrentPosition = m_movie.Duration * trackpercentage;
                }
                else
                {
                    m_thumbarea.X = 0;
                    redrawtrackbar();
                }
            }
            
            m_soundselected = false;
            m_thumbselected = false;
            playTimer.Enabled = true;
        }

        private void splitContainer1_Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_thumbselected)
            {
                int x = e.X - m_thumbarea.Width / 2;
                x = x < 0 ? 0 : x > splitContainer1.Panel2.Width - m_thumbarea.Width ? splitContainer1.Panel2.Width - m_thumbarea.Width : x;
                m_thumbarea.X = x;
                redrawtrackbar();
            }
            if (m_soundselected)
            {
                int sndmin = splitContainer1.Panel2.Width - 68;
                m_soundvolume = (e.X - sndmin) / 58.0;
                m_soundvolume = m_soundvolume < 0 ? 0 : m_soundvolume > 1 ? 1 : m_soundvolume;
                m_sndarea.X = splitContainer1.Panel2.Width - 71 + (int)(58 * m_soundvolume);
                if (m_movie != null)
                {
                    m_movie.Audio.Volume = ConvertVolume(m_soundvolume); ;
                }
                redrawtrackbar();
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
            if (m_movie != null)
            {
                m_thumbarea.X = (int)((splitContainer1.Panel2.Width - m_thumbarea.Width) * (m_movie.CurrentPosition / m_movie.Duration));
                msglabel.Text = translateTime(m_movie.CurrentPosition) + "/" + translateTime(m_movie.Duration);
                redrawtrackbar();
                
            }
        }

        private string translateTime(double time)
        {
            int sec = (int)Math.Floor(time);
            int h = sec / 3600;
            sec %= 3600;
            int m = sec / 60;
            sec %= 60;
            string msg = h.ToString("D1") + ":" + m.ToString("D2") + ":" + sec.ToString("D2");
            return msg;
        }

    }
}
