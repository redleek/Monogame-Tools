using System.Collections.Generic;

namespace MonoGame_Tools.Scripting
{
    /// <summary>
    /// Represents a collection of the item instances - Matthew
    /// </summary>
    public class ItemCollection
    {
        /// <summary>
        /// Stores internal collections in a dictionary of string and int - Matthew
        /// </summary>
        private Dictionary<string, int> myCollections;

        /// <summary>
        /// Creates new instance of item collection. - Matthew
        /// </summary>
        public ItemCollection()
        {
            myCollections = new Dictionary<string, int>();
        }

        /// <summary>
        /// Gets the number of item types in the item collection. - Matthew
        /// </summary>
        /// <returns>The number of items in the collection.</returns>
        public int getNumItems()
        {
            return myCollections.Count;
        }

        /// <summary>
        /// Check whether this collection contains a given item. - Matthew
        /// </summary>
        /// <param name="internalItemName">The internal name of the item to search for</param>
        /// <returns>True if this collection contains the item otherwise false.</returns>
        public bool HasItem(string internalItemName)
        {
            return myCollections.ContainsKey(internalItemName);
        }

        /// <summary>
        /// Checks whether this collection contains an item - Matthew
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>True if this collection contains the item otherwise false.</returns>
        public bool HasItem(Item item)
        {
            return HasItem(item.internalName);
        }

        /// <summary>
        /// Gets the number of items of a specific type in the collection - Matthew
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>The number of given item in this collection</returns>
        public int GetItemCount(Item item)
        {
            return GetItemCount(item.internalName);
        }

        /// <summary>
        /// Gets the number of items of a specific type in the collection - Matthew
        /// </summary>
        /// <param name="internalName">The internal item name to search for</param>
        /// <returns>The number of the given item in this collection</returns>
        public int GetItemCount(string internalName)
        {
            if (myCollections.ContainsKey(internalName))
            {
                return myCollections[internalName];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Adds a number of a given item type to this collection. - Matthew
        /// </summary>
        /// <param name="item">The item type to add to the collection</param>
        /// <param name="count">The number of items to add</param>
        public void AddItem(Item item, int count = 1)
        {
            AddItem(item.internalName, count);
        }

        /// <summary>
        /// Adds a number of a given item type to this collection - Matthew
        /// </summary>
        /// <param name="internalName">The internal item name to add to this collection</param>
        /// <param name="count">The number of items to add</param>
        public void AddItem(string internalName , int count = 1)
        {
            if (!myCollections.ContainsKey(internalName))
            {
                myCollections.Add(internalName, count);
            }
            else
            {
                myCollections[internalName] += count;
            }
        }

        /// <summary>
        /// Removes a number of a given item type from this collection - Matthew
        /// </summary>
        /// <param name="item">The item type to remove from this collection</param>
        /// <param name="count">The number of items to remove</param>
        public void RemoveItem(Item item, int count = 1)
        {
            RemoveItem(item.internalName, count);
        }

        /// <summary>
        /// Removes a number of a given item type from this collection - Matthew
        /// </summary>
        /// <param name="internalName">The internal item name to remove from the collection</param>
        /// <param name="count">The number of items to remove</param>
        public void RemoveItem(string internalName, int count = 1)
        {
            if (!myCollections.ContainsKey(internalName))
            {
                myCollections.Add(internalName, -count);
            }
            else
            {
                myCollections[internalName] -= count;
            }
        }   
    }
}