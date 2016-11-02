using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame_Tools.Fundamental;
using MonoGame_Tools.Items;

namespace MonoGame_Tools.CharacterLogic
{
    public class Character
    {
        public int CharacterController;
        public int CurrentHealth;
        public int Class; // Class of character. Probably won't be used
        public bool Selected;
        public bool HasMove;
        public bool HasAction;

        string c_name;
        public Vector2 Location;
        List<Texture2D> AnimationStill = new List<Texture2D>();

        List<int> CharacterStates = new List<int>();

        public List<Item> Items = new List<Item>();

        public Modifier BaseModifier = new Modifier();
        public Modifier ItemModifier = new Modifier();
        public Modifier TotalModifier = new Modifier();

        public Character(ContentManager CM, int Class)
        {
            CharacterController = (int)Constants.CharacterControllerType.Player;
            BaseModifier.MaxHealth = 200;
            CurrentHealth = 100;
            BaseModifier.Weight = 1;
            BaseModifier.Strength = 1;
            BaseModifier.Speed = 1;
            BaseModifier.Mana = 1;
            Location = new Vector2(-100, -100); // This should be off screen ugly i know.
            AnimationStill = new List<Texture2D>();
            AnimationStill.Add(CM.Load<Texture2D>("Default"));
            BaseModifier.MoveRange = 5;
            TotalModifier = BaseModifier;
            this.Class = Class;
            Selected = false;
            HasMove = true;
            HasAction = true;

        }

        public void moveToLocation(int x, int y)
        {
            Location = new Vector2(x, y);
        }

        public string listInventory()
        {
            string invString = "";
            foreach (Item i in Items)
            {
                invString += (i.name + " \n");
            }
            return invString;
        }

        public string listEquippedItems()
        {
            string invString = "";
            foreach (Item I in Items)
            {
                if (I.isEquipped)
                {
                    invString += (I.name + " \n");
                }
            }
            return invString;
        }

        public void useItem(int IndexOfItem)
        {
            ItemLogic.useItem(this, Items.ElementAt(IndexOfItem));
        }

        public List<Item> getEquipedItems()
        {
            List<Item> equipped = new List<Item>();
            foreach (Item I in Items)
            {
                if (I.isEquipped)
                {
                    equipped.Add(I);
                }
            }
            return equipped;
        }

        public void Draw(SpriteBatch SP)
        {
            SP.Draw(AnimationStill.FirstOrDefault(), new Vector2(Location.X * Constants.MapSquareSize, Location.Y * Constants.MapSquareSize), Color.White);
        }
    }
}
