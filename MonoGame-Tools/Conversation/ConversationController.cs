using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Tools.Conversation
{
    public class ConversationController
    {
        public List<Conversation> Conversations;
        public int CurrentConversationIndex;

        public ConversationController()
        {
            Conversations = new List<Conversation>();
        }

        void addConversation(Conversation c)
        {
            Conversations.Add(c);
        }

        public void loadFromLibrary(int section, ContentManager Content, SpriteBatch SP)
        {
            SpriteFont Segoe14 = Content.Load<SpriteFont>("Segoe14");
            Texture2D DefaultTexture2D = Content.Load<Texture2D>("textBoxDefault");

            switch (section)
            {
                case 0:
                    Conversations.Clear();
                    Conversation test1 = new Conversation("test1");
                    test1.add(new TextBox(DefaultTexture2D, Segoe14, "Test", "Test"));
                    Conversations.Add(test1);
                    Conversation Victory = new Conversation("Victory");
                    Victory.add(new TextBox(DefaultTexture2D, Segoe14, "Test", "Test Victory."));
                    Conversations.Add(Victory);
                    break;
                case 1:
                    Conversations.Clear();
                    Conversation help = new Conversation("help");
                    help.add(new TextBox(DefaultTexture2D, Segoe14, "test", "test"));
                    Conversations.Add(help);
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }

        public void startConvIfNotStarted(string ConvName)
        {
            foreach (Conversation c in Conversations)
            {
                if (c.getConvName() == ConvName)
                {
                    if (c.ConversationCompleted == false && c.InConversation == false)
                    {
                        c.start();
                        CurrentConversationIndex = Conversations.IndexOf(c);
                    }
                }
            }
        }

        public void Input()
        {
            if (CurrentConversationIndex != -1)
            {
                Conversations.ElementAt<Conversation>(CurrentConversationIndex).input();
            }
        }

        public void Draw(SpriteBatch SP)
        {
            if (CurrentConversationIndex != -1)
            {
                Conversations.ElementAt<Conversation>(CurrentConversationIndex).draw(SP);
            }
        }
    }
}
