using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame_Tools.Fundamental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Tools.Conversation
{
    public class TextBox
    {
        SpriteFont defaultFont;
        string CharacterName = "???";
        string TextBody = "...";
        Texture2D Backdrop;
        int side = 1;
        Vector2 TextVector = new Vector2(42, 62);
        Vector2 TitleVector = new Vector2(42, 10);

        public TextBox(Texture2D Backdrop, SpriteFont DefaultFont, string CharacterName, string TextBody, int side)
        {
            this.defaultFont = DefaultFont;
            this.CharacterName = CharacterName;
            this.TextBody = TextFormatting.FormatTextWrap(DefaultFont, TextBody, 400);
            this.Backdrop = Backdrop;
            this.side = side;
        }

        public TextBox(Texture2D Backdrop, SpriteFont DefaultFont, string CharacterName, string TextBody)
        {
            this.defaultFont = DefaultFont;
            this.CharacterName = CharacterName;
            this.TextBody = TextFormatting.FormatTextWrap(DefaultFont, TextBody, 400);
            this.Backdrop = Backdrop;
        }

        public TextBox(Texture2D Backdrop, SpriteFont DefaultFont, string TextBody)
        {
            this.defaultFont = DefaultFont;
            this.TextBody = TextFormatting.FormatTextWrap(DefaultFont, TextBody, 400);
            this.Backdrop = Backdrop;
        }

        public TextBox(Texture2D Backdrop, SpriteFont DefaultFont)
        {
            this.defaultFont = DefaultFont;
            this.Backdrop = Backdrop;
        }

        public string Format(string BaseText)
        {
            //string EditedText = BaseText;
            // return TextFormatting.FormatTextWrap(BaseText);


            return BaseText;
        }

        public void Draw(SpriteBatch SP)
        {
            if (side == 1)
            {
                SP.Draw(Backdrop, new Vector2(0, Constants.MainWindowHeight - Backdrop.Height), Color.White); //switch out for most common case
                SP.DrawString(defaultFont, Format(CharacterName), new Vector2(TitleVector.X, Constants.MainWindowHeight - Backdrop.Height + TitleVector.Y), Color.Black);
                SP.DrawString(defaultFont, Format(TextBody), new Vector2(TextVector.X, Constants.MainWindowHeight - Backdrop.Height + TextVector.Y), Color.Black);
            }
            else
            {
                SpriteEffects FlipHorziontally = SpriteEffects.FlipHorizontally;
                SP.Draw(Backdrop, new Rectangle(Constants.MainWindowWidth - Backdrop.Width, Constants.MainWindowHeight - Backdrop.Height, Backdrop.Bounds.Width, Backdrop.Bounds.Height), null, Color.White, 0, Vector2.Zero, FlipHorziontally, 0); //switch out for most common case
                SP.DrawString(defaultFont, Format(CharacterName), new Vector2(Constants.MainWindowWidth - TitleVector.X - defaultFont.MeasureString(Format(CharacterName)).X, Constants.MainWindowHeight - Backdrop.Height + TitleVector.Y), Color.Black);
                SP.DrawString(defaultFont, Format(TextBody), new Vector2(Constants.MainWindowWidth - Backdrop.Width + TextVector.X, Constants.MainWindowHeight - Backdrop.Height + TextVector.Y), Color.Black);
            }
        }

        /* public void Draw(SpriteBatch SP, Rectangle Location, Vector2 NameLocation, Vector2 TextLocation)
         {
             if (side == 1)
             {
                 SP.Draw(Backdrop, Location, Color.Wheat);
                 SP.DrawString(DefaultFont, Format(CharacterName), new Vector2(NameLocation.X, NameLocation.Y), Color.Black);
                 SP.DrawString(DefaultFont, Format(TextBody), new Vector2(TextLocation.X, TextLocation.Y), Color.Black);
             }
             else
             {
                 SP.Draw(Backdrop, Location, Color.Wheat);
                 SP.DrawString(DefaultFont, Format(CharacterName), new Vector2(NameLocation.X, NameLocation.Y), Color.Black);
                 SP.DrawString(DefaultFont, Format(TextBody), new Vector2(TextLocation.X, TextLocation.Y), Color.Black);
             }
         }*/
    }
}
