using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame_Tools.Utils
{
    public class TextureCache
    {
        Dictionary<string, Texture2D> m_textures;
        ContentManager m_content;

        public Texture2D this[string index]
        {
            get
            {
                if (!m_textures.ContainsKey(index))
                {
                    try
                    {
                        Texture2D content = m_content.Load<Texture2D>(index);
                        m_textures.Add(index, content);
                    }
                    catch (ContentLoadException)
                    {
                        m_textures.Add(index, null);
                        Logger.LogMessage(LogMessageType.Warning, "Cannot load content \"{0}\", setting to null", index);
                    }
                }

                return m_textures[index];
            }
        }

        public TextureCache(ContentManager content)
        {
            m_textures = new Dictionary<string, Texture2D>();
            m_content = content;
        }

        public void Unload(string index)
        {
            if (m_textures.ContainsKey(index))
            {
                Texture2D texture = m_textures[index];
                texture.Dispose();
                m_textures.Remove(index);
            }
        }
    }
}
