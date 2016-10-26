using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame_Tools.Scripting
{
    /// <summary>
    /// Representing a Lua Scripting Context - Matthew
    /// </summary>
    public class LuaContext
    {
        /// <summary>
        /// Underlying Lua context to use - Matthew
        /// </summary>
        Lua m_luaContext;

        /// <summary>
        /// Create a new lua context - Matthew
        /// </summary>
        public LuaContext()
        {
            m_luaContext = new Lua();
            Logger.LogMessage("Starting Lua version {0}", m_luaContext.DoString("return _VERSION"));

            m_luaContext.LoadCLRPackage();
            Logger.LogMessage("Loaded Lua CLR");
        }

        /// <summary>
        /// Register a global variable with a given name to the context - Matthew
        /// </summary>
        /// <param name="name">The name of the global variable to register</param>
        public void RegisterVariable(string name)
        {
            m_luaContext.DoString(string.Format("{0}=null", name));
            Logger.LogMessage(LogMessageType.Script, "Registering global Lua variable \"{0}\"", name);
        }

        /// <summary>
        /// Registers a global variable with a given name to the context - Matthew
        /// </summary>
        /// <typeparam name="T">The C# type to store</typeparam>
        /// <param name="name">Name ofglobal variable to register</param>
        /// <param name="initValue">The initial value of the variable</param>
        public void RegisterVariable<T>(string name, T initValue)
        {
            m_luaContext.DoString(string.Format("{0}=null", name));
            m_luaContext[name] = initValue;
            Logger.LogMessage(LogMessageType.Script, "Registering global Lua variable \"{0}\"", name);
        }

        /// <summary>
        /// Set a global variable to the given value - Matthew
        /// </summary>
        /// <typeparam name="T">C# type of value to set</typeparam>
        /// <param name="name">Name of global variable </param>
        /// <param name="value">Value to set variable to</param>
        public void SetVariable<T>(string name, T value)
        {
            m_luaContext[name] = value;
        }

        /// <summary>
        /// Get the global variable with given name - Matthew
        /// </summary>
        /// <typeparam name="T">C# type to return </typeparam>
        /// <param name="name">The name of the variable to get</param>
        /// <returns>The global variable with the given name</returns>
        public T GetVariable<T>(string name)
        {
            return (T)m_luaContext[name];
        }

        /// <summary>
        /// Performs a lua function - Matthew
        /// </summary>
        /// <param name="function">The lua function to perform</param>
        /// <returns>The result of the script</returns>
        public object[] DoString(string function)
        {
            return m_luaContext.DoString(function);
        }

        /// <summary>
        /// Performs the context of the lua file - Matthew
        /// </summary>
        /// <param name="path">The path of the lua script file</param>
        /// <returns>Result of the script</returns>
        public object[] DoFile(string path)
        {
            return m_luaContext.DoFile(path);
        }

        /// <summary>
        /// Create lua function object from file - Matthew
        /// </summary>
        /// <param name="path">The path of the lua script file</param>
        /// <returns>A LuaFunction created from the given path</returns>
        public LuaFunction LoadFile(string path)
        {
            return m_luaContext.LoadFile(path);
        }

        /// <summary>
        /// Creates a lua function object from a string - Matthew
        /// </summary>
        /// <param name="function">Text containing the lua function</param>
        /// <param name="commandName">The name for this lua command</param>
        /// <returns>A LuaFunction created from the given string</returns>
        public LuaFunction LoadString(string function, string commandName)
        {
            return m_luaContext.LoadString(function, commandName);
        }

        /// <summary>
        /// Registers a lua function for use by all lua functions - Matthew
        /// </summary>
        /// <param name="function">The lua function to register </param>
        /// <param name="commandName">The name to give to this Lua function</param>
        public void RegisterLuaFunction(string function, string commandName)
        {
            RegisterVariable(commandName);
            SetVariable(commandName, m_luaContext.LoadString(function, commandName));
            Logger.LogMessage(LogMessageType.Script, "Registering global Lua function \"{0}\"", commandName);
        }

        /// <summary>
        /// Registers a lua function for use by all lua functions
        /// </summary>
        /// <param name="function"></param>
        /// <param name="commandName"></param>
        public void RegisterLuaFunction(LuaFunction function, string commandName)
        {
            RegisterVariable(commandName);
            SetVariable(commandName, function);
            Logger.LogMessage(LogMessageType.Script, "Registering global Lua function \"{0}\"", commandName);
        }
    }
}