using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGame_Tools.Utils;

namespace MonoGame_Tools.Dialogue
{
    public class TextureChangeTag
    {
        string m_textureName;
        TextureRole m_role;

        public string TextureName
        {
            get { return m_textureName; }
            set { m_textureName = value; }
        }
        public TextureRole Role
        {
            get { return m_role; }
            set { m_role = value; }
        }

        public TextureChangeTag()
        {
            m_textureName = null;
            m_role = TextureRole.Background;
        }

        public Texture2D GetTexture()
        {
            if (TextureName == null)
                return null;
            else
                return Global.Textures[TextureName];
        }
    }

    public enum TextureRole
    {
        Speaker0,
        Speaker1,
        Speaker2,
        Speaker3,
        Speaker4,
        Speaker5,
        Speaker6,
        Speaker7,
        Background,
        TextPanel
    }
}
