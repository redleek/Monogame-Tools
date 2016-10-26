/*   GameObject.cs - Basic object for a game.
*    Copyright (C) 2015  Connor Blakey <connorblakey96@gmail.com>, Matthew Burling <mattyburlin@gmail.com>.
*
*    This program is free software; you can redistribute it and/or modify
*    it under the terms of the GNU General Public License as published by
*    the Free Software Foundation; either version 2 of the License, or
*    (at your option) any later version.
*
*    This program is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*
*    You should have received a copy of the GNU General Public License along
*    with this program; if not, write to the Free Software Foundation, Inc.,
*    51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Tools
{
    /// <summary>
    /// A basic object used to draw a Texture2D in a Rectangle to screen.
    /// </summary>
    public class GameObject
    {
        // Create allocations for position of piece, sprite texture and rectangle to draw in.
        public Texture2D m_sprite;
        public Rectangle m_rectangle;

        /// <summary>
        /// Create a new instance of a GameObject.
        /// </summary>
        /// <param name="p_setSprite">Sprite to be loaded into the object.</param>
        /// <param name="p_setRectangle">Rectange to draw the sprite in and it's size.</param>
        public GameObject(Texture2D p_setSprite, Rectangle p_setRectangle)
        { m_sprite = p_setSprite; m_rectangle = p_setRectangle; }

        /// <summary>
        /// Return the Vector2 position of the GameObject.
        /// </summary>
        /// <returns>The vector position of the GameObject.</returns>
        public Vector2 getPosition()
        { return new Vector2(m_rectangle.X, m_rectangle.Y); }

        /// <summary>
        /// Draw the GameObject to the SpriteBatch.
        /// </summary>
        /// <param name="p_sb">spriteBatch to draw to.</param>
        /// <param name="p_color">Colour to draw over</param>
        public virtual void Draw(SpriteBatch p_sb, Color p_color)
        {
            if (m_sprite != null)
                p_sb.Draw(m_sprite, m_rectangle, p_color);
        }

        /// <summary>
        /// Draw the GameObject to the SpriteBatch.
        /// </summary>
        /// <param name="p_sb">spriteBatch to draw to.</param>
        public virtual void Draw(SpriteBatch p_sb)
        { this.Draw(p_sb, Color.White); }
    }
}