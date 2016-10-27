using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame_Tools.MapTools
{
    /// <summary>
    /// Specify a layer of tiles
    /// </summary>
    public class Layer
    {
        private IDictionary<Vector2, Tile2D> m_tiles;
        
        public string Name
        {
            get;
            set;
        }
    }
}
