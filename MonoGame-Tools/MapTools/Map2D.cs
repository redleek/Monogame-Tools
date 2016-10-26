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
    /// Map class for storing a map layout.
    /// </summary>
    class Map2D : IMap
    {
        private string m_name;
        private uint m_tileWidth, m_tile_height;
        private IDictionary<Vector2, GameObject> m_tiles;

        public Map2D(string p_mapFile)
        { loadMap(p_mapFile); }

        public Map2D()
        { m_tiles = new Dictionary<Vector2, GameObject>(); }

        /// <summary>
        /// Name of map.
        /// </summary>
        public string Name
        { get { return m_name; } }

        /// <summary>
        /// Width of all tiles
        /// </summary>
        public uint TileWidth
        { get { return m_tileWidth; } }

        /// <summary>
        /// Height of all tiles
        /// </summary>
        public uint TileHeight
        { get { return m_tile_height; } }

        /// <summary>
        /// Gets a chunk of the map to display in a window.
        /// </summary>
        /// <param name="p_domain"></param>
        /// <returns></returns>
        public IEnumerable<GameObject> getDomain(object p_domain)
        {
            List<GameObject> domain = new List<GameObject>();
            throw new NotImplementedException();
            return domain;
        }

        /// <summary>
        /// Load a map from file.
        /// </summary>
        /// <param name="p_mapFile">Name and/or directory of map.</param>
        /// <returns>Did map successfully load?</returns>
        public bool loadMap(string p_mapFile)
        {
            throw new NotImplementedException();
            m_tiles = new Dictionary<Vector2, GameObject>();
            try
            { }
            catch (Exception e)
            { }

        }

        /// <summary>
        /// Draw the entire map
        /// </summary>
        /// <param name="p_sb"></param>
        public void Draw(SpriteBatch p_sb)
        {
            foreach (GameObject tile in m_tiles.Values)
                tile.Draw(p_sb);
        }

        /// <summary>
        /// Draw a chunk of the map.
        /// </summary>
        public void drawDomain(SpriteBatch p_sb, object p_domain)
        {
            foreach (GameObject tile in getDomain(p_domain))
                tile.Draw(p_sb);
        }
    }
}
