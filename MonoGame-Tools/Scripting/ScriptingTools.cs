using System;
using MonoGame_Tools.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MonoGame_Tools.Scripting
{
    public class ScriptingTools
    {
        public static string GetTextEmbedded(string text, LuaContext context)
        {
            string message = text;

            // Get the regex matches for %xxx%
            MatchCollection matches = Regex.Matches(message, @"[^\\]%([^%]*)%");

            foreach (Match command in matches)
            {
                if (message.Contains(command.Value))
                {
                    string luaCommand = command.Value.Trim('%', command.Value[0]);

                    if (!luaCommand.Contains("return "))
                    {
                        luaCommand = luaCommand.Insert(0, "return ");
                    }

                    string result = (string)context.DoString(luaCommand)[0];

                    message = message.Replace(command.Value.Trim(command.Value[0]), result);
                }
            }

            message = message.Replace("\\%", "%");

            return message;
        }
    }
}
