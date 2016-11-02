using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace MonoGame_Tools.Conversation
{
    /// <summary>
    /// This code is borrowed from Erra and Alina on stack overflow.
    /// Address :http://stackoverflow.com/questions/15986473/how-do-i-implement-word-wrap
    /// The code takes a string and formats it to implement wordwrap and linebreaking on words extending past the max line character length. 
    /// </summary>
    /// 
    public static class TextFormatting
    {
        public static string FormatTextWrap(SpriteFont font, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = font.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    if (size.X > maxLineWidth)
                    {
                        if (sb.ToString() == "")
                        {
                            sb.Append(FormatTextWrap(font, word.Insert(word.Length / 2, " ") + " ", maxLineWidth));
                        }
                        else
                        {
                            sb.Append("\n" + FormatTextWrap(font, word.Insert(word.Length / 2, " ") + " ", maxLineWidth));
                        }
                    }
                    else
                    {
                        sb.Append("\n" + word + " ");
                        lineWidth = size.X + spaceWidth;
                    }
                }
            }
            return sb.ToString();
        }
    }
}