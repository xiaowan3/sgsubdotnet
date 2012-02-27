using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace SGS.VideoPlayer
{
    public partial class MPlayer : UserControl, ISGSPlayer
    {
        public MPlayer()
        {
            InitializeComponent();
            g1 = splitContainer1.Panel2.CreateGraphics();
        }


        private readonly Image _trackleft = Resources.PANEL_Left;
        private readonly Image _trackright = Resources.PANEL_MPRight;
        private readonly Image _trackmiddle = Resources.PANEL_Fill;
        private readonly Image _trackthumb = Resources.TRACK_Thumb;
        private readonly Image _soundthumb = Resources.SOUND_Thumb;
        private Rectangle _thumbarea = new Rectangle(2, 3, Resources.TRACK_Thumb.Width, Resources.TRACK_Thumb.Height);
        private Rectangle _sndarea = new Rectangle(0, 15, Resources.SOUND_Thumb.Width, Resources.SOUND_Thumb.Height);
        private double _soundvolume = 0.5;

        private bool _thumbselected;
        private bool _soundselected;

        private Graphics g1;

        #region mplayer members

        public string MPlayerPath
        {
            set
            {
                _mplayerPath = value;
                _mplayerWorkingDir = _mplayerPath.Substring(0,_mplayerPath.LastIndexOf('\\') + 1);
            }
        }

        private string _mplayerPath;
        private string _mplayerWorkingDir;

        private Thread _mplayerController;
        private Process _mplayerProcess;
        private StreamReader _mplayerOut;
        private StreamWriter _mplayerIn;
        private ReaderWriterLock _timePosLock = new ReaderWriterLock();

        private ManualResetEvent _readDurEvent = new ManualResetEvent(false);
        private ManualResetEvent _waitStartEvent = new ManualResetEvent(false);
        private ManualResetEvent _readPauseEvent = new ManualResetEvent(false);
        private bool _waitStart;
        private bool _mediaOpened;
        private double _timePos
        {
            get
            {
                _timePosLock.AcquireReaderLock(1000);
                double t = _timePosValue;
                _timePosLock.ReleaseReaderLock();
                return t;
            }
            set
            {
                _timePosLock.AcquireWriterLock(1000);
                _timePosValue = value;
                _timePosLock.ReleaseWriterLock();
            }
        }
        private double _timePosValue;

        #endregion


        void screen_SizeChanged(object sender, EventArgs e)
        {
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
                string cmd = string.Format("volume {0} 1", _soundvolume*100);
                if (MediaOpened) _mplayerIn.WriteLine(cmd);
                RedrawTrackbar();
            }
            if (MediaOpened && trackBarRect.Contains(e.Location))
            {
                double trackpercentage = (double)(e.X - _thumbarea.Width / 2) / (splitContainer1.Panel2.Width - _thumbarea.Width);
                msglabel.Text = translateTime(Duration * trackpercentage);
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
                double trackpercentage = (double) (e.X - _thumbarea.Width/2)/
                                         (splitContainer1.Panel2.Width - _thumbarea.Width);
                if (MediaOpened)
                {
                    CurrentPosition = trackpercentage*Duration;
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
            if (MediaOpened)
            {
                _thumbarea.X = (int)((splitContainer1.Panel2.Width - _thumbarea.Width) * (_timePos / Duration));
                msglabel.Text = string.Format("{0}/{1}", translateTime(_timePos), translateTime(Duration));
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

        public void Init()
        {
        }

        public  void Uninit()
        {
            if(_mplayerProcess != null && !_mplayerProcess .HasExited)
            {
                _mplayerIn.WriteLine("quit");
                if(!_mplayerProcess.WaitForExit(500))
                {
                    _mplayerProcess.Kill();
                }
            }
        }

        public  void Pause()
        {
            if (MediaOpened)
            {
                if (!Paused) _mplayerIn.WriteLine("pause");
            }
        }

        public  void OpenVideo(string filename)
        {
            _waitStart = true;
            _waitStartEvent.Reset();
            if (_mplayerProcess == null  || _mplayerProcess.HasExited)
            {
                ThreadStart ts = new ThreadStart(mpctThread);
                _mplayerController = new Thread(ts);
                string arg =
                    string.Format(
                        "-nomouseinput -colorkey 0x010101 -noquiet -nofs -slave -vo directx -ao dsound -priority abovenormal -framedrop -wid {0} \"{1}\" -loop 0",
                        screen.Handle.ToInt32(), filename);
                _mplayerProcess = new Process
                                      {
                                          StartInfo =
                                              {
                                                  FileName = _mplayerPath,
                                                  Arguments = arg,
                                                  CreateNoWindow = true,
                                                  RedirectStandardOutput = true,
                                                  RedirectStandardInput = true,
#if DEBUG
                                                  RedirectStandardError = true,
#endif
                                                  UseShellExecute = false,
                                                  WorkingDirectory = _mplayerWorkingDir
                                              },
                                          EnableRaisingEvents = true
                                      };
                _mplayerProcess.Exited += new EventHandler(mplayerProcess_Exited);
                _mplayerProcess.Start();
                _mplayerOut = _mplayerProcess.StandardOutput;
                _mplayerIn = _mplayerProcess.StandardInput;
                _mplayerController.Start();
            }
            else
            {
                string fn = filename.Replace('\\', '/');
                string cmd = string.Format("loadfile \"{0}\" 0", fn);
                _mplayerIn.WriteLine(cmd);
            }
            _mediaOpened = true;
            _durRead = false;
            _waitStartEvent.WaitOne(1000);
            playTimer.Start();
            _mplayerIn.WriteLine("loop 0 1");
        }

        private void mpctThread()
        {
            while (_mplayerProcess != null && !_mplayerProcess.HasExited)
            {
                string str = _mplayerOut.ReadLine();
                if (str == null) continue;
                int sega = str.IndexOf("A:");
                int segv = str.IndexOf("V:");
                if (sega == 0 || segv == 0)
                {
                    int numst = 2;
                    int numed;
                    while (!char.IsDigit(str, numst) && str[numst] != '.') 
                    {
                        numst++;
                    }
                    numed = numst;
                    while (char.IsDigit(str, numed) || str[numed] == '.')
                    {
                        numed++;
                    }
                    _timePos = Double.Parse(str.Substring(numst, numed - numst));
                    if (_waitStart) _waitStartEvent.Set();
                    continue;
                }
                int eq = str.IndexOf('=');
                if (eq > 3 && str.Substring(0, 3) == "ANS")
                {
                    string val = str.Substring(eq + 1);
                    string para = str.Substring(0, eq);
                    switch (para)
                    {
                        case "ANS_pause":
                            _paused = val.Trim() == "yes";
                            _readPauseEvent.Set();
                            break;
                        case "ANS_LENGTH":
                            _duration = double.Parse(val);
                            _readDurEvent.Set();
                            break;
                        case "ANS_TIME_POSITION":
                            _timePos = Double.Parse(val);
                            break;
                    }
                }
            }

        }

        private void mplayerProcess_Exited(object sender, EventArgs e)
        {
            _mediaOpened = false;
#if DEBUG
            string str = _mplayerProcess.StandardError.ReadToEnd();
            Debug.Write(str);
#endif
            _mplayerProcess = null;
            _readDurEvent.Set();
            _readPauseEvent.Set();
            playTimer.Stop();
        }

        public  void Play()
        {
            if (MediaOpened)
            {
                if (Paused) _mplayerIn.WriteLine("pause");
            }
        }

        public  void TogglePause()
        {
            if (MediaOpened)
            {
                _mplayerIn.WriteLine("pause");
            }
        }

        public void Step()
        {
            if(MediaOpened)
            {
                _mplayerIn.WriteLine("frame_step");
            }
        }

        public double CurrentPosition
        {
            get { return _timePos; }
            set
            {
                if (value < Duration)
                {
                    string cmd = string.Format("pausing_keep_force seek {0}", (value - _timePos).ToString("F2"));
                    _mplayerIn.WriteLine(cmd);
                    _mplayerIn.WriteLine("get_time_pos");
                }
            }
        }

        private bool _paused;
        public bool Paused
        {
            get
            {
                if (MediaOpened)
                {
                    _readPauseEvent.Reset();
                    _mplayerIn.WriteLine("pausing_keep_force get_property pause");
                    _readPauseEvent.WaitOne();
                    return _paused;
                }
                 return false;
            }
        }

        public bool MediaOpened
        {
            get { return (_mplayerProcess != null && !_mplayerProcess.HasExited) && _mediaOpened; }
        }

        public bool CanStep { get { return true; } }

        private double _duration;
        private bool _durRead;
        public double Duration
        {
            get
            {
                if (_durRead) return _duration;
                if (MediaOpened)
                {
                    _readDurEvent.Reset();
                    _mplayerIn.WriteLine("pausing_keep_force get_time_length");
                    _readDurEvent.WaitOne();
                    _durRead = true;
                    return _duration;
                }
                return 0;
            }
        }
    }

 
}
