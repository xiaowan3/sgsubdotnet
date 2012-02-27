using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SGS.Controls
{
    
    /// <summary>
	/// Summary description for SeperaratorCollection.
	/// </summary>
    public class HightlightTypeColection
    {
        private ArrayList _innerList  = new ArrayList();


        internal HightlightTypeColection()
		{
		}

		public void AddRange(ICollection c)
		{
            _innerList.AddRange(c);
		}


		#region IList Members

		public bool IsReadOnly
		{
			get
			{
                return _innerList.IsReadOnly;
			}
		}

        public HighlightType this[int index]
		{
			get
			{
                return (HighlightType)_innerList[index];
			}
			set
			{
                _innerList[index] = value;
			}
		}

		public void RemoveAt(int index)
		{
            _innerList.RemoveAt(index);
		}

        public void Insert(int index, HighlightType value)
		{
            _innerList.Insert(index, value);
		}

        public void Remove(HighlightType value)
		{
            _innerList.Remove(value);
		}

        public bool Contains(HighlightType value)
		{
            return _innerList.Contains(value);
		}

		public void Clear()
		{
            _innerList.Clear();
		}

        public int IndexOf(HighlightType value)
		{
            return _innerList.IndexOf(value);
		}

        public int Add(HighlightType value)
		{
            return _innerList.Add(value);
		}

		public bool IsFixedSize
		{
			get
			{
                return _innerList.IsFixedSize;
			}
		}

		#endregion

		#region ICollection Members

		public bool IsSynchronized
		{
			get
			{
                return _innerList.IsSynchronized;
			}
		}

		public int Count
		{
			get
			{
                return _innerList.Count;
			}
		}

		public void CopyTo(Array array, int index)
		{
            _innerList.CopyTo(array, index);
		}

		public object SyncRoot
		{
			get
			{
                return _innerList.SyncRoot;
			}
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
            return _innerList.GetEnumerator();
		}

		#endregion


    }
}
