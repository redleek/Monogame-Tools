using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame_Tools.CharacterLogic;
using MonoGame_Tools.Fundamental;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Tools.CharacterLogic
{
    public class CharacterController
    {
        public List<Character> AllCharacters;

        public CharacterController()
        {
            AllCharacters = new List<Character>();
        }

        public void loadCharactersFromLibrary(int Section, ContentManager Content)
        {
            switch (Section)
            {
                case 0:
                Character DefaultChar = new Character(Content, (int)Constants.CharacterType.Basic);
                Character DefaultEvil = new Character(Content, (int)Constants.CharacterType.Basic);
                DefaultEvil.CharacterController = (int)Constants.CharacterControllerType.Enemy;
                //Need to add them.
                // as well as move them.
                break;
                default:
                    break;
            }
        }

        public List<Character> getCharacterOfController(int Controller)
        {
            List<Character> Characters = new List<Character>();

            foreach (Character c in AllCharacters)
            {
                if(c.CharacterController == Controller)
                {
                    Characters.Add(c);
                }
            }
            return Characters;
        }

        public void addCharacter(Character newCharacter)
        {
            AllCharacters.Add(newCharacter);
        }

        public void removeCharacter(Character oldCharacter)
        {
            AllCharacters.Remove(oldCharacter);
        }

        public void selectCharacter(Character Target)
        {
            foreach(Character c in AllCharacters)
            {
                if (c == Target)
                {
                    c.Selected = true;
                }
                else
                {
                    c.Selected = false;
                }
            }
        }

        public Boolean isCharacterAt(int x, int y)
        {
            foreach(Character c in AllCharacters)
            {
                if ((int)c.Location.X == x && (int)c.Location.Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        public void moveCharacter(Character character, int x, int y)
        {
            foreach(Character c in AllCharacters)
            {
                if (character == c)
                {
                    //c.moveToLocation needs to be implemented here.
                    break;
                }
            }
        }

        public void Draw(SpriteBatch SP)
        {
            foreach (Character c in AllCharacters)
            {
                c.Draw(SP);
            }
        }
    }
}
