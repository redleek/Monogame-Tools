using MonoGame_Tools.CharacterLogic;
using MonoGame_Tools.Fundamental;
namespace MonoGame_Tools.Items
{
    static class ItemLogic
    {
        static public void useItem(Character character, Item i)
        {
            switch (i.name)
            {
                #region Consumables 0
                case "Health Potion":
                    {
                        if (consumableDurabilityCheck(i))
                        {
                            if (character.TotalModifier.MaxHealth - character.CurrentHealth >= 30)
                            {
                                character.CurrentHealth += 30;
                            }
                            else
                            {
                                character.CurrentHealth = character.TotalModifier.MaxHealth;
                            }
                        }
                    }
                    break;

                case "Hurt Potion":
                    {
                        if (consumableDurabilityCheck(i))
                        {
                            character.CurrentHealth -= 30;
                            if (character.CurrentHealth < 0)
                            {
                                character.CurrentHealth = 1;
                            }
                        }
                    }
                    break;
                #endregion

                #region Weapons 1

                case "Sword of Swording":
                    {
                        /* if (consumableDurabilityCheck(I)) //Need to do something with this.
                          {
                              switchItemInSlot(character, I);
                              //character.attacks.add
                          }*/
                    }
                    break;


                #endregion

                #region Armor 2x 
                case "Special Snowflake Chestpiece":
                    /*  if (consumableDurabilityCheck(I)) //Need to do something with this.
                      {
                          I.ItemModifier.Stamina = 2;
                          I.ItemModifier.Weight = 5;
                          switchItemInSlot(character, I);
                          //calculate character stats with new armor?
                      }*/
                    break;
                #endregion

                default:
                    break;
            }
        }

        static public void equipItem(Character character, Item i)
        {
            if (i.slot != (int)Constants.ItemSlot.Consumable)
            {
                switchItemInSlot(character, i);
            }
            recalculateCharacterStats(character);
        }

        static public void giveTo(Character character, Item i) //gives item to a character (unequips it if equiped an autoequips all consumable items
        {
            if (character.Items.Count < Constants.InvSize)
                i.isEquipped = false;
            character.Items.Add(i);
            if (i.slot == (int)Constants.ItemSlot.Consumable)
            {
                i.isEquipped = true;
            }
        }

        static public void giveToFrom(Character Receivingcharacter, Character GivingCharacter, Item i)
        {
            if (i.isTradable && Receivingcharacter.Items.Count < Constants.InvSize)
            {
                ItemLogic.giveTo(Receivingcharacter, i);
                //Receivingcharacter.Items.Add(I);
                GivingCharacter.Items.Remove(i);
                // character.Items.Add(this);
            }
            else //Item is not tradable or recieving character has to many items
            {

            }
        }

        static public void restore(Item i)
        {
            i.currentDurability = i.durability;
        }

        static public void switchItemInSlot(Character character, Item i)
        {
            foreach (Item notI in character.Items)
            {
                if (notI.slot == i.slot) //condition for 3 usable items in usable slot
                {
                    notI.isEquipped = false;
                }
            }
            i.isEquipped = true;
        }

        static public bool consumableDurabilityCheck(Item i)
        {
            if (i.currentDurability > 0)
            {
                i.currentDurability--;
                return true;
            }
            else
            {
                return false;
            }
        }

        static public void recalculateCharacterStats(Character character)
        {
            Modifier TotalModifier = new Modifier();
            foreach (Item I in character.Items)
            {
                TotalModifier = sumModifiers(TotalModifier, I.ItemModifier);
            }
            character.ItemModifier = TotalModifier;
            character.TotalModifier = sumModifiers(character.ItemModifier, character.BaseModifier);
        }

        public static Modifier sumModifiers(Modifier First, Modifier Second)
        {
            Modifier tempModifier = new Modifier();
            tempModifier.MaxHealth = First.MaxHealth + Second.MaxHealth;
            tempModifier.Strength = First.Strength + Second.Strength;
            tempModifier.Willpower = First.Willpower + Second.Willpower;
            tempModifier.Stamina = First.Stamina + Second.Stamina;
            tempModifier.Mana = First.Mana + Second.Mana;
            tempModifier.Weight = First.Weight + Second.Weight;
            tempModifier.Speed = First.Speed + Second.Speed;

            return tempModifier;
        }


    }
}
