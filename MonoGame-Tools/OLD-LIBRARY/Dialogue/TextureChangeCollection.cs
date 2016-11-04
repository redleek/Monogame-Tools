using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame_Tools.Dialogue
{
    public class TextureChangeCollection : IEnumerable<TextureChangeTag>, IEnumerator<TextureChangeTag>
    {
        Dictionary<TextureRole, TextureChangeTag> m_internalList;

        public TextureChangeTag this[TextureRole index]
        {
            get
            {
                return m_internalList.ContainsKey(index) ? m_internalList[index] : null;
            }
            set
            {
                if (!m_internalList.ContainsKey(index))
                    m_internalList.Add(index, value);
                else
                    throw new ArgumentException("Cannot have two texture changes with the same role!");
            }
        }

        public int Count
        {
            get { return m_internalList.Count; }
        }

        public TextureChangeCollection()
        {
            m_internalList = new Dictionary<TextureRole, TextureChangeTag>();
        }

        public IEnumerator<TextureChangeTag> GetEnumerator()
        {
            return m_internalList.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_internalList.Values.GetEnumerator();
        }

        public TextureChangeTag Current
        {
            get { return m_internalList.Values.GetEnumerator().Current; }
        }

        public void Dispose()
        {

        }

        object System.Collections.IEnumerator.Current
        {
            get { return m_internalList.Values.GetEnumerator().Current; }
        }

        public bool MoveNext()
        {
            return m_internalList.Values.GetEnumerator().MoveNext();
        }


        public void Reset()
        {
            ((IEnumerator<TextureChangeTag>)(m_internalList.Values.GetEnumerator())).Reset();
        }
    }
}