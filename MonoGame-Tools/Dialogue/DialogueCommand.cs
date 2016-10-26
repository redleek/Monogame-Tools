using MonoGame_Tools.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MonoGame_Tools.Dialogue
{
    public class DialogueCommand
    {
        string m_command;
        string m_condition;

        public string Command
        {
            get { return m_command; }
            set { m_command = value; }
        }

        public string Condition
        {
            get { return m_condition; }
            set
            {
                if (value.ToLower().Trim() == "true" || string.IsNullOrEmpty(value))
                {
                    m_condition = "true";
                }
                else
                {
                    m_condition = value;
                }
            }
        }

        public DialogueCommand()
        {
            m_command = "";
            m_condition = "true";
        }

        public DialogueCommand(string command)
        {
            Command = command;
            m_condition = "true";
        }

        public DialogueCommand(string command, string condition)
        {
            Command = command;
            Condition = condition;
        }

        public bool ConditionsMet(LuaContext context)
        {
            return (bool)context.DoString("return " + m_condition)[0];
        }

        public void Perform(LuaContext context)
        {
            if (ConditionsMet(context))
            {
                context.DoString(m_command);
            }
        }

        public void WriteToXML(XmlWriter writer)
        {
            // Only save this thing if there is actually a command to be excecuted
            if (!string.IsNullOrEmpty(m_command))
            {
                // Write the start of this command
                writer.WriteStartElement("command");                            // <command

                // Only write the condition attribute if there is one
                if (m_condition != "true")
                    writer.WriteAttributeString("condition", m_condition);      // condition="$condition"

                // Write the actual command as the value
                writer.WriteValue(m_command);                                   // >$command

                // End this command element
                writer.WriteEndElement();                                       // </command>
            }
        }

        public static DialogueCommand ReadFromXML(XmlNode node)
        {
            // Define a new command to return the result in
            DialogueCommand command = new DialogueCommand();

            // Set the command to the node's inner text
            command.m_command = node.InnerText;

            // If the node has the condtion attribute defined, load it's value into the condition
            if (node.Attributes["condition"] != null)
                command.m_condition = node.Attributes["condition"].Value;

            // Return the result
            return command;

        }

        public override string ToString()
        {
            return m_command + (m_condition != "true" ? " if " + m_condition : "");
        }

        public static implicit operator DialogueCommand(string command)
        {
            return new DialogueCommand(command);
        }
    }
}