using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame_Tools.Dialogue
{
    public class DialogueOutputCollection : IEnumerator<DialogueOutput>, IEnumerable<DialogueOutput>
    {
        List<DialogueOutput> m_dialogs;

        public DialogueOutput this[int index]
        {
            get { return m_dialogs.FirstOrDefault(choice => choice.Id == index); }
        }

        public DialogueOutputCollection()
        {
            m_dialogs = new List<DialogueOutput>();
        }

        public int GetFirstAvailableId()
        {
            int index = 0;

            while (this[index] != null)
            {
                index++;
            }

            return index;
        }

        public void Add(DialogueOutput value)
        {
            m_dialogs.Add(value);
        }

        public bool Remove(DialogueOutput value)
        {
            return m_dialogs.Remove(value);
        }

        public DialogueOutput[] ToArray()
        {
            return m_dialogs.ToArray();
        }

        #region Enum Stuff

        public DialogueOutput Current
        {
            get { return ((IEnumerator<DialogueOutput>)m_dialogs).Current; }
        }

        object IEnumerator.Current
        {
            get { return ((IEnumerator<DialogueOutput>)m_dialogs).Current; }
        }

        public bool MoveNext()
        {
            return ((IEnumerator<DialogueOutput>)m_dialogs).MoveNext();
        }

        public void Reset()
        {
            ((IEnumerator<DialogueOutput>)m_dialogs).Reset();
        }

        public IEnumerator<DialogueOutput> GetEnumerator()
        {
            return ((IEnumerable<DialogueOutput>)m_dialogs).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<DialogueOutput>)m_dialogs).GetEnumerator();
        }

        #endregion
        public void Dispose()
        {

        }
    }
}
