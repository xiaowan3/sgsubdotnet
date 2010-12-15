using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WaveReader
{
    public partial class WaveScope : UserControl
    {
        public double CurrentPosition { get; set; }
        public WaveForm Wave = null;
        public WaveScope()
        {
            InitializeComponent();
        }
        private Pen bgPen = new Pen(Color.Black);
        private Pen gridPen = new Pen(Color.Red);
        private Pen linePen = new Pen(Color.Blue);
        protected override void OnPaint(PaintEventArgs e)
        {

            Redraw();
        }
        public void Redraw()
        {
            Graphics g1 = CreateGraphics();
            BufferedGraphicsContext b = new BufferedGraphicsContext();
            BufferedGraphics bg = b.Allocate(g1, new Rectangle(0, 0, Width, Height));

            Graphics g = bg.Graphics;
            g.Clear(Color.Black);
            int startpos = (int)(Width * 0.75);
            if (Wave != null)
            {
                for (int i = 0; i < Width; i++)
                {
                    int v = (int)(50 * Wave.ValueAt((i - startpos) * Wave.DeltaT + CurrentPosition));
                    g.DrawLine(linePen, i, 50 - v, i, 50 + v);
                }
                g.DrawLine(gridPen, startpos, 0, startpos, Height);
            }
            bg.Render(g1);
        }
    }
}
