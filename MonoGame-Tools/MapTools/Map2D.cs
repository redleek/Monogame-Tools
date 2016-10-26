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
        public IDictionary<Vector2, GameObject> m_tiles;

        public Map2D(string p_mapFile)
        { this.loadMap(p_mapFile); }

        public Map2D()
        { m_tiles = new Dictionary<Vector2, GameObject>(); }

        public IEnumerable<GameObject> getDomain(Vector2 p_domain)
        {
            List<GameObject> domain = new List<GameObject>();
            throw new NotImplementedException();
            return domain;
        }

        public bool loadMap(string p_mapFile)
        {
            throw new NotImplementedException();
            try
            {
                m_tiles = new Dictionary<Vector2, GameObject>();
            }
            catch (Exception e)
            { }
            
        }

        public void Draw(SpriteBatch p_sb)
        {
            throw new NotImplementedException();
        }
    }
}
