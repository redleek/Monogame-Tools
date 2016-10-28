using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame_Tools.Scripting
{
    public abstract class Entity
    {
        protected AttributeCollection<object> myAttributes;
        protected ItemCollection myItems;

        public object this[string index]
        {
            get { return myAttributes; }
            set { myAttributes[index] = value; }
        }

        protected Entity()
        {
            myItems = new ItemCollection();
            myAttributes = new AttributeCollection<object>();
        }

        public void GiveItem(string name, int quantity = 1)
        {
            myItems.AddItem(Items.GetItem(name), quantity);
        }
    }
}
