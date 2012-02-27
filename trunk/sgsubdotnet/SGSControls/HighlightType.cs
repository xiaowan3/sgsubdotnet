using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SGS.Controls
{
    public class HighlightType
    {
        public readonly string Name;
        public readonly Color Color;
        public readonly Font Font;

        public HighlightType(string name, Color color, Font font)
        {
            Name = name;
            Color = color;
            Font = font;
        }
    }
}
