using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame_Tools.Utils
{
    public class TextureCache<T>
    {
        Dictionary<string, T> m_textures;
        ContentManager m_content;

        public T this[string index]
        {
            get
            {
                if (!m_textures.ContainsKey(index))
                {
                    try
                    {
                        T content = m_content.Load<T>(index);
                        m_textures.Add(index, content);
                    }
                    catch (ContentLoadException)
                    {
                        Logger.LogMessage(LogMessageType.Warning, "Cannot load content \"{0}\", setting to null", index);
                    }
                }

                return m_textures[index];
            }
        }

        public TextureCache(ContentManager content)
        {
            m_textures = new Dictionary<string, T>();
            m_content = content;
        }

        public void Load(string p_contentName)
        {
            T newTexture = m_content.Load<T>(p_contentName);
            m_textures.Add(p_contentName, newTexture);
        }

        public void Unload(string index)
        {
            m_content.Unload();
        }
    }
}
