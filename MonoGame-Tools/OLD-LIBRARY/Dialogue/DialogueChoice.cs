using MonoGame_Tools.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MonoGame_Tools.Dialogue
{
    /// <summary>
    /// Represents a choice in a branching dialog tree - Matthew
    /// </summary>
    public class DialogueChoice : IEquatable<DialogueChoice>
    {
        const string DEFAULT_MESSAGE = "RESPONSE NOT FOUND";

        private int m_id;
        private int m_nextId;
        public string m_condition;
        public string m_message;

        public List<DialogueCommand> m_commands;

        /// <summary>
        /// Gets or sets the ID for this choice - Matthew
        /// </summary>
        public int Id
        {
            get { return m_id; }
            set { m_id = value;}
        }

        /// <summary>
        /// Gets or sets the ID of the dialog option that this choice points to - Matthew [-1 is the end of the dialogue tree this should always kill.]
        /// </summary>
        public int NextId
        {
            get { return m_nextId; }
            set { m_nextId = value;}
        }

        /// <summary>
        /// Creates a new empty Dialog option
        /// </summary>
        public DialogueChoice()
        {
            m_commands = new List<DialogueCommand>();
            m_message = DEFAULT_MESSAGE;

            m_nextId = -1;
            m_condition = "true";
        }

        public DialogueChoice(string message) : this()
        {
            m_message = message;
        }

        /// <summary>
        /// Checks if this choice's conditions are met - Matthew
        /// </summary>
        /// <param name="context">The Lua context to use for checking conditions</param>
        /// <returns>True if the choice's conditions are met, otherwise false</returns>
        public bool IsAvailable(LuaContext context)
        {
            return (bool)context.DoString(string.Format("return {0}", m_condition))[0];
        }


        /// <summary>
        /// Performs the actions tied to this choice if the choice is available
        /// </summary>
        /// <param name="context">The Lua context to use for excecuting commands</param>
        public void PerformCommands(LuaContext context)
        {
            if (IsAvailable(context))
            {
                foreach (DialogueCommand command in m_commands)
                {
                    command.Perform(context);
                }
            }
        }

        /// <summary>
        /// Writes this choice to an XML stream
        /// </summary>
        /// <param name="writer">The XML writer to write to</param>
        public void WriteToXML(XmlWriter writer, bool writeEditor = true)
        {
            writer.WriteStartElement("choice"); // <choice

            writer.WriteAttributeString("id", m_id.ToString()); //id = "$id"

            writer.WriteAttributeString("nextId", m_nextId.ToString()); //nextId = "$nextId"

            if (m_condition != "true")
            {
                writer.WriteAttributeString("condition", m_condition); // condition= "$condition"
            }

            if (m_message != DEFAULT_MESSAGE)
            {
                writer.WriteElementString("message", m_message); // <message>$message</message>
            }

            writer.WriteStartElement("commands"); // <commands>

            // Iterate over each command and write it
            foreach (DialogueCommand command in m_commands)
            {
                command.WriteToXML(writer); //  <command.... />
            }

            // Close the commands list
            writer.WriteEndElement();                                           // </commands>

            // Close this element
            writer.WriteEndElement(); // </choice>
        }

        /// <summary>
        /// Reads a DialogChoice from an XML node
        /// </summary>
        /// <param name="node">The node to read from</param>
        /// <returns>A DialogChoice read from the node</returns>
        public static DialogueChoice ReadFromXML(XmlNode node)
        {
            DialogueChoice choice = new DialogueChoice();

            if (node.Attributes["id"] != null)
            {
                choice.m_id = int.Parse(node.Attributes["id"].Value);
            }

            // If the Next ID attribute exists, read it
            if (node.Attributes["nextId"] != null)
            {
                choice.m_nextId = int.Parse(node.Attributes["nextId"].Value);
            }

            // If the condition attribute exists, read it
            if (node.Attributes["condition"] != null)
            {
                choice.m_condition = node.Attributes["condition"].Value;
            }

            // If the message node exists, save it to the output
            if (node["message"] != null)
            {
                choice.m_message = node["message"].InnerText;
            }

            // Get the commands subnode
            XmlNode commands = node.SelectSingleNode("commands");

            // If the commands list is defined
            if (commands != null)
            {
                // Iterate over all subnodes
                foreach (XmlNode commandNode in commands)
                {
                    // Load the command from the subnode
                    choice.m_commands.Add(DialogueCommand.ReadFromXML(commandNode));
                }
            }

            // Return the result
            return choice;
        }


        /// <summary>
        /// Creates a human-readable string representation of this Dialog choice
        /// </summary>
        /// <returns>A string representation of this instance</returns>
        public override string ToString()
        {
            return string.Format("Dialog ID: {0} | Commands: {1} | Condition: {2}", m_nextId, m_commands.Count, m_condition);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(DialogueChoice))
                return false;
            else
            {
                DialogueChoice choice = (DialogueChoice)obj;
                return this.Equals(choice);
            }
        }

        public override int GetHashCode()
        {
            return m_message.GetHashCode();
        }

        public bool Equals(DialogueChoice choice)
        {
            return choice.m_message == m_message & choice.m_nextId == m_nextId & choice.m_condition == m_condition & choice.m_commands == m_commands;
        }

        public string GetMessage(LuaContext context)
        {
            return ScriptingTools.GetTextEmbedded(m_message, context);
        }
    }
}
