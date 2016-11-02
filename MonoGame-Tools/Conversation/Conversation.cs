using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Tools.Conversation
{
    public class Conversation
    {
        List<TextBox> Conv;
        int ConvoLength = 0;
        int CurTextBox = 0;
        public string ConvName;
        public bool InConversation = false;
        public bool ConversationCompleted = false;
        KeyboardState oldState;

        public Conversation(string ConvName)
        {
            this.ConvName = ConvName;
            Conv = new List<TextBox>();
            oldState = Keyboard.GetState();
        }

        public Conversation(string ConvName, List<TextBox> Conv)
        {
            this.Conv = Conv;
            this.ConvName = ConvName;
            ConvoLength = this.Conv.Count;
            oldState = Keyboard.GetState();
        }

        public void add(TextBox T)
        {
            Conv.Add(T);
            ConvoLength++;
            //Conv.ElementAt<TextBox>(1);
        }

        public string getConvName()
        {
            return ConvName;
        }

        public void start()
        {
            InConversation = true;
        }

        public int next() //increments Conversation, returns 1 if conversation has reached last textbox, else 0
        {
            if (CurTextBox >= ConvoLength - 1)
            { return 1; }
            else
            { CurTextBox++; return 0; }
        }

        public void reset() //starts the conversation over
        {
            CurTextBox = 0;
        }

        public void input()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Space))
            {
                if (!oldState.IsKeyDown(Keys.Space))
                {
                    if (next() == 0) //go to next text box or reset and exit conversation
                    {
                        //nothing
                    }
                    else
                    {
                        InConversation = false;
                        ConversationCompleted = true;
                        reset();
                    }
                }
            }
            else if (oldState.IsKeyDown(Keys.Space))
            {
                //key has been released
            }
            oldState = newState;
        }

        public void draw(SpriteBatch SP)
        {
            if (InConversation)
            {
                Conv.ElementAt<TextBox>(CurTextBox).Draw(SP);
            }
        }
    }
}
