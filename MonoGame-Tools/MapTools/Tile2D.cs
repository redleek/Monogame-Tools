using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame_Tools.MapTools
{
    /// <summary>
    /// Tile for a map
    /// </summary>
    class Tile2D : GameObject
    {
        private bool m_walkable;

        public Tile2D(Vector2 p_position, uint p_width, uint p_height, bool p_walkable)
            : base(p_position, p_width, p_height)
        { m_walkable = p_walkable; }

        /// <summary>
        /// Is it possible to walk on this tile?
        /// </summary>
        public bool Walkable
        {
            get { return m_walkable; }
        }
    }
}
