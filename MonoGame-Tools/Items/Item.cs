using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Tools.Items
{
    public class Item
    {
        public string name;
        public int durability;
        public int currentDurability;
        public int value;
        public bool isEquipped = false;
        public int slot;
        public Modifier ItemModifier = new Modifier();
        public bool isTradable;

        public Item(string name, int durability, int value)
        {
            this.name = name;
            this.durability = durability;
            currentDurability = durability;
            this.value = value;
            isTradable = true;
        }

        public Item(string name, int durability, int value, int slot, bool isTradable) //slot is item type, one item per slot, 0 == consumable, 1 == weapon, 2x == armor
        {
            this.name = name;
            this.durability = durability;
            currentDurability = durability;
            this.value = value;
            this.slot = slot;
            this.isTradable = isTradable;
        }


    }
}
