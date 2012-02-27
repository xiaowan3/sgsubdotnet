using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace SGS.WaveReader
{
    public class WFMouseEventArgs : MouseEventArgs
    {
        public WFMouseEventArgs(MouseButtons btn, int clicks, int x, int y, int delta)
            : base(btn, clicks, x, y, delta)
        {
        }
        public WFMouseEventArgs(MouseEventArgs e, double time)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            Time = time;
        }

        public double Time;
    }

    public partial class WaveScope : UserControl
    {
        public double CurrentPosition { get; set; }
        public WaveForm Wave = null;
        public WaveScope()
        {
            InitializeComponent();
            startpos = (int)(Width * 0.75);
        }
        public double LastStart = 0;
        public double LastEnd = 0;
        public double Start = 0;
        public double End = 0;
        private Pen bgPen = new Pen(Color.Black);
        private Pen gridPen = new Pen(Color.Red);
        private Pen hLinePen = new Pen(Color.DarkGray);
        private Pen linePen = new Pen(Color.Blue);
        private Pen lastPen = new Pen(Color.DarkGray);
        private Pen lastPenT = new Pen(Color.FromArgb(60, 60, 110), 11);
        private Pen curPen = new Pen(Color.Green);
        private Pen curPenT = new Pen(Color.Green, 6);

        public override Size MaximumSize
        {
            get
            {
                return base.MaximumSize;
            }
            set
            {
                base.MaximumSize = new Size(value.Width, 120);
            }
        }
        public override Size MinimumSize
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                base.MinimumSize = new Size(value.Width, 120);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Redraw();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            startpos = (int)(Width * 0.75);
        }

        protected int startpos;
        public void Redraw()
        {
            Graphics g1 = CreateGraphics();
            BufferedGraphicsContext b = new BufferedGraphicsContext();
            BufferedGraphics bg = b.Allocate(g1, new Rectangle(0, 0, Width, Height));
            Bitmap img = new Bitmap(Width,100);
            Graphics g = bg.Graphics;
            g.Clear(Color.Black);
            if (Wave != null)
            {
                BitmapData bd = img.LockBits(new Rectangle(0,0,Width,100),ImageLockMode.ReadWrite,PixelFormat.Format24bppRgb); 
                int pitch = Math.Abs(bd.Stride);
                Byte[] imgbyte = new byte[100 * pitch];
                for (int i = 0; i < Width; i++)
                {
                    Byte[] v = Wave.ValueAt((i - startpos) * Wave.DeltaT + CurrentPosition);
                    for (int j = 0; j < 100; j++)
                    {
                        imgbyte[j * pitch + i* 3] = v[j];
                        imgbyte[j * pitch + i * 3 + 1] = v[j];
                        imgbyte[j * pitch + i * 3 + 2] = v[j];
                    }

                }
                System.Runtime.InteropServices.Marshal.Copy(imgbyte, 0, bd.Scan0, 100 * pitch);
                img.UnlockBits(bd);
                g.DrawImage(img, 0, 0);
                int lasts = (int)((LastStart - CurrentPosition) / Wave.DeltaT) + startpos;
                int laste = (int)((LastEnd - CurrentPosition) / Wave.DeltaT) + startpos;
                int st = (int)((Start - CurrentPosition) / Wave.DeltaT) + startpos;
                int ed = (int)((End - CurrentPosition) / Wave.DeltaT) + startpos;
                g.DrawLine(hLinePen, 0, 100, Width, 100);
                if (lasts < Width || laste > 0)
                {
                    g.DrawLine(lastPen, lasts, 0, lasts, Height);
                    if (laste > lasts)
                    {
                        g.DrawLine(lastPenT, lasts, 110, laste, 110);
                        g.DrawLine(lastPen, laste, 0, laste, Height);
                    }
                    else
                    {
                        g.DrawLine(lastPenT, lasts, 110, lasts + 10, 110);
                    }
                }
                if (st < Width || ed > 0)
                {
                    g.DrawLine(curPen, st, 0, st, Height);
                    if (ed > st)
                    {
                        g.DrawLine(curPenT, st, 110, ed, 110);
                        g.DrawLine(curPen, ed, 0, ed, Height);
                    }
                    else
                    {
                        g.DrawLine(curPenT, st, 110, st + 10, 110);
                    }

                }
                g.DrawLine(gridPen, startpos, 0, startpos, Height);
            }
            bg.Render(g1);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {

            base.OnMouseDown(e);
            if (WSMouseDown != null && Wave != null)
            {
                WSMouseDown(this, new WFMouseEventArgs(e, CurrentPosition - Wave.DeltaT * (startpos - e.X)));
            }
        }

        /// <summary>
        /// Occurs when mouse button is pushed down.
        /// </summary>
        public event EventHandler<WFMouseEventArgs> WSMouseDown = null;


    }
}
