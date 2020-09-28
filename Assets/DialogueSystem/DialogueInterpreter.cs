using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.DialogueSystem.Interpreter
{
    public delegate object InterpreterFunc(JSONNode args, DialogueEvent evt);
    public class DialogueInterpreter
    {
        static bool DebugMode = true;

        const string messageType = "message";

        JSONArray commands;

        public object RunCommand(JSONNode command, DialogueEvent evt)
        {
            string commandName = command["command"].Value;
            string[] command_args = commandName.Split('_');
            JSONNode args = command["arguments"];
            return GetFunction(command_args[0], command_args[1]).Invoke(args, evt);
        }

        private InterpreterFunc GetFunction(string nameSpace, string functionName)
        {
            if (DebugMode)
            {
                Debug.Log("Processing: " + nameSpace + ": " + functionName);
            }

            switch (nameSpace)
            {
                case messageType:
                    return MessageInterpreters.functionMap[functionName];
                default:
                    return null;
            }

        }
    }
}