using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;
using SGSDatatype;

namespace SGSControls
{
    /// <summary>
    /// A textbox the does syntax highlighting.
    /// </summary>
    public class SyntaxHighlightingTextBox : System.Windows.Forms.RichTextBox
    {

        //public SyntaxHighlightingTextBox()
        //{
        //    HighlightTypes.Add(new HighlightType("Text", Color.Black, null));
        //    HighlightTypes.Add(new HighlightType("Text", Color.Red, null));
        //    HighlightTypes.Add(new HighlightType("Text", Color.Blue, null));
        //    HighlightTypes.Add(new HighlightType("Text", Color.Gray, null));
        //}


        #region Members

        //Members exposed via properties

        private HightlightTypeColection _highlightTypes = new HightlightTypeColection();

        private readonly List<char> _separators = new List<char>();
        //Internal use members

        private bool _parsing;

        //Undo/Redo members
        private ArrayList _undoList = new ArrayList();
        private Stack _redoStack = new Stack();
        private bool _isUndo;
        private UndoRedoInfo _lastInfo = new UndoRedoInfo("", new Win32.POINT(), 0);
        private int _maxUndoRedoSteps = 50;
        private SGSConfig _config;

        private HighlightType _hlUnknown;
        private HighlightType _hlComment;
        private HighlightType _hlToolong;
        private HighlightType _hlUncertain;



        #endregion

        #region Properties


        public SyntaxHighlightingTextBox()
        {


        }

        public void SetConfig(SGSConfig config)
        {
            _config = config;
            RefreshConfig();

            _hlUnknown = new HighlightType("Text", Color.Red, null);
            HighlightTypes.Add(_hlUnknown);
            _hlComment = new HighlightType("Text", Color.Gray, null);
            HighlightTypes.Add(_hlComment);
            _hlUncertain = new HighlightType("Text", Color.Blue, null);
            HighlightTypes.Add(_hlUncertain);
            _hlToolong = new HighlightType("Text", Color.Pink, null);
            HighlightTypes.Add(_hlToolong);
        }

        public void RefreshConfig()
        {
            if(_config == null) throw new Exception("config is null");
            _separators.Clear();
            _separators.Add(_config.UnknownPlaceholder);
            _separators.Add(_config.CommentSeparator);
            _separators.Add(_config.UncertainLeftSeparator);
            _separators.Add(_config.UncertainRightSeparator);
        }

        /// <summary>
        /// Set the maximum amount of Undo/Redo steps.
        /// </summary>
        [Category("Behavior")]
        public int MaxUndoRedoSteps
        {
            get
            {
                return _maxUndoRedoSteps;
            }
            set
            {
                _maxUndoRedoSteps = value;
            }
        }



        /// <summary>
        /// The collection of highlight descriptors.
        /// </summary>
        /// 
        public HightlightTypeColection HighlightTypes
        {
            get
            {
                return _highlightTypes;
            }
        }

        #endregion

        #region Overriden methods

        /// <summary>
        /// The on text changed overrided. Here we parse the text into RTF for the highlighting.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            if (_config == null) return;
            if (_parsing) return;
            _parsing = true;
            Win32.LockWindowUpdate(Handle);
            base.OnTextChanged(e);

            if (!_isUndo)
            {
                _redoStack.Clear();
                _undoList.Insert(0, _lastInfo);
                LimitUndo();
                _lastInfo = new UndoRedoInfo(Text, GetScrollPos(), SelectionStart);
            }

            //Save scroll bar an cursor position, changeing the RTF moves the cursor and scrollbars to top positin
            Win32.POINT scrollPos = GetScrollPos();
            int cursorLoc = SelectionStart;

            //Created with an estimate of how big the stringbuilder has to be...
            var sb = new StringBuilder((int)(Text.Length * 1.5 + 150));

            //Adding RTF header
            sb.Append(@"{\rtf1\fbidis\ansi\ansicpg936\deff0\deflang1033\deflangfe2052{\fonttbl{");

            ////Font table creation
            int fontCounter = 0;
            Hashtable fonts = new Hashtable();
            AddFontToTable(sb, Font, ref fontCounter, fonts);
            foreach (HighlightType highlightType in _highlightTypes)
            {
                if ((highlightType.Font != null) && !fonts.ContainsKey(highlightType.Font.Name))
                {
                    AddFontToTable(sb, highlightType.Font, ref fontCounter, fonts);
                }
            }
            sb.Append("}\n");

            ////ColorTable

            sb.Append(@"{\colortbl ;");
            Hashtable colors = new Hashtable();
            int colorCounter = 1;
            AddColorToTable(sb, ForeColor, ref colorCounter, colors);
            AddColorToTable(sb, BackColor, ref colorCounter, colors);

            foreach (HighlightType highlightType in _highlightTypes)
            {
                if (!colors.ContainsKey(highlightType.Color))
                {
                    AddColorToTable(sb, highlightType.Color, ref colorCounter, colors);
                }
            }

            //Parsing text

            sb.Append("}\n").Append(@"\viewkind4\uc1\pard\ltrpar");
            SetDefaultSettings(sb, colors, fonts);

            string[] lines = Text.Split('\n');
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                if (lineIndex != 0)
                {
                    AddNewLine(sb);
                }
                int charIndex = 0;
                while (charIndex < lines[lineIndex].Length)
                {
                    int segStart;
                    if(lines[lineIndex].Length > 14) //Too long.
                    {
                        SetHighlightTypeSettings(sb, _hlToolong, colors, fonts);
                        sb.Append(StrToRtf(lines[lineIndex]));
                        SetDefaultSettings(sb, colors, fonts);
                        break;
                    }
                    else if (lines[lineIndex][charIndex] == _config.UnknownPlaceholder) //Window
                    {
                        segStart = charIndex;

                        do
                        {
                            charIndex ++;
                        } while (charIndex < lines[lineIndex].Length && 
                            lines[lineIndex][charIndex] == _config.UnknownPlaceholder);

                        SetHighlightTypeSettings(sb, _hlUnknown, colors, fonts);
                        sb.Append(lines[lineIndex].Substring(segStart, charIndex - segStart));
                        SetDefaultSettings(sb, colors, fonts);
                    }
                    else if (lines[lineIndex][charIndex] == _config.CommentSeparator)  //Comment
                    {
                        SetHighlightTypeSettings(sb, _hlUncertain, colors, fonts);
                        sb.Append(StrToRtf(lines[lineIndex].Substring(charIndex)));
                        SetDefaultSettings(sb, colors, fonts);
                        break;
                    }
                    else
                    {
                        segStart = charIndex;
                        while (charIndex < lines[lineIndex].Length && !_separators.Contains(lines[lineIndex][charIndex]))
                        {
                            charIndex++;
                        }
                        sb.Append(StrToRtf(lines[lineIndex].Substring(segStart, charIndex - segStart)));
                    }
                }


            }
            //			System.Diagnostics.Debug.WriteLine(sb.ToString());
            Rtf = sb.ToString();

            //Restore cursor and scrollbars location.
            SelectionStart = cursorLoc;

            _parsing = false;

            SetScrollPos(scrollPos);
            Win32.LockWindowUpdate((IntPtr)0);
            Invalidate();



        }

        ///   <summary>
        /// ½«×Ö·û´®×ª»»³ÉRTF±àÂë
        ///   </summary>
        ///   <param   name= "str"> ×Ö·û´® </param>
        ///   <returns> ½«×Ö·û´®×ª»»³É´¿ASCIIµÄ±àÂë </returns>
        private static string StrToRtf(string str)
        {

            int length = str.Length;
            const int z = (int)'z';
            var ret = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                char ch = str[i];

                switch (ch)
                {
                    case '\\':
                        ret.Append("\\\\");
                        break;
                    case '\n':
                        ret.Append("\\par ");
                        break;
                    default:
                        if (ch > z)
                        {
                            //   Gets   the   encoding   for   the   specified   code   page.
                            Encoding targetEncoding = Encoding.Default;

                            //   Gets   the   byte   representation   of   the   specified   string.
                            byte[] encodedChars = targetEncoding.GetBytes(str[i].ToString());

                            for (int j = 0; j < encodedChars.Length; j++)
                            {
                                string st = encodedChars[j].ToString();
                                ret.Append("\\'").Append(int.Parse(st).ToString("X"));
                            }
                        }
                        else
                        {
                            ret.Append(ch);
                        }
                        break;
                }
            }
            return ret.ToString();
        }

        protected override void OnVScroll(EventArgs e)
        {
            if (_parsing) return;
            base.OnVScroll(e);
        }


        /// <summary>
        /// Taking care of Keyboard events
        /// </summary>
        /// <param name="m"></param>
        /// <remarks>
        /// Since even when overriding the OnKeyDown methoed and not calling the base function 
        /// you don't have full control of the input, I've decided to catch windows messages to handle them.
        /// </remarks>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.WM_PAINT:
                    {
                        //Don't draw the control while parsing to avoid flicker.
                        if (_parsing)
                        {
                            return;
                        }
                        break;
                    }
                case Win32.WM_KEYDOWN:
                    {
                        {

                            if (((Keys)(int)m.WParam == Keys.Z) &&
                                ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) != 0))
                            {
                                Undo();
                                return;
                            }
                            if (((Keys)(int)m.WParam == Keys.Y) &&
                                ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) != 0))
                            {
                                Redo();
                                return;
                            }
                        }
                        break;
                    }
                case Win32.WM_CHAR:
                    {
                        switch ((Keys)(int)m.WParam)
                        {
                            case Keys.Space:
                                if ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) != 0)
                                {
                                    return;
                                }
                                break;
                        }
                    }
                    break;

            }
            base.WndProc(ref m);
        }


        #endregion

        #region Undo/Redo Code
        public new bool CanUndo
        {
            get
            {
                return _undoList.Count > 0;
            }
        }
        public new bool CanRedo
        {
            get
            {
                return _redoStack.Count > 0;
            }
        }

        private void LimitUndo()
        {
            while (_undoList.Count > _maxUndoRedoSteps)
            {
                _undoList.RemoveAt(_maxUndoRedoSteps);
            }
        }

        public new void Undo()
        {
            if (!CanUndo)
                return;
            _isUndo = true;
            _redoStack.Push(new UndoRedoInfo(Text, GetScrollPos(), SelectionStart));
            UndoRedoInfo info = (UndoRedoInfo)_undoList[0];
            _undoList.RemoveAt(0);
            Text = info.Text;
            SelectionStart = info.CursorLocation;
            SetScrollPos(info.ScrollPos);
            _lastInfo = info;
            _isUndo = false;
        }
        public new void Redo()
        {
            if (!CanRedo)
                return;
            _isUndo = true;
            _undoList.Insert(0, new UndoRedoInfo(Text, GetScrollPos(), SelectionStart));
            LimitUndo();
            UndoRedoInfo info = (UndoRedoInfo)_redoStack.Pop();
            Text = info.Text;
            SelectionStart = info.CursorLocation;
            SetScrollPos(info.ScrollPos);
            _isUndo = false;
        }

        private class UndoRedoInfo
        {
            public UndoRedoInfo(string text, Win32.POINT scrollPos, int cursorLoc)
            {
                Text = text;
                ScrollPos = scrollPos;
                CursorLocation = cursorLoc;
            }
            public readonly Win32.POINT ScrollPos;
            public readonly int CursorLocation;
            public readonly string Text;
        }
        #endregion


        #region Rtf building helper functions

        /// <summary>
        /// Set color and font to default control settings.
        /// </summary>
        /// <param name="sb">the string builder building the RTF</param>
        /// <param name="colors">colors hashtable</param>
        /// <param name="fonts">fonts hashtable</param>
        private void SetDefaultSettings(StringBuilder sb, Hashtable colors, Hashtable fonts)
        {
            SetColor(sb, ForeColor, colors);
            SetFont(sb, Font, fonts);
            SetFontSize(sb, (int)Font.Size);
            EndTags(sb);
        }


        /// <summary>
        /// Set Color and font to a highlight type settings.
        /// </summary>
        /// <param name="sb">the string builder building the RTF</param>
        /// <param name="ht">the HighlightType with the font and color settings to apply.</param>
        /// <param name="colors">colors hashtable</param>
        /// <param name="fonts">fonts hashtable</param>
        private void SetHighlightTypeSettings(StringBuilder sb, HighlightType ht, Hashtable colors, Hashtable fonts)
        {
            SetColor(sb, ht.Color, colors);
            if (ht.Font != null)
            {
                SetFont(sb, ht.Font, fonts);
                SetFontSize(sb, (int)ht.Font.Size);
            }
            EndTags(sb);
        }
        /// <summary>
        /// Sets the color to the specified color
        /// </summary>
        private void SetColor(StringBuilder sb, Color color, Hashtable colors)
        {
            sb.Append(@"\cf").Append(colors[color]);
        }
        /// <summary>
        /// Sets the backgroung color to the specified color.
        /// </summary>
        private void SetBackColor(StringBuilder sb, Color color, Hashtable colors)
        {
            sb.Append(@"\cb").Append(colors[color]);
        }
        /// <summary>
        /// Sets the font to the specified font.
        /// </summary>
        private void SetFont(StringBuilder sb, Font font, Hashtable fonts)
        {
            if (font == null) return;
            sb.Append(@"\f").Append(fonts[font.Name]);
        }
        /// <summary>
        /// Sets the font size to the specified font size.
        /// </summary>
        private void SetFontSize(StringBuilder sb, int size)
        {
            sb.Append(@"\fs").Append(size * 2);
        }
        /// <summary>
        /// Adds a newLine mark to the RTF.
        /// </summary>
        private void AddNewLine(StringBuilder sb)
        {
            sb.Append("\\par\n");
        }

        /// <summary>
        /// Ends a RTF tags section.
        /// </summary>
        private void EndTags(StringBuilder sb)
        {
            sb.Append(' ');
        }

        /// <summary>
        /// Adds a font to the RTF's font table and to the fonts hashtable.
        /// </summary>
        /// <param name="sb">The RTF's string builder</param>
        /// <param name="font">the Font to add</param>
        /// <param name="counter">a counter, containing the amount of fonts in the table</param>
        /// <param name="fonts">an hashtable. the key is the font's name. the value is it's index in the table</param>
        private void AddFontToTable(StringBuilder sb, Font font, ref int counter, Hashtable fonts)
        {

            sb.Append(@"\f").Append(counter).Append(@"\fnil\fcharset134 ").Append(StrToRtf(font.Name)).Append(";}");
            fonts.Add(font.Name, counter++);
        }

        /// <summary>
        /// Adds a color to the RTF's color table and to the colors hashtable.
        /// </summary>
        /// <param name="sb">The RTF's string builder</param>
        /// <param name="color">the color to add</param>
        /// <param name="counter">a counter, containing the amount of colors in the table</param>
        /// <param name="colors">an hashtable. the key is the color. the value is it's index in the table</param>
        private void AddColorToTable(StringBuilder sb, Color color, ref int counter, Hashtable colors)
        {

            sb.Append(@"\red").Append(color.R).Append(@"\green").Append(color.G).Append(@"\blue")
                .Append(color.B).Append(";");
            colors.Add(color, counter++);
        }

        #endregion

        #region Scrollbar positions functions
        /// <summary>
        /// Sends a win32 message to get the scrollbars' position.
        /// </summary>
        /// <returns>a POINT structore containing horizontal and vertical scrollbar position.</returns>
        private unsafe Win32.POINT GetScrollPos()
        {
            Win32.POINT res = new Win32.POINT();
            IntPtr ptr = new IntPtr(&res);
            Win32.SendMessage(Handle, Win32.EM_GETSCROLLPOS, 0, ptr);
            return res;

        }

        /// <summary>
        /// Sends a win32 message to set scrollbars position.
        /// </summary>
        /// <param name="point">a POINT conatining H/Vscrollbar scrollpos.</param>
        private unsafe void SetScrollPos(Win32.POINT point)
        {
            IntPtr ptr = new IntPtr(&point);
            Win32.SendMessage(Handle, Win32.EM_SETSCROLLPOS, 0, ptr);

        }
        #endregion
    }

}