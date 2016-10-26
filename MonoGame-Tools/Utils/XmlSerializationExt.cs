using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MonoGame_Tools
{
    public static class XmlSerializationExt
    {
        public static void WriteAttributeInt(this XmlWriter writer, string name , int value)
        {
            writer.WriteStartAttribute(name);
            writer.WriteValue(value);
            writer.WriteEndAttribute();
        }
    }
}
