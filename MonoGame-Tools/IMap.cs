using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame_Tools
{
    interface IMap
    {
        bool loadMap(string p_mapFile);
        IEnumerable<GameObject> getDomain(Vector2 p_domain);
    }
}
