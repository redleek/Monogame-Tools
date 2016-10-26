using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Tools.MapTools
{
    interface IMapWindow
    {
        uint Width
        { get; set; }
        uint Height
        { get; set; }
        void Draw(SpriteBatch p_sb, object p_region);
    }
}
