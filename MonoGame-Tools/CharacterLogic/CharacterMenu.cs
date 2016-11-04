
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Tools.Fundamental;
using MonoGame_Tools.Items;

namespace MonoGame_Tools.CharacterLogic
{
    public class CharacterMenu
    {
        List<Texture2D> MenuTextures;
        List<String> MenuItems;
        List<String> FilteredMenuItems;
        SpriteFont DefaultFont;
        bool IsVisibleLvl1;
        bool IsVisibleLvl2;
        bool IsVisibleLvl3;
        int menuIndexLvl2;
        Vector2 mousePosition;
        Vector2 MenuDrawLocation;
        int SelectedChracterIndex;

        public CharacterMenu(List<Texture2D> MenuTextures, SpriteFont DefaultFont) //top, mid, bot
        {
            this.MenuTextures = MenuTextures;
            MenuItems = new List<string>();
            // MenuItems.Add("Attack");
            // MenuItems.Add("Ability");
            MenuItems.Add("Action");
            MenuItems.Add("fight");
            MenuItems.Add("Items");
            MenuItems.Add("Wait");
            this.DefaultFont = DefaultFont;
            IsVisibleLvl1 = false;
            IsVisibleLvl2 = false;
            mousePosition = new Vector2(0, 0);
            FilteredMenuItems = new List<string>();
            SelectedChracterIndex = -1;
        }

        /* public void SelectCharacter(Character c) // sort of floundering around
         {
             c.Selected = true;
         }
         public void Draw(Character c)
         {

             //foreach item, foreach action, foreach attack, foreach derp?
         }
         */

        List<string> filterList(Character c) //filters out options that are not avaialable
        {
            return MenuItems;
        }

        public void Input(List<Character> CharacterList, MouseState oldState) //resstructure this aea to account for going backwards in the menu
        {
            if (SelectedChracterIndex == -1)
            {
                foreach (Character c in CharacterList) //looks for a character that has been clicked on (if none exist nothing happens, otherwise that character is selected)
                {
                    if (IsVisibleLvl1 != true && InputMethods.checkIfMouseClickInBounds(oldState, new Vector2(c.Location.X * Constants.MapSquareSize, c.Location.Y * Constants.MapSquareSize), new Vector2(Constants.MapSquareSize, Constants.MapSquareSize), (int)Constants.MouseButtons.Left))
                    {
                        SelectedChracterIndex = CharacterList.IndexOf(c);
                        IsVisibleLvl1 = true;
                        FilteredMenuItems = filterList(c);
                        break;
                    }
                }
            }
            else
            {
                if (IsVisibleLvl1 && !IsVisibleLvl2 && InputMethods.checkIfMouseClicked(oldState, (int)Constants.MouseButtons.Left)) //attack, items, abilities
                {
                    if (InputMethods.checkIfMouseClickInBounds(oldState, new Vector2((CharacterList[SelectedChracterIndex].Location.X + 1) * Constants.MapSquareSize, CharacterList[SelectedChracterIndex].Location.Y * Constants.MapSquareSize), new Vector2(Constants.CharacterMenuWidth, Constants.CharacterMenuHeight * (FilteredMenuItems.Count)), (int)Constants.MouseButtons.Left))
                    {
                        IsVisibleLvl2 = true;
                        /*string 
                        for (int x = 0; x  < FilteredMenuItems.Count; x++)
                        {
                            
                        }*/
                        menuIndexLvl2 = (int)(Mouse.GetState().Y - (CharacterList[SelectedChracterIndex].Location.Y * Constants.MapSquareSize)) / Constants.CharacterMenuHeight;
                        Console.Write(menuIndexLvl2);

                        //display appropriat sub menu
                        //enum switch if item menu selected display items
                    }
                    else
                    {
                        IsVisibleLvl1 = false;
                        SelectedChracterIndex = -1;
                    }



                    //if (FilteredMenuItems)
                }
                else if (IsVisibleLvl1 && IsVisibleLvl2 && InputMethods.checkIfMouseClicked(oldState, (int)Constants.MouseButtons.Left))
                {
                    switch (FilteredMenuItems[menuIndexLvl2])
                    {
                        case "Items":
                            {
                                Vector2 characterDisplacement = new Vector2((CharacterList[SelectedChracterIndex].Location.X + 1) * (Constants.MapSquareSize), CharacterList[SelectedChracterIndex].Location.Y * Constants.MapSquareSize);
                                List<Item> EquippedItems = new List<Item>();
                                EquippedItems = CharacterList[SelectedChracterIndex].getEquipedItems();
                                bool forLoopHasWorked = false;
                                for (int j = 0; j < EquippedItems.Count; j++)
                                {
                                    if (InputMethods.checkIfMouseClickInBounds(oldState, new Vector2(Constants.CharacterMenuWidth + characterDisplacement.X, 32 * j + characterDisplacement.Y), new Vector2(Constants.CharacterMenuWidth, Constants.CharacterMenuHeight), (int)Constants.MouseButtons.Left))
                                    {
                                        forLoopHasWorked = true;
                                        ItemLogic.useItem(CharacterList[SelectedChracterIndex], EquippedItems[j]);
                                        Console.WriteLine(CharacterList[SelectedChracterIndex].CurrentHealth);
                                    }
                                }

                                if (!forLoopHasWorked)
                                {
                                    IsVisibleLvl1 = false;
                                    IsVisibleLvl2 = false;
                                    SelectedChracterIndex = -1;
                                }
                                else
                                {
                                    CharacterList[SelectedChracterIndex].HasAction = false;
                                    IsVisibleLvl1 = false;
                                    IsVisibleLvl2 = false;
                                    SelectedChracterIndex = -1;
                                }
                                /*if ()
                            {
                            }
                            else
                            {
                                IsVisibleLvl1 = false;
                                SelectedChracterIndex = -1;
                            }*/
                            }
                            break;

                        case "Wait":
                            {
                                CharacterList[SelectedChracterIndex].HasMove = false;
                                CharacterList[SelectedChracterIndex].HasAction = false;
                                IsVisibleLvl1 = false;
                                IsVisibleLvl2 = false;
                                SelectedChracterIndex = -1;
                            }
                            break;

                        default:
                            break;
                    }
                }
                /*else if (IsVisibleLvl2 && InputMethods.chechIfMouseClicked(oldState, (int)Constants.MouseButtons.Left)) //no support for sub menues yet so hold off for testing //attack menue, item menu, abilities menu
                {
                    if (InputMethods.checkIfMouseClickInBounds(oldState, new Vector2((CharacterList[SelectedChracterIndex].Location.X + 1) * Constants.MapSquareSize, CharacterList[SelectedChracterIndex].Location.Y * Constants.MapSquareSize), new Vector2(Constants.CharacterMenuWidth, Constants.CharacterMenuHeight * (FilteredMenuItems.Count - 1)), (int)Constants.MouseButtons.Left))
                    {
                        //perform appropriate action
                        //enum switch if item in inverntory pressed use item
                    }
                    else
                    {
                        IsVisibleLvl1 = false;
                        IsVisibleLvl2 = false;
                        SelectedChracterIndex = -1;
                    }
                }*/
            }
        }

        public void Draw(SpriteBatch SP, List<Character> CharacterList) //have to redo this too
        {
            if (IsVisibleLvl1)
            {
                int x = FilteredMenuItems.Count;
                Vector2 characterDisplacement = new Vector2((CharacterList[SelectedChracterIndex].Location.X + 1) * (Constants.MapSquareSize), CharacterList[SelectedChracterIndex].Location.Y * Constants.MapSquareSize);

                if (x < 3)
                {
                    //draw nothing (there is nothing to draw)
                }



                SP.Draw(MenuTextures[0], new Vector2(0 + characterDisplacement.X, 0 + characterDisplacement.Y), Color.White);
                SP.DrawString(DefaultFont, MenuItems[0], new Vector2(10 + characterDisplacement.X, 8 + characterDisplacement.Y), Color.White);
                for (int temp = 1; temp < x - 1; temp++)
                {
                    SP.Draw(MenuTextures[1], new Vector2(0 + characterDisplacement.X, 32 * temp + characterDisplacement.Y), Color.White);
                    SP.DrawString(DefaultFont, MenuItems[temp], new Vector2(10 + characterDisplacement.X, 8 + 32 * temp + characterDisplacement.Y), Color.White);
                }
                SP.Draw(MenuTextures[2], new Vector2(0 + characterDisplacement.X, 32 * (x - 1) + characterDisplacement.Y), Color.White);
                SP.DrawString(DefaultFont, MenuItems[MenuItems.Count - 1], new Vector2(10 + characterDisplacement.X, 8 + 32 * (x - 1) + characterDisplacement.Y), Color.White);

                if (IsVisibleLvl2)
                {
                    switch (FilteredMenuItems[menuIndexLvl2])
                    {
                        case "Items":
                            {
                                List<Item> EquippedItems = new List<Item>();
                                EquippedItems = CharacterList[SelectedChracterIndex].getEquipedItems();
                                int i = EquippedItems.Count;
                                while (i < 3)
                                {
                                    EquippedItems.Add(new Item("tempItem", 0, 0));
                                    i++;
                                }

                                SP.Draw(MenuTextures[0], new Vector2(Constants.CharacterMenuWidth + characterDisplacement.X, 0 + characterDisplacement.Y), Color.White);
                                SP.DrawString(DefaultFont, EquippedItems[0].name, new Vector2(Constants.CharacterMenuWidth + 10 + characterDisplacement.X, 8 + characterDisplacement.Y), Color.White);
                                for (int temp = 1; temp < i - 1; temp++)
                                {
                                    SP.Draw(MenuTextures[1], new Vector2(Constants.CharacterMenuWidth + characterDisplacement.X, 32 * temp + characterDisplacement.Y), Color.White);
                                    SP.DrawString(DefaultFont, EquippedItems[temp].name, new Vector2(Constants.CharacterMenuWidth + 10 + characterDisplacement.X, 8 + 32 * temp + characterDisplacement.Y), Color.White);
                                }
                                SP.Draw(MenuTextures[2], new Vector2(Constants.CharacterMenuWidth + characterDisplacement.X, 32 * (i - 1) + characterDisplacement.Y), Color.White);
                                SP.DrawString(DefaultFont, EquippedItems[i - 1].name, new Vector2(Constants.CharacterMenuWidth + 10 + characterDisplacement.X, 8 + 32 * (i - 1) + characterDisplacement.Y), Color.White);
                            }
                            break;
                        default:
                            break;
                    }
                    /* SP.Draw(MenuTextures[0], new Vector2(0 + characterDisplacement.X, 0 + characterDisplacement.Y), Color.White);
                     SP.DrawString(DefaultFont, MenuItems[0], new Vector2(10 + characterDisplacement.X, 8 + characterDisplacement.Y), Color.White);
                     for (int temp = 1; temp < x - 1; temp++)
                     {
                         SP.Draw(MenuTextures[1], new Vector2(0 + characterDisplacement.X, 32 * temp + characterDisplacement.Y), Color.White);
                         SP.DrawString(DefaultFont, MenuItems[temp], new Vector2(10 + characterDisplacement.X, 8 + 32 * temp + characterDisplacement.Y), Color.White);
                     }
                     SP.Draw(MenuTextures[2], new Vector2(0 + characterDisplacement.X, 32 * (x - 1) + characterDisplacement.Y), Color.White);
                     SP.DrawString(DefaultFont, MenuItems[MenuItems.Count - 1], new Vector2(10 + characterDisplacement.X, 8 + 32 * (x - 1) + characterDisplacement.Y), Color.White);*/
                }
            }
            else
            {
                //nothing?
            }
        }
    }
}