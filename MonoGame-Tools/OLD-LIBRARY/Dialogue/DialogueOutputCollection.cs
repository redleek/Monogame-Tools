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

        /// <summary>
        /// Gets the first available ID in this collection
        /// </summary>
        /// <returns></returns>
        public int GetFirstAvailableId()
        {
            int index = 0;

            while (this[index] != null)
            {
                index++;
            }

            return index;
        }

        /// <summary>
        /// Adds a dialog output to this collection
        /// </summary>
        /// <param name="value">The value to add</param>
        public void Add(DialogueOutput value)
        {
            m_dialogs.Add(value);
        }

        /// <summary>
        /// Removes a dialog from this collection
        /// </summary>
        /// <param name="value">The value to remove</param>
        /// <returns>True if the object was sucessfully removed</returns>
        public bool Remove(DialogueOutput value)
        {
            return m_dialogs.Remove(value);
        }

        public DialogueOutput[] ToArray()
        {
            return m_dialogs.ToArray();
        }

        #region Enum Stuff

        /// <summary>
        /// Gets the current object in this enumerable collection
        /// </summary>
        public DialogueOutput Current
        {
            get { return ((IEnumerator<DialogueOutput>)m_dialogs).Current; }
        }

        /// <summary>
        /// Gets the current object in this enumerable collection
        /// </summary>
        object IEnumerator.Current
        {
            get { return ((IEnumerator<DialogueOutput>)m_dialogs).Current; }
        }

        /// <summary>
        /// Moves to the next object in this enumerable collection
        /// </summary>
        public bool MoveNext()
        {
            return ((IEnumerator<DialogueOutput>)m_dialogs).MoveNext();
        }

        /// <summary>
        /// Sets the current object to it's initial state in this enumerable collection
        /// </summary>
        public void Reset()
        {
            ((IEnumerator<DialogueOutput>)m_dialogs).Reset();
        }

        /// <summary>
        /// Gets an enumerator that iterates through this collection
        /// </summary>
        /// <returns>An enumerator that iterates through this collection</returns>
        public IEnumerator<DialogueOutput> GetEnumerator()
        {
            return ((IEnumerable<DialogueOutput>)m_dialogs).GetEnumerator();
        }

        /// <summary>
        /// Gets an enumerator that iterates through this collection
        /// </summary>
        /// <returns>An enumerator that iterates through this collection</returns>
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
