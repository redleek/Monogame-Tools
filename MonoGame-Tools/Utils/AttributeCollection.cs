using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame_Tools
{
    /// <summary>
    /// Represents a collection of attribute variables of a given type.
    /// Attributes are added by simply indexing them - Matthew
    /// </summary>
    public class AttributeCollection<T> : IEnumerator<KeyValuePair<string,T>>, IEnumerable<KeyValuePair<string, T>>
    {
        Dictionary<string, T> m_collection;

        /// <summary>
        /// Gets or sets the attribute with the given name, and creates it if it does not exist
        /// </summary>
        /// <param name="index">The name of the attribute to get/set</param>
        /// <returns>The attribute with the given name</returns>
        public T this[string index]
        {
            get
            {
                if (m_collection.ContainsKey(index))
                {
                    return m_collection[index];
                }
                else
                {
                    m_collection.Add(index, default(T));
                    return m_collection.Last().Value;
                }
            }
            set
            {
                if (m_collection.ContainsKey(index))
                {
                    m_collection[index] = value;
                }
                else
                {
                    m_collection.Add(index, value);
                }
            }
        }

        /// <summary>
        /// Gets the length of this collection
        /// </summary>
        public int Length
        {
            get { return m_collection.Count; }
        }

        /// <summary>
        /// Creates a new empty attribute collection
        /// </summary>
        public AttributeCollection()
        {
            m_collection = new Dictionary<string, T>();
        }


        /// <summary>
        /// Removes the attribute with the given name if it exists
        /// </summary>
        /// <param name="name">The name of the attribute to remove</param>
        /// <returns>True if the attribute was removed, false otherwise</returns>
        public bool Remove(string name)
        {
            return m_collection.Remove(name);
        }

        /// <summary>
        /// Gets whether or not this collection contains an attribute with the given name
        /// </summary>
        /// <param name="name">The name of the attribute to look for</param>
        /// <returns>True if the attribute exists, false otherwise</returns>
        public bool HasAttribute(string name)
        {
            return m_collection.ContainsKey(name);
        }

        #region Enumerator Stuff

        /// <summary>
        /// Gets the current object in this enumerable collection
        /// </summary>
        public KeyValuePair<string, T> Current
        {
            get { return ((IEnumerator<KeyValuePair<string, T>>)(m_collection)).Current; }
        }

        /// <summary>
        /// Gets the current object in this enumerable collection
        /// </summary>
        object IEnumerator.Current
        {
            get { return ((IEnumerator<KeyValuePair<string, T>>)m_collection).Current; }
        }

        /// <summary>
        /// Moves to the next object in this enumerable collection
        /// </summary>
        public bool MoveNext()
        {
            return ((IEnumerator<KeyValuePair<string, T>>)m_collection).MoveNext();
        }

        /// <summary>
        /// Sets the current object to it's initial state in this enumerable collection
        /// </summary>
        public void Reset()
        {
            ((IEnumerator<KeyValuePair<string, T>>)m_collection).Reset();
        }

        /// <summary>
        /// Gets an enumerator that iterates through this collection
        /// </summary>
        /// <returns>An enumerator that iterates through this collection</returns>
        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, T>>)m_collection).GetEnumerator();
        }

        /// <summary>
        /// Gets an enumerator that iterates through this collection
        /// </summary>
        /// <returns>An enumerator that iterates through this collection</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, T>>)m_collection).GetEnumerator();
        }
        #endregion

        public void Dispose()
        {

        }


    }
}
