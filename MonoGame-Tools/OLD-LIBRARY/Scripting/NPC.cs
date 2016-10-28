using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Tools.Scripting
{
    public class NPC : Entity
    {
        string m_name;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
    }
}
