using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Tools.MapTools
{
    /// <summary>
    /// Map class for storing a map layout.
    /// </summary>
    class Map2D : IMap
    {
        private uint m_tileWidth, m_tile_height;
        private IList<Layer> m_layers;

        public Map2D(ContentManager p_cm, string p_mapFile)
        { loadMap(p_cm, p_mapFile); }

        public Map2D()
        { m_layers = new List<Layer>(); }

        /// <summary>
        /// Name of map.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

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
        public IEnumerable<Layer> getDomain(object p_domain)
        {
            // Since multiple layers, get domain from each layer
            List<Layer> domainLayers = new List<Layer>();
            foreach (Layer layer in m_layers)
                domainLayers.Add(
                    layer.getDomain((Vector2)p_domain)
                    );
            return domainLayers;
        }

        /// <summary>
        /// Load a map from file.
        /// </summary>
        /// <param name="p_mapFile">Name and/or directory of map.</param>
        /// <returns>Did map successfully load?</returns>
        public bool loadMap(ContentManager p_cm, string p_mapFile)
        {
            throw new NotImplementedException();
            bool success = true;

            const string xmlExtension = ".xml";
            XmlDocument xmlDoc = null;
            try
            {
                m_layers = new List<Layer>(); ;
                xmlDoc = new XmlDocument();
                string path = string.Format(
                    "{0}/{1}{2}", p_cm.RootDirectory, p_mapFile.Trim(), xmlExtension
                    );
                xmlDoc.Load(path);
                XmlLoad(xmlDoc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Extract information from 
        /// </summary>
        /// <param name="p_doc"></param>
        private void XmlLoad(XmlDocument p_doc)
        {

        }

        /// <summary>
        /// Draw the entire map
        /// </summary>
        /// <param name="p_sb"></param>
        public virtual void Draw(SpriteBatch p_sb)
        {
            foreach (Layer layer in m_layers)
                layer.Draw(p_sb);
        }

        /// <summary>
        /// Draw a chunk of the map.
        /// </summary>
        public virtual void drawDomain(SpriteBatch p_sb, object p_domain)
        {
            throw new NotImplementedException();
        }
    }
}
