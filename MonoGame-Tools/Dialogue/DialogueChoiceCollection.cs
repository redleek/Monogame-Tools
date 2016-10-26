using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Tools.Dialogue
{
    public class DialogueChoiceCollection : IEnumerator<DialogueChoice>, IEnumerable<DialogueChoice>
    {
        List<DialogueChoice> m_choices;

        public DialogueChoice this[int index]
        {
            get { return m_choices.FirstOrDefault(choice => choice.Id == index); }
        }

        public DialogueChoiceCollection()
        {
            m_choices = new List<DialogueChoice>();
        }

        /// <summary>
        /// Gets the first available ID in this collection
        /// </summary>
        /// <returns></returns>
        public int GetFirstAvailableId()
        {
            int index = 0;

            while (this[index] != null)
                index++;

            return index;
        }

        /// <summary>
        /// Adds a dialog choice to this collection
        /// </summary>
        /// <param name="value">The value to add</param>
        public void Add(DialogueChoice value)
        {
            m_choices.Add(value);
        }

        /// <summary>
        /// Removes a dialog from this collection
        /// </summary>
        /// <param name="value">The value to remove</param>
        /// <returns>True if the object was sucessfully removed</returns>
        public bool Remove(DialogueChoice value)
        {
            return m_choices.Remove(value);
        }

        /// <summary>
        /// Converts this collection into an array
        /// </summary>
        /// <returns>An array representation of this collection</returns>
        public DialogueChoice[] ToArray()
        {
            return m_choices.ToArray();
        }

        #region Enumerator Stuff

        /// <summary>
        /// Gets the current object in this enumerable collection
        /// </summary>
        public DialogueChoice Current
        {
            get { return ((IEnumerator<DialogueChoice>)m_choices).Current; }
        }

        /// <summary>
        /// Gets the current object in this enumerable collection
        /// </summary>
        object IEnumerator.Current
        {
            get { return ((IEnumerator<DialogueChoice>)m_choices).Current; }
        }

        /// <summary>
        /// Moves to the next object in this enumerable collection
        /// </summary>
        public bool MoveNext()
        {
            return ((IEnumerator<DialogueChoice>)m_choices).MoveNext();
        }

        /// <summary>
        /// Sets the current object to it's initial state in this enumerable collection
        /// </summary>
        public void Reset()
        {
            ((IEnumerator<DialogueChoice>)m_choices).Reset();
        }

        /// <summary>
        /// Gets an enumerator that iterates through this collection
        /// </summary>
        /// <returns>An enumerator that iterates through this collection</returns>
        public IEnumerator<DialogueChoice> GetEnumerator()
        {
            return ((IEnumerable<DialogueChoice>)m_choices).GetEnumerator();
        }

        /// <summary>
        /// Gets an enumerator that iterates through this collection
        /// </summary>
        /// <returns>An enumerator that iterates through this collection</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<DialogueChoice>)m_choices).GetEnumerator();
        }

        #endregion

        public void Dispose()
        {

        }
    }
}