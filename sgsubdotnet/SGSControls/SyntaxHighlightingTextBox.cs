using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Security.AccessControl;
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

        private HighlightType _hlHole;
        private HighlightType _hlComment;
        private HighlightType _hlToolong;
        private HighlightType _hlUncertain;
        private HighlightType _hlTimeTag;
        private HighlightType _hlLiteralLine;

        private bool _saved = true;


        #endregion

        #region Properties


        public SyntaxHighlightingTextBox()
        {

        }

        public void SetConfig(SGSConfig config)
        {
            _config = config;
            RefreshConfig();

            _hlHole = new HighlightType("Text", Color.Red, null);
            HighlightTypes.Add(_hlHole);
            _hlComment = new HighlightType("Text", Color.Gray, null);
            HighlightTypes.Add(_hlComment);
            _hlUncertain = new HighlightType("Text", Color.DarkRed, null);
            HighlightTypes.Add(_hlUncertain);
            _hlToolong = new HighlightType("Text", Color.Pink, null);
            HighlightTypes.Add(_hlToolong);
            _hlTimeTag = new HighlightType("Text", Color.Green, null);
            HighlightTypes.Add(_hlTimeTag);
            _hlLiteralLine = new HighlightType("Text", Color.DarkBlue, null);
            HighlightTypes.Add(_hlLiteralLine);
        }

        public void RefreshConfig()
        {
            if (_config == null) throw new Exception("config is null");
            _separators.Clear();
            _separators.Add(_config.HolePlaceholder);
            _separators.Add(_config.CommentMark);
            _separators.Add(_config.UncertainLeftMark);
  
        }

        public void SetSaved()
        {
            _saved = true;
        }

        public void Save(string filename)
        {
            var file = new FileStream(filename, FileMode.Create, FileAccess.Write);
            var ostream = new StreamWriter(file, Encoding.Unicode);
            ostream.Write(Text);
            ostream.Flush();
            file.Flush();
            ostream.Close();
            file.Close();
            _saved = true;
        }

        public void Open(string filename)
        {
            var file = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(file, Encoding.Default, true);
            Text = reader.ReadToEnd();
        }

        public void Export(string filename)
        {
            var file = new FileStream(filename, FileMode.Create, FileAccess.Write);
            var writer = new StreamWriter(file, Encoding.Unicode);
            string[] lines = Text.Split('\n');
            foreach (string line in lines)
            {
                string currentline;
                if (line.Length > 0 && line[0] == _config.LiteralLineMark)
                {
                    currentline = line.Substring(1);
                }
                else
                {
                    currentline = line;
                    int c = line.IndexOf(_config.CommentMark);
                    int mark;
                    if (c != -1) currentline = line.Substring(0, c);
                    while ((mark = currentline.IndexOf(_config.UncertainLeftMark)) != -1)
                    {
                        currentline = currentline.Remove(mark, 1);
                    }
                    while ((mark = currentline.IndexOf(_config.UncertainRightMark)) != -1)
                    {
                        currentline = currentline.Remove(mark, 1);
                    }
                }
                writer.WriteLine(currentline);

            }
            writer.Flush();
            file.Flush();
            writer.Close();
            file.Close();
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

        public bool Saved { get { return _saved; } }



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

        #region Events

        public event EventHandler<SummaryEventArgs> RefreshSummary = null;
        public event EventHandler<SeekEventArgs> SeekPlayer = null;
        public event EventHandler<PlayerControlEventArgs> PlayerControl = null;
        public event EventHandler<TimeEditEventArgs> TimeEdit = null;
        public event EventHandler CheckAutosave = null;
        #endregion

        #region Overriden methods

        /// <summary>
        /// The on text changed overrided. Here we parse the text into RTF for the highlighting.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            _saved = false;
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
            int linecount = lines.Length;
            var summaryEventArgs = new SummaryEventArgs { Lines = linecount };
            for (int lineIndex = 0; lineIndex < linecount; lineIndex++)
            {
                bool window = false;
                bool uncertain = false;
                bool toolong = false;
                var tokens = new List<SyntaxToken>();
                int lineLen = 0;
                if (lineIndex != 0)
                {
                    AddNewLine(sb);
                }
                int charIndex = 0;
                while (charIndex < lines[lineIndex].Length)
                {
                    int segStart;
                    if (lines[lineIndex][0]==_config.LiteralLineMark)
                    {
                        var token = new SyntaxToken(TokenType.Literal, lines[lineIndex]);
                        tokens.Add(token);
                        lineLen = lines[lineIndex].Length - 1;
                        break;
                    }
                    if (lines[lineIndex][charIndex] == _config.HolePlaceholder) //Window
                    {
                        segStart = charIndex;

                        do
                        {
                            charIndex++;
                        } while (charIndex < lines[lineIndex].Length &&
                            lines[lineIndex][charIndex] == _config.HolePlaceholder);
                        var token = new SyntaxToken(TokenType.Hole,
                                                    lines[lineIndex].Substring(segStart, charIndex - segStart));
                        tokens.Add(token);
                        lineLen += token.Length;
                        window = true;
                    }
                    else if (lines[lineIndex][charIndex] == _config.CommentMark)  //Comment
                    {
                        int start = lines[lineIndex].LastIndexOf('[');
                        int end = lines[lineIndex].LastIndexOf(']');
                        int len = end - start - 1;
                        if (start > charIndex && end > start
                            && TimeTag.TryParse(lines[lineIndex].Substring(start + 1, len)) != null)
                        {
                            var token1 = new SyntaxToken(TokenType.Comment,
                                                         lines[lineIndex].Substring(charIndex, start - charIndex));
                            var token2 = new SyntaxToken(TokenType.TimeTag, lines[lineIndex].Substring(start));
                            tokens.Add(token1);
                            tokens.Add(token2);
                            break;
                        }
                        var token = new SyntaxToken(TokenType.Comment, lines[lineIndex].Substring(charIndex));
                        tokens.Add(token);
                        lineLen += token.Length;
                        break;
                    }
                    else if (lines[lineIndex][charIndex] == _config.UncertainLeftMark)
                    {
                        segStart = charIndex;
                        int rightMark = lines[lineIndex].IndexOf(_config.UncertainRightMark, segStart + 1);
                        if (rightMark == -1)
                        {
                            var token = new SyntaxToken(TokenType.Uncertain, lines[lineIndex].Substring(segStart));
                            tokens.Add(token);
                            lineLen += token.Length;
                            break;
                        }
                        else
                        {
                            var token = new SyntaxToken(TokenType.Uncertain,
                                                        lines[lineIndex].Substring(segStart, rightMark - segStart + 1));
                            tokens.Add(token);
                            lineLen += token.Length;
                            charIndex = rightMark + 1;
                        }
                        uncertain = true;
                    }
                    else
                    {
                        segStart = charIndex;
                        while (charIndex < lines[lineIndex].Length && !_separators.Contains(lines[lineIndex][charIndex]))
                        {
                            charIndex++;
                        }
                        var token = new SyntaxToken(TokenType.Text,
                                                    lines[lineIndex].Substring(segStart, charIndex - segStart));
                        tokens.Add(token);
                        lineLen += token.Length;
                    }
                }
                foreach (SyntaxToken syntaxToken in tokens)
                {
                    switch (syntaxToken.Type)
                    {
                        case TokenType.Text:
                            if (lineLen <= _config.LineLength)
                            {
                                SetDefaultSettings(sb, colors, fonts);
                            }
                            else
                            {
                                SetHighlightTypeSettings(sb, _hlToolong, colors, fonts);
                                toolong = true;
                            }
                            break;
                        case TokenType.Hole:
                            SetHighlightTypeSettings(sb, _hlHole, colors, fonts);
                            break;
                        case TokenType.Comment:
                            SetHighlightTypeSettings(sb, _hlComment, colors, fonts);
                            break;
                        case TokenType.Uncertain:
                            SetHighlightTypeSettings(sb, _hlHole, colors, fonts);
                            break;
                        case TokenType.TimeTag:
                            SetHighlightTypeSettings(sb, _hlTimeTag, colors, fonts);
                            break;
                        case TokenType.Literal:
                            if (lineLen <= _config.LineLength)
                            {
                                SetHighlightTypeSettings(sb, _hlLiteralLine, colors, fonts);
                            }
                            else
                            {
                                SetHighlightTypeSettings(sb, _hlToolong, colors, fonts);
                                toolong = true;
                            }
                            break;
                    }
                    sb.Append(syntaxToken.Rtf);
                }
                if (window) summaryEventArgs.Windows++;
                if (uncertain) summaryEventArgs.Uncertern++;
                if (toolong) summaryEventArgs.Toolong++;
            }

            Rtf = sb.ToString();

            //Restore cursor and scrollbars location.
            SelectionStart = cursorLoc;

            _parsing = false;

            SetScrollPos(scrollPos);
            Win32.LockWindowUpdate((IntPtr)0);
            Invalidate();

            if (RefreshSummary != null) RefreshSummary(this, summaryEventArgs);
            if (CheckAutosave != null) CheckAutosave(this, new EventArgs());
        }

        ///   <summary>
        /// 将字符串转换成RTF编码
        ///   </summary>
        ///   <param   name= "str"> 字符串 </param>
        ///   <returns> 将字符串转换成纯ASCII的编码 </returns>
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
                        if (((Keys)(int)m.WParam == _config.PlayerRW) &&
                            ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) != 0) ||
                            ((Keys)(int)m.WParam == _config.PlayerRW2) &&
                            ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) == 0))
                        {
                            SeekBack();
                            return;
                        }
                        if (((Keys)(int)m.WParam == _config.PlayerFF) &&
                            ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) != 0) ||
                            ((Keys)(int)m.WParam == _config.PlayerFF2) &&
                            ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) == 0))
                        {
                            SeekForward();
                            return;
                        }
                        if (((Keys)(int)m.WParam == _config.PlayerTogglePause) &&
                            ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) != 0) ||
                            ((Keys)(int)m.WParam == _config.PlayerTogglePause2) &&
                            ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) == 0))
                        {
                            TogglePlayer();
                            return;
                        }
                        if (((Keys)(int)m.WParam == _config.PlayerJumpto) &&
                            ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) != 0) ||
                            ((Keys)(int)m.WParam == _config.PlayerJumpto2) &&
                            ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) == 0))
                        {
                            SeekToCurrentLine();
                            return;
                        }
                        if (((Keys)(int)m.WParam == _config.InsertTag) &&
                            ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) != 0) ||
                            ((Keys)(int)m.WParam == _config.InsertTag2) &&
                            ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) == 0))
                        {
                            InsertTimeTag();
                            return;
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

        #region Hot Keys
        private void SeekBack()
        {
            if(SeekPlayer != null)
            {
                var seekEventArgs = new SeekEventArgs(SeekDir.CurrentPos,-_config.SeekStep);
                SeekPlayer(this, seekEventArgs);
            }
        }
        private void SeekForward()
        {
            if (SeekPlayer != null)
            {
                var seekEventArgs = new SeekEventArgs(SeekDir.CurrentPos, _config.SeekStep);
                SeekPlayer(this, seekEventArgs);
            }
        }
        private void TogglePlayer()
        {
            var playerControlEventArgs = new PlayerControlEventArgs(PlayerCommand.Toggle);
            if (PlayerControl != null)
                PlayerControl(this, playerControlEventArgs);
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

        private Win32.POINT _scrollPos;
        public void LockScrollPos()
        {
            _scrollPos = GetScrollPos();

        }
        public void UnlockScrollPos()
        {
            SetScrollPos(_scrollPos);
        }

        #endregion

        #region TimeTag

        public void SeekToCurrentLine()
        {
            int linestart, linelen;

            string line = FindCurrentLine(out linestart, out linelen);
            if (line.Length > 0 && line[0] == _config.LiteralLineMark) return;
            var tag = FindTimeTag(line);
            if (tag != null && SeekPlayer != null)
            {
                SeekPlayer(this, new SeekEventArgs(SeekDir.Begin, tag.StartTime));
            }
        }

        public void InsertTimeTag()
        {
            if (_config == null) return;
            int linebegin, linelen;
            string line = FindCurrentLine(out linebegin, out linelen);
            if (line.Length > 0 && line[0] == _config.LiteralLineMark) return;
            int pos = SelectionStart;
            LockScrollPos();
            var timeEditEventArgs = new TimeEditEventArgs(TimeType.BeginTime, 0, true);
            if (TimeEdit != null) TimeEdit(this, timeEditEventArgs);
            if (timeEditEventArgs.CancelEvent) return;
            string tag = TimeTag.CreateTag(timeEditEventArgs.TimeValue, 1);
            if (FindTimeTag(line) != null)
            {
                string text = Text;
                int l = text.LastIndexOf('[', linebegin + linelen - 1);
                int len = text.IndexOf(']', l) - l + 1;
                Text = text.Remove(l, len).Insert(l, tag);
            }
            else
            {
                if (line.LastIndexOf(_config.CommentMark) == -1)
                {
                    tag = "%" + tag;
                }
                Text = Text.Insert(linebegin + linelen, tag);
            }
            SelectionStart = pos;
            UnlockScrollPos();
        }

        private TimeTag FindTimeTag(string line)
        {
            int commentmark = line.LastIndexOf(_config.CommentMark);
            if (commentmark == -1) return null;
            int tagstart = line.LastIndexOf('[');
            if (tagstart == -1) return null;
            int tagend = line.LastIndexOf(']');
            if (tagend == -1) return null;
            if (tagend < tagstart) return null;
            var tag = TimeTag.TryParse(line.Substring(tagstart + 1, tagend - tagstart - 1));
            return tag;
        }

        private string FindCurrentLine(out int first, out int len)
        {
            int pos = SelectionStart;
            int totalLen = Text.Length;
            if (totalLen == 0)
            {
                first = 0;
                len = 0;
                return "";
            }
            if (pos >= totalLen) pos = totalLen - 1;
            if (pos == 0) pos = 1;
            int linebegin = Text.LastIndexOf('\n', pos - 1);
            first = linebegin + 1; //行的第一个字符位置
            int last = Text.IndexOf('\n', first);
            last = last == -1 ? totalLen - 1 : last - 1;
            len = last - first + 1;
            return Text.Substring(first, len);
        }
        #endregion
    }

    public class SummaryEventArgs : EventArgs
    {
        public int Lines;
        public int Windows;
        public int Uncertern;
        public int Toolong;
    }
}