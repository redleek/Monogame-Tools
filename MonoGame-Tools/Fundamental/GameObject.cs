using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGame_Tools
{
    /// <summary>
    /// A basic object used to draw a Texture2D in a Rectangle to screen.
    /// </summary>
    public class GameObject
    {
        // Create allocations for position of piece, sprite texture and rectangle to draw in.
        private Texture2D m_sprite;
        private Vector2 m_position;
        private Rectangle m_boundingBox;

        /// <summary>
        /// Create a new instance of a GameObject.
        /// </summary>
        /// <param name="p_setRectangle">Rectange to draw the sprite in and it's size.</param>
        public GameObject(Vector2 p_position, uint p_width, uint p_height)
        {
            m_position = p_position;
            m_boundingBox = new Rectangle(
                (int)Position.X, (int)Position.Y, (int)p_width, (int)p_height
                );
        }

        /// <summary>
        /// Load a sprite from the content directory.
        /// </summary>
        /// <param name="p_spriteManager">Content manager for the current game.</param>
        /// <param name="p_spritePath">Path of the sprite.</param>
        public void LoadSprite(ContentManager p_spriteManager, string p_spritePath)
        {
            m_sprite = p_spriteManager.Load<Texture2D>(p_spritePath.Trim());
        }

        /// <summary>
        /// Get the current sprite being used.
        /// </summary>
        public Texture2D Sprite
        {
            get { return m_sprite; }
        }

        /// <summary>
        /// Get or set the Vector2 position of the GameObject.
        /// </summary>
        public Vector2 Position
        {
            get { return m_position; }
            set
            {
                m_position = value;
                m_boundingBox.X = (int)value.X;
                m_boundingBox.Y = (int)value.Y;
            }
        }

        public Rectangle BoundingBox
        {
            get { return m_boundingBox; }
            set { m_boundingBox = value; }
        }

        /// <summary>
        /// Draw the GameObject to the SpriteBatch.
        /// </summary>
        /// <param name="p_sb">spriteBatch to draw to.</param>
        /// <param name="p_color">Colour to draw over</param>
        public virtual void Draw(SpriteBatch p_sb, Color p_color)
        {
            if (m_sprite != null)
                p_sb.Draw(m_sprite, m_boundingBox, p_color);
        }

        /// <summary>
        /// Draw the GameObject to the SpriteBatch.
        /// </summary>
        /// <param name="p_sb">spriteBatch to draw to.</param>
        public virtual void Draw(SpriteBatch p_sb)
        { this.Draw(p_sb, Color.White); }
    }
}