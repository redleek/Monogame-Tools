using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Tools.MapTools
{
    public interface IMap
    {
        bool loadMap(string p_mapFile);
        string Name
        { get; }
        IEnumerable<GameObject> getDomain(object p_domain);
        void Draw(SpriteBatch p_sb);
        void drawDomain(SpriteBatch p_sb, object p_domain);
    }
}
