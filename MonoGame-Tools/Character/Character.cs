using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame_Tools.Fundamental;

namespace MonoGame_Tools.Character
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

        //Some modifiers here.

        public Character(ContentManager CM, int Class)
        {
            //CharacterController.
            B
        }
    }
}
