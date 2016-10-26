using MonoGame_Tools.Scripting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MonoGame_Tools.Dialogue
{
    /// <summary>
    /// Represents a scene of branching or linear dialog
    /// </summary>
    public class DialogueScene
    {
        private static readonly Dictionary<TextureRole, ScreenSpaceRect> TEXTURE_LOCATIONS;

        static DialogueScene()
        {
            TEXTURE_LOCATIONS = new Dictionary<TextureRole, ScreenSpaceRect>()
            {
                { TextureRole.Background, new ScreenSpaceRect(0, 0, 1, 1) },
                { TextureRole.TextPanel, new ScreenSpaceRect(0, 0.7f, 1, 0.3f) },
                { TextureRole.Speaker0, new ScreenSpaceRect(0.000f, 0.55f, 0.25f, 0.25f) },
                { TextureRole.Speaker1, new ScreenSpaceRect(0.125f, 0.55f, 0.25f, 0.25f) },
                { TextureRole.Speaker2, new ScreenSpaceRect(0.250f, 0.55f, 0.25f, 0.25f) },
                { TextureRole.Speaker3, new ScreenSpaceRect(0.325f, 0.55f, 0.25f, 0.25f) },
                { TextureRole.Speaker4, new ScreenSpaceRect(0.500f, 0.55f, 0.25f, 0.25f) },
                { TextureRole.Speaker5, new ScreenSpaceRect(0.625f, 0.55f, 0.25f, 0.25f) },
                { TextureRole.Speaker6, new ScreenSpaceRect(0.750f, 0.55f, 0.25f, 0.25f) },
                { TextureRole.Speaker7, new ScreenSpaceRect(0.875f, 0.55f, 0.25f, 0.25f) }

            };

        }

        const string DEFAULT_NAME = "UNNAMED_SCENE";
        const string DEFAULT_SELECTOR = "return 0";
        static readonly DialogueOutput UNKNOWN_CHOICE = new DialogueOutput() { Message = "NO DIALOG OPTION FOUND!" };

        string m_name;
        string m_initDialogSelector;

        int m_currentDialog;

        LuaContext m_luaContext;

        DialogueChoiceCollection m_choices;
        DialogueOutputCollection m_dialogs;

        Dictionary<TextureRole, Texture2D> myTextures;

        public Dictionary<TextureRole, Texture2D> Textures
        {
            get { return myTextures; }
        }

        /// <summary>
        /// Gets the dialog option with the given ID from this scene
        /// </summary>
        /// <param name="id">The ID of the dialog option to search for</param>
        /// <returns>The first dialog option with the given ID</returns>
        public DialogueOutput this[int id]
        {
            get { return m_dialogs[id]; }
        }

        /// <summary>
        /// Gets the currently active dialog option
        /// </summary>
        public DialogueOutput CurrentDialog
        {
            get
            {
                DialogueOutput found = this[m_currentDialog];

                return found == null ? UNKNOWN_CHOICE : found;
            }
            protected set
            {
                m_currentDialog = value.Id;
                UpdateImagery();
            }
        }

        /// <summary>
        /// Gets the choices for the player in the current dialog
        /// </summary>
        public DialogueChoice[] CurrentChoices
        {
            get
            {
                List<DialogueChoice> choices = new List<DialogueChoice>();

                foreach (int index in CurrentDialog.ChoicesIndices)
                    choices.Add(m_choices[index]);

                return choices.Where(choice => choice.IsAvailable(m_luaContext)).ToArray();
            }
        }

        /// <summary>
        /// Gets the name of this scene
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// Gets or sets the Lua script to use for selecting the initial dialog.
        /// This must return a number
        /// </summary>
        public string Selector
        {
            get { return m_initDialogSelector; }
            set { m_initDialogSelector = value; }
        }

        /// <summary>
        /// Gets a list of all dialog outputs in this scene
        /// </summary>
        public DialogueOutputCollection Dialogs
        {
            get { return m_dialogs; }
        }

        /// <summary>
        /// Gets a list of all dialog choices in this scene
        /// </summary>
        public DialogueChoiceCollection Choices
        {
            get { return m_choices; }
        }

        /// <summary>
        /// Creates a new dialog scene with the default name
        /// </summary>
        /// <param name="context">The Lua context to use for running sripts</param>
        public DialogueScene(LuaContext context) : this(context, DEFAULT_NAME)
        {
        }

        /// <summary>
        /// Creates a new dialog scene
        /// </summary>
        /// <param name="context">The Lua context to use for running sripts</param>
        /// <param name="name">The name of the scene, for debugging purposes</param>
        public DialogueScene(LuaContext context, string name)
        {
            m_name = name;
            m_initDialogSelector = DEFAULT_SELECTOR;
            m_luaContext = context;

            m_dialogs = new DialogueOutputCollection();
            m_choices = new DialogueChoiceCollection();
            m_dialogs.Add(UNKNOWN_CHOICE);

            myTextures = new Dictionary<TextureRole, Texture2D>();
        }

        /// <summary>
        /// Add a new dialog to this scene
        /// </summary>
        /// <param name="dialog">The dialog to add</param>
        public void AddDialogOption(DialogueOutput dialog)
        {
            m_dialogs.Add(dialog);
        }

        /// <summary>
        /// Removes a dialog output from this scene
        /// </summary>
        /// <param name="dialogOut">The output to remove</param>
        public void DeleteDialogOption(DialogueOutput dialogOut)
        {
            m_dialogs.Remove(dialogOut);
        }

        public void AddChoice(DialogueChoice dialogChoice)
        {
            dialogChoice.Id = m_choices.Count();
            m_choices.Add(dialogChoice);
        }

        public bool DeleteChoice(DialogueChoice choice)
        {
            return m_choices.Remove(choice);
        }

        /// <summary>
        /// Gets the first available ID for a Dialog Outpur
        /// </summary>
        /// <returns></returns>
        public int GetFirstAvailableId()
        {
            int index = -1;

            while (this[index] != null)
                index++;

            return index;
        }

        /// <summary>
        /// Gets the first available ID for a Dialog Choice
        /// </summary>
        /// <returns></returns>
        public int GetFirstAvailableChoiceId()
        {
            int index = 0;

            while (Choices[index] != null)
                index++;

            return index;
        }

        /// <summary>
        /// Makes this scene go to it's first Dialog Output
        /// </summary>
        public void GotoFirst()
        {
            object id = m_luaContext.DoString(m_initDialogSelector)[0];

            if (id.GetType() == typeof(Double))
                CurrentDialog = this[(int)((double)id)];
            else
            {
                Logger.LogMessage(LogMessageType.Warning, "DialogScene \"{0}\" has a selector that does not return a number, instead returns {1}", m_name, id.GetType().Name);
                CurrentDialog = UNKNOWN_CHOICE;
            }
        }

        /// <summary>
        /// Gets the parent dialog for the given choice, or null if no parent is found
        /// </summary>
        /// <param name="choice">The choice to search for</param>
        /// <returns>The output that contains the choice, or null</returns>
        public DialogueOutput GetParentForChoice(DialogueChoice choice)
        {
            foreach (DialogueOutput dialog in Dialogs)
            {
                if (dialog.ChoicesIndices.Contains(choice.Id))
                    return dialog;
            }
            return null;
        }

        /// <summary>
        /// Perform the actions for a given choice and move dialog to the next part
        /// </summary>
        /// <param name="dialogChoice">The choice to perform</param>
        public void SelectChoice(DialogueChoice dialogChoice)
        {
            dialogChoice.PerformCommands(m_luaContext);
            CurrentDialog = this[dialogChoice.NextId];
        }

        /// <summary>
        /// Writes this Dialog Scene to an XML document
        /// </summary>
        /// <param name="writer">The XML writer to write to</param>
        public void WriteToXML(XmlWriter writer, bool writeEditor = true)
        {
            writer.WriteStartElement("scene");

            if (m_name != DEFAULT_NAME)
                writer.WriteAttributeString("name", m_name);

            if (m_initDialogSelector != DEFAULT_SELECTOR)
                writer.WriteElementString("selector", m_initDialogSelector);

            writer.WriteStartElement("dialogues");

            foreach (DialogueOutput option in Dialogs)
            {
                if (option != UNKNOWN_CHOICE)
                    option.WriteToXML(writer, writeEditor);
            }

            writer.WriteEndElement();

            writer.WriteStartElement("choices");

            foreach (DialogueChoice choice in m_choices)
            {
                choice.WriteToXML(writer, writeEditor);
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        /// <summary>
        /// Reads a Dialog Scene from an XML document
        /// </summary>
        /// <param name="node">The node that represents this scene</param>
        /// <param name="context">The Lua context to use for this scene</param>
        /// <returns>A dialog Scene as read from the document</returns>
        public static DialogueScene ReadFromXML(XmlNode node, LuaContext context)
        {
            DialogueScene scene = new DialogueScene(context);

            if (node.Attributes["name"] != null)
                scene.m_name = node.Attributes["name"].Value;

            if (node["selector"] != null)
                scene.m_initDialogSelector = node["selector"].InnerText;

            XmlNode dialogs = node["dialogues"];

            foreach (XmlNode dialogNode in dialogs)
            {
                if (dialogNode.Name == "dialog")
                {
                    scene.m_dialogs.Add(DialogueOutput.ReadFromXML(dialogNode));
                }
            }

            XmlNode choices = node["choices"];

            foreach (XmlNode choiceNode in choices)
            {
                if (choiceNode.Name == "choice")
                {
                    scene.m_choices.Add(DialogueChoice.ReadFromXML(choiceNode));
                }
            }

            return scene;
        }

        public void MoveNext()
        {
            if (CurrentDialog.NextId != -1)
            {
                CurrentDialog = this[CurrentDialog.NextId];
            }
            else
            {
                DeleteDialogOption(CurrentDialog);
            }
        }

        public void End()
        {

        }

        private void UpdateImagery()
        {
            TextureChangeCollection changes = CurrentDialog.TexChanges;

            foreach (TextureRole role in Enum.GetValues(typeof(TextureRole)))
            {
                if (CurrentDialog.NextId == -1 && CurrentDialog.Message == UNKNOWN_CHOICE.Message)
                {
                    myTextures[role] = null;
                }
                myTextures[role] = changes[role] != null ? changes[role].GetTexture() : (myTextures.ContainsKey(role) ? myTextures[role] : null);
            }

            BackgroundTexture = changes[TextureRole.Background] != null ? changes[TextureRole.Background].GetTexture() : BackgroundTexture;
            Speaker0Texture = changes[TextureRole.Speaker0] != null ? changes[TextureRole.Speaker0].GetTexture() : Speaker0Texture;
            Speaker1Texture = changes[TextureRole.Speaker1] != null ? changes[TextureRole.Speaker1].GetTexture() : Speaker1Texture;
            Speaker2Texture = changes[TextureRole.Speaker2] != null ? changes[TextureRole.Speaker2].GetTexture() : Speaker2Texture;
            Speaker3Texture = changes[TextureRole.Speaker3] != null ? changes[TextureRole.Speaker3].GetTexture() : Speaker3Texture;
            Speaker4Texture = changes[TextureRole.Speaker4] != null ? changes[TextureRole.Speaker4].GetTexture() : Speaker4Texture;
            Speaker5Texture = changes[TextureRole.Speaker5] != null ? changes[TextureRole.Speaker5].GetTexture() : Speaker5Texture;
            Speaker6Texture = changes[TextureRole.Speaker6] != null ? changes[TextureRole.Speaker6].GetTexture() : Speaker6Texture;
            Speaker7Texture = changes[TextureRole.Speaker7] != null ? changes[TextureRole.Speaker7].GetTexture() : Speaker7Texture;
            TextPanelTexture = changes[TextureRole.TextPanel] != null ? changes[TextureRole.TextPanel].GetTexture() : TextPanelTexture;



        }

        public void Render(SpriteBatch batch, SpriteFont font)
        {
            UpdateImagery();

            foreach (TextureRole role in Enum.GetValues(typeof(TextureRole)))
            {
                if (Textures.ContainsKey(role) && Textures[role] != null)
                {
                    batch.Draw(Textures[role], TEXTURE_LOCATIONS[role].ToWorld(batch.GraphicsDevice.Viewport), Color.White);
                }

            }

            if (CurrentDialog.NextId == -1 && CurrentDialog.Message == UNKNOWN_CHOICE.Message)
            {
                // Do nothing.
            }
            else
            {
                batch.DrawString(font, CurrentDialog.GetMessage(m_luaContext), new Vector2(5, (int)(batch.GraphicsDevice.Viewport.Height * 0.75f)), Color.Black);
            }

            //if (CurrentDialog.NextId != -1 && CurrentDialog.Message != UNKNOWN_CHOICE.Message)
            //{
            //    batch.DrawString(font, CurrentDialog.GetMessage(m_luaContext), new Vector2(5, (int)(batch.GraphicsDevice.Viewport.Height * 0.75f)), Color.Black);
            //}
        }


        public Texture2D BackgroundTexture { get; protected set; }
        public Texture2D Speaker0Texture { get; protected set; }
        public Texture2D Speaker1Texture { get; protected set; }
        public Texture2D Speaker2Texture { get; protected set; }
        public Texture2D Speaker3Texture { get; protected set; }
        public Texture2D Speaker4Texture { get; protected set; }
        public Texture2D Speaker5Texture { get; protected set; }
        public Texture2D Speaker6Texture { get; protected set; }
        public Texture2D Speaker7Texture { get; protected set; }
        public Texture2D TextPanelTexture { get; protected set; }
    }
}
