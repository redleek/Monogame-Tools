using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Tools.Fundamental
{
    static class InputMethods
    {
        static public bool checkIfMouseClicked(MouseState oldState, int MouseButton)
        {
            MouseState newState = Mouse.GetState();
            bool returnValue = false;
            switch (MouseButton)
            {
                case (int)Constants.MouseButtons.Left:
                    if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Pressed)
                    {
                        returnValue = true;
                    }
                    break;
                case (int)Constants.MouseButtons.Right:
                    if (newState.RightButton == ButtonState.Pressed && oldState.RightButton != ButtonState.Pressed)
                    {
                        returnValue = true;
                    }
                    break;
                case (int)Constants.MouseButtons.Middle:
                    if (newState.MiddleButton == ButtonState.Pressed && oldState.MiddleButton != ButtonState.Pressed)
                    {
                        returnValue = true;
                    }
                    break;
            }
            return returnValue;
        }

        static public bool checkIfMouseClickInBounds(MouseState oldState, Vector2 Origin, Vector2 boundrySize, int MouseButton) //requires persistent oldstate to be passed from calling class
        {
            MouseState newState = Mouse.GetState();
            bool returnVariable = false;
            switch (MouseButton)
            {
                case (int)Constants.MouseButtons.Left:
                    if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Pressed)
                    {
                        if (newState.X >= Origin.X && newState.Y >= Origin.Y && newState.X < Origin.X + boundrySize.X && newState.Y <= Origin.Y + boundrySize.Y)
                        {
                            returnVariable = true;
                        }
                        else
                        {
                            returnVariable = false;
                        }
                    }
                    break;
                case (int)Constants.MouseButtons.Right:
                    if (newState.RightButton == ButtonState.Pressed && oldState.RightButton != ButtonState.Pressed)
                    {
                        if (newState.X >= Origin.X && newState.Y >= Origin.Y && newState.X < Origin.X + boundrySize.X && newState.Y <= Origin.Y + boundrySize.Y)
                        {
                            returnVariable = true;
                        }
                        else
                        {
                            returnVariable = false;
                        }
                    }
                    break;
                case (int)Constants.MouseButtons.Middle:
                    if (newState.MiddleButton == ButtonState.Pressed && oldState.MiddleButton != ButtonState.Pressed)
                    {
                        if (newState.X >= Origin.X && newState.Y >= Origin.Y && newState.X < Origin.X + boundrySize.X && newState.Y <= Origin.Y + boundrySize.Y)
                        {
                            returnVariable = true;
                        }
                        else
                        {
                            returnVariable = false;
                        }
                    }
                    break;
            }
            return returnVariable;
        }
    }
}
