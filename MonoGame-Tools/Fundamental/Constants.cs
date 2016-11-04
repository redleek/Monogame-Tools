using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Tools.Fundamental
{
    /// <summary>
    /// This is to be used as constants which won't ever change. EVER.
    /// You could place map stuff in here.
    /// </summary>
    static class Constants
    {
        public static int MainWindowHeight = 900;
        public static int MainWindowWidth = 1600;
        public enum ItemSlot { Consumable, Weapon, Head, Chest, Legs, Accessory };
        //Armor?
        public enum CharacterType { Basic, Horse, Flyer};
        public enum LandType { Impassible, Cave, Plain};
        public enum CharacterControllerType { Player, Enemy, Neutral};
        public enum MouseButtons { Left, Right, Middle };
        public static int CharacterMenuHeight = 32;
        public static int CharacterMenuWidth = 130;
        public static int InvSize = 10;

        // THIS VALUE IS NOT FINAL DON'T USE IT REPLACE IT WITH YOUR CALCULATION.
        public static float MapSquareSize { get; internal set; }
    }
}
