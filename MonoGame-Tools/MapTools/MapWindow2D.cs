using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Tools.MapTools
{
    /// <summary>
    /// A window for displaying a map.
    /// </summary>
    class MapWindow2D : IMapWindow
    {
        private Map2D m_map;
        private uint m_width, m_height;

        public MapWindow2D(Map2D p_map)
        { m_map = p_map; }

        /// <summary>
        /// Width of the map tiles
        /// </summary>
        public uint TileWidth
        {
            get { return m_map.TileWidth; }
        }

        /// <summary>
        /// Height of the map tiles
        /// </summary>
        public uint TileHeight
        {
            get { return m_map.TileHeight; }
        }

        /// <summary>
        /// Width of the Window
        /// </summary>
        public uint Width
        {
            get { return m_width; }
            set { m_width = value; }
        }

        /// <summary>
        /// Height of the window
        /// </summary>
        public uint Height
        {
            get { return m_height; }
            set { m_height = value; }
        }

        /// <summary>
        /// Draw a region of the map.
        /// </summary>
        /// <param name="p_sb"></param>
        public void Draw(SpriteBatch p_sb, object p_region)
        {
            throw new NotImplementedException();
        }
    }
}
