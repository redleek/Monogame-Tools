using System;
using System.Collections.Generic;

namespace MonoGame_Tools.Scripting
{
    /// <summary>
    /// Represents the collection of all item types. - Matthew
    /// </summary>
    public static class Items
    {
        /// <summary>
        /// Stores internal item collection.
        /// </summary>
        private static Dictionary<string, Item> myInternalCollection;

        /// <summary>
        /// Static constructor for Items collection.
        /// </summary>
        static Items()
        {
            myInternalCollection = new Dictionary<string, Item>();
        }

        /// <summary>
        /// Registers an item to the known items collection - Matthew
        /// </summary>
        /// <param name="item">The item to register</param>
        public static void RegisterItem(Item item)
        {
            if (myInternalCollection.ContainsKey(item.Name))
            {
                throw new ArgumentException("Item already exists in collection");
            }
                //Otherwise add the item
                myInternalCollection.Add(item.internalName, item);
        }

        /// <summary>
        /// Gets the item with the given name
        /// </summary>
        /// <param name="name">The internal name of the item to get</param>
        /// <returns>The item with the given internal name</returns>
        public static Item GetItem(string name)
        {
            if (myInternalCollection.ContainsKey(name))
            {
                return myInternalCollection[name];
            }
            else
            {
                return null;
            }
        }
    }
}