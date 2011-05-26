using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSDatatype
{
    public class SSAIndex
    {
        private double _mediaLength;
        private List<V4Event>[] _eventIndex;

        public SubStationAlpha Subtitle { get; set; }

        /// <summary>
        /// Create event index
        /// </summary>
        /// <param name="mediaLength">mediaLength</param>
        public void CreateIndex(double mediaLength)
        {
            if( mediaLength<= 0) _eventIndex = null;
            _mediaLength = mediaLength;
            var n = (int) Math.Ceiling(_mediaLength) + 1;
            _eventIndex = new List<V4Event>[n];
            for(int i = 0;i<n;i++)_eventIndex[i]= new List<V4Event>();
            foreach (V4Event v4Event in Subtitle.EventsSection.EventList)
            {
                var a = (int)Math.Floor(v4Event.Start.Value);
                var b = (int)Math.Ceiling(v4Event.End.Value);
                for (int i = a; i < b; i++)
                {
                    if (i < n) _eventIndex[i].Add(v4Event);
                }
            }
        }

        public void RefreshIndex()
        {
            if (_eventIndex == null) return;
            var n = (int)Math.Ceiling(_mediaLength) + 1;
            foreach (var t in _eventIndex) t.Clear();
            foreach (V4Event v4Event in Subtitle.EventsSection.EventList)
            {
                var a = (int)Math.Floor(v4Event.Start.Value);
                var b = (int)Math.Ceiling(v4Event.End.Value);
                for (int i = a; i < b; i++)
                {
                    if (i < n) _eventIndex[i].Add(v4Event);
                }
            }
        }

        /// <summary>
        /// Refresh index when event time is edited.
        /// </summary>
        /// <param name="v4Event">Edited event.</param>
        /// <param name="oldStart">Old start time.</param>
        /// <param name="oldEnd">Old end time.</param>
        public void ItemEdited(V4Event v4Event, double oldStart, double oldEnd)
        {
            var olds = (int)Math.Floor(oldStart);
            var olde = (int)Math.Ceiling(oldEnd);

            var news = (int)Math.Floor(v4Event.Start.Value);
            var newe = (int)Math.Ceiling(v4Event.End.Value);
            int s = Math.Min(news, olds), e = Math.Max(newe, olde);
            if (_eventIndex == null) return;
            for (int i = s; i < e; i++)
            {
                if (i >= _eventIndex.Length) continue;
                if (i < news || i >= newe)
                {
                    if (_eventIndex[i].Contains(v4Event)) _eventIndex[i].Remove(v4Event);
                }
                else
                {
                    if (!_eventIndex[i].Contains(v4Event)) _eventIndex[i].Add(v4Event);

                }
            }
        }
        public string GetSubtitle(double time)
        {
            string str = "";
            if (_eventIndex != null && time <= _mediaLength)
            {
                str = _eventIndex[(int)Math.Floor(time)].
                    Where(
                        t =>
                        t.Start.Value <= time && t.End.Value >= time &&
                        t.End.Value - t.Start.Value > 0.1).
                    Aggregate(str, (current, t) => current + (t.Text + Environment.NewLine));
            }
            return str;
        }
    }

}
