using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Tools.Dialogue
{
    internal class ScreenSpaceRect
    {
        private float myX;
        private float myY;
        private float myWidth;
        private float myHeight;

        public ScreenSpaceRect(float x, float y, float width, float height)
        {
            this.myX = x;
            this.myY = y;
            this.myWidth = width;
            this.myHeight = height;
        }

        public Rectangle ToWorld(Viewport viewport)
        {
            return new Rectangle((int)(myX * viewport.Width), (int)(myY * viewport.Height), (int)(myWidth * viewport.Width), (int)(myHeight * viewport.Height));
        }
    }
}