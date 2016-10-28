using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Tools.MapTools
{
    /// <summary>
    /// Specify a layer of tiles
    /// </summary>
    public class Layer
    {
        /// <summary>
        /// Dictionary where Vector2 (key) is the x,y coordinates
        /// and Tile2D is the tile at that location.
        /// </summary>
        private IDictionary<Vector2, Tile2D> m_tiles;

        public string Name
        {
            get;
            set;
        }

        public Layer(string p_name)
        {
            Name = p_name;
            m_tiles = new Dictionary<Vector2, Tile2D>();
        }

        public Layer getDomain(Vector2 p_domain)
        {
            Layer domainLayer = new Layer(Name);
            throw new NotImplementedException();
            return domainLayer;
        }

        public virtual void Draw(SpriteBatch p_sb)
        {
            foreach (Tile2D tile in m_tiles.Values)
                tile.Draw(p_sb);
        }
    }
}
