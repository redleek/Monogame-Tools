using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Tools.Scripting
{
    /// <summary>
    /// Represents an item that can be picked up and stored in inventory
    /// There should be only one instance of an item at any given time. - Matthew
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Stores item unique internal name - Matthew
        /// </summary>
        private string myInternalName;

        /// <summary>
        /// Stores item name - Matthew
        /// </summary>
        private string myName;

        /// <summary>
        /// Stores item description- Matthew
        /// </summary>
        private string myDescription;

        /// <summary>
        /// Stores script to be invoked when item is used - Matthew
        /// </summary>
        private string myUsedScript;

        /// <summary>
        /// Stores the script to be invoked when item is picked up - Matthew
        /// </summary>
        private string myPickedUpScript;

        /// <summary>
        /// Gets or sets the item's name - Matthew
        /// </summary>
        public string Name
        {
            get { return myName; }
            set { myName = value; }
        }

        /// <summary>
        /// Gets or sets the item description - Matthew
        /// </summary>
        public string Description
        {
            get { return myDescription; }
            set { myDescription = value; }
        }

        /// <summary>
        /// Gets or sets the lua script to be called when the item is used - Matthew
        /// </summary>
        public string usedScript
        {
            get { return myUsedScript; }
            set { myUsedScript = value; }
        }

        /// <summary>
        /// Gets or sets the lua script when the item is picked up - Matthew
        /// </summary>
        public string pickedUpScript
        {
            get { return myPickedUpScript; }
            set { myPickedUpScript = value; }
        }

        /// <summary>
        /// Get or sets the items unique internal name - Matthew
        /// </summary>
        public string internalName
        {
            get { return myInternalName; }
            set { myInternalName = value; }
        }

        /// <summary>
        /// Creates a new instance of an item. Only one item for an item should ever be constructed. 
        /// Everything else should just be a reference. - Matthew
        /// </summary>
        /// <param name="internalName">The internal name of the item, this should be unique to each item type</param>
        /// <param name="name">The name of the item</param>
        /// <param name="description">The item's description</param>
        /// <param name="usedScript">The script to invoke when the item is used</param>
        /// <param name="pickedUpScript">The script to invoke when the item is picked up</param>
        public Item(string internalName,string name,string description,string usedScript = null,string pickedUpScript = null)
        {
            myInternalName = internalName;
            myName = name;
            myDescription = description;
            myUsedScript = usedScript;
            myPickedUpScript = pickedUpScript;

            Items.RegisterItem(this);
        }

        /// <summary>
        /// Invokes the items used script with the given context - Matthew
        /// </summary>
        /// <param name="context">The context to run the item's script against</param>
        public void OnUsed(LuaContext context)
        {
            if (myPickedUpScript != null)
            {
                context.DoString(myUsedScript);
            }
        }

        /// <summary>
        /// Invokes the items picked up script with the given context - Matthew
        /// </summary>
        /// <param name="context">The context to run the item's script against</param>
        public void OnPickedUp(LuaContext context)
        {
            if (myPickedUpScript != null)
            {
                context.DoString(myPickedUpScript);
            }
        }

        /// <summary>
        /// Checks if the item is equal to another object  - Matthew
        /// </summary>
        /// <param name="obj">The object the check equality against</param>
        /// <returns>True if the objects are equal, false if otherwise </returns>
        public override bool Equals(object obj)
        {
            if (obj is Item)
            {
                Item other = obj as Item;

                return (
                    other.Name.Equals(myName) &&
                    other.Description.Equals(myDescription) &&
                    other.usedScript.Equals(myUsedScript) &&
                    other.pickedUpScript.Equals(myPickedUpScript)
                    );
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a semi-unique hash code for this item. - Matthew
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return myName.GetHashCode() & myDescription.GetHashCode() + myUsedScript.GetHashCode();
        }
    }
}