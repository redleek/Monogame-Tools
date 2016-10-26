using MonoGame_Tools.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace MonoGame_Tools.Dialogue
{
    [Serializable]
    public class DialogueOutput
    {
        const string MISSING_MESSAGE = "WARNING: Dialog Option {0} is missing message";

        int m_id;
        int m_nextId;
        int m_activeSpeaker;
        string m_message;
        string m_script;
        List<int> m_choices;
        TextureChangeCollection m_textureChanges;

        /// <summary>
        /// Gets all choices for this dialog output
        /// </summary>
        public int[] ChoicesIndices
        {
            get { return m_choices.ToArray(); }
        }

        /// <summary>
        /// Gets or sets the message for this dialog output
        /// </summary>
        public string Message
        {
            get
            {
                return m_message;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    m_message = string.Format(MISSING_MESSAGE, m_id);
                else
                    m_message = value;
            }
        }

        /// <summary>
        /// Gets or sets the ID of this dialog output
        /// </summary>
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        /// <summary>
        /// Gets or sets the ID of the next dialog option to go to
        /// This will be used if there are no available choices
        /// </summary>
        public int NextId
        {
            get { return m_nextId; }
            set { m_nextId = value; }
        }

        /// <summary>
        /// Gets or sets the index of the active speaker for this dialogue
        /// </summary>
        public int ActiveSpeaker
        {
            get { return m_activeSpeaker; }
            set { m_activeSpeaker = value; }
        }

        public string Script
        {
            get { return m_script; }
            set { m_script = value; }
        }

        public TextureChangeCollection TexChanges
        {
            get { return m_textureChanges; }
        }

        public DialogueOutput() : this(-1)
        {
        }

        public DialogueOutput(int id)
        {
            m_choices = new List<int>();
            m_textureChanges = new TextureChangeCollection();
            m_id = id;
            m_nextId = -1;
            m_message = string.Format(MISSING_MESSAGE, m_id);
        }

        /// <summary>
        /// Writes this Dialog Option to an XML stream
        /// </summary>
        /// <param name="writer">The XML writer to write to</param>
        public void WriteToXML(XmlWriter writer, bool writeEditor = true)
        {
            // Starts this element
            writer.WriteStartElement("dialog");                             // <dialog

            // Write the ID as an sttribute
            writer.WriteAttributeInt("id", m_id);                           // id="$id"

            // Write the speaker as an attribute
            writer.WriteAttributeInt("speaker", m_activeSpeaker);           //speaker=$activeSpeaker

            // Write the next dialog ID if it exists
            if (m_nextId != -1)
                writer.WriteAttributeString("nextId", m_nextId.ToString()); // nextId="$nextId"

            // Start the choices list
            writer.WriteStartAttribute("choices");                            // <choices>

            // Iterate over each choice and write it
            foreach (int choice in m_choices)
            {
                writer.WriteValue(choice + ",");                            // 1, 2, 3....
            }

            // End the choices list
            writer.WriteEndAttribute();

            // Write the script if it is not null
            if (m_script != null)
            {
                writer.WriteElementString("script", m_script);
            }

            // Only write texture changes if we have anything
            if (m_textureChanges.Count > 0)
            {
                foreach (TextureChangeTag tag in m_textureChanges)
                {
                    writer.WriteStartElement("TextureChange");

                    writer.WriteAttributeString("role", Enum.GetName(typeof(TextureRole), tag.Role));

                    if (tag.TextureName != null)
                        writer.WriteAttributeString("texture", tag.TextureName);

                    writer.WriteEndElement();
                }
            }

            // Write the message as an element
            writer.WriteElementString("message", m_message);                // <message>$message</message>

            // Ends this element
            writer.WriteEndElement();                                       // </dialog>
        }

        /// <summary>
        /// Reads a DialogOption from an XML node
        /// </summary>
        /// <param name="node">The node to read from</param>
        /// <returns>A DialogOption read from the node</returns>
        public static DialogueOutput ReadFromXML(XmlNode node)
        {
            // Throw an exception if the ID is not defined
            if (node.Attributes["id"] == null)
                throw new XmlException("ID is not defined for this node!");

            // Throw an exception if the ID is not defined
            if (node.Attributes["speaker"] == null)
                throw new XmlException("Speaker is not defined for this node!");

            // Define a new Dialog option to return the result in
            DialogueOutput option = new DialogueOutput();

            // Try parsing ID from XML, and throw an exception if it fails
            if (!int.TryParse(node.Attributes["id"].Value, out option.m_id))
                throw new XmlException("Could not parse ID to integer");

            // Try parsing ID from XML, and throw an exception if it fails
            if (!int.TryParse(node.Attributes["speaker"].Value, out option.m_activeSpeaker))
                throw new XmlException("Could not parse Speaker to integer");

            // Try parsing ID from XML, and throw an exception if it fails
            if (node.Attributes["nextId"] != null && !int.TryParse(node.Attributes["nextId"].Value, out option.m_nextId))
                throw new XmlException("Could not parse NextID to integer");

            // Get the choices subnode
            if (node.Attributes["choices"] != null)
            {
                XmlAttribute choices = node.Attributes["choices"];

                string value = choices.InnerText;

                string[] ints = value.Split(',');

                foreach (string val in ints.Where(s => s.Length > 0))
                    option.m_choices.Add(int.Parse(val));
            }

            // Get the script if there is one
            if (node["script"] != null)
            {
                option.m_script = node["script"].Value;
            }

            // As long as the message exists, we load it
            if (node["message"] != null)
                option.m_message = node["message"].InnerText;
            // Otherwise, set it's message to an error text and log the warning
            else
            {
                option.m_message = string.Format(MISSING_MESSAGE, option.m_id);
                Logger.LogMessage(LogMessageType.Warning, "Dialog Option #{0} is missing a message content", option.m_id);
            }


            foreach (XmlNode texNode in node.SelectNodes("TextureChange"))
            {
                TextureChangeTag tag = new TextureChangeTag();

                tag.Role = (TextureRole)Enum.Parse(typeof(TextureRole), texNode.Attributes["role"].Value);

                if (texNode.Attributes["texture"] != null)
                    tag.TextureName = texNode.Attributes["texture"].Value;

                option.m_textureChanges[tag.Role] = tag;
            }

            // return the result
            return option;
        }

        public bool RemoveChoice(DialogueChoice dialogChoice)
        {
            return m_choices.Remove(dialogChoice.Id);
        }

        public void AddChoice(DialogueChoice dialogChoice)
        {
            m_choices.Add(dialogChoice.Id);
        }

        public void AddTextureChange(TextureChangeTag tag)
        {
            m_textureChanges[tag.Role] = tag;
        }

        public string GetMessage(LuaContext context)
        {
            return ScriptingTools.GetTextEmbedded(m_message, context);
        }

        public override string ToString()
        {
            return string.Format("ID: {0} | Message: {1} | Choices: {2}", m_id, m_message, m_choices.Count);
        }
    }
}
