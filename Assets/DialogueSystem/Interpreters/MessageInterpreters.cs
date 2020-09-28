using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEditor.VersionControl;
using UnityEngine;
namespace Kultie.DialogueSystem.Interpreter
{
    public static class MessageInterpreters
    {

        const string showText = "showtext";
        const string choices = "choices";

        public static Dictionary<string, InterpreterFunc> functionMap = new Dictionary<string, InterpreterFunc>() {
            {showText, ShowText },
            {choices, ShowChoice }
        };

        private static object ShowChoice(JSONNode args, DialogueEvent evt)
        {
            JSONArray choices = args["choices"].AsArray;
            return new DialogueChoiceModel()
            {
                message = args["message"],
                choices = choices
            };
        }

        private static object ShowText(JSONNode args, DialogueEvent evt)
        {
            string message = args["message"];
            message = ConvertMessage(message, evt);
            return new DialogueModel()
            {
                message = message
            };
        }

        private static string ConvertMessage(string str, DialogueEvent evt)
        {
            if (string.IsNullOrEmpty(str)) return str;
            var depthItems = new Dictionary<int, int>();
            int depth = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '[')
                {
                    depth++;
                    depthItems.Add(depth, i);
                }
                else if (str[i] == ']')
                {
                    if (!depthItems.ContainsKey(depth))
                    {
                        continue;
                    }
                    int item = depthItems[depth];

                    var current = str.Substring(item, i - item);
                    current = current.Replace("[", string.Empty);
                    current = current.Replace("]", string.Empty);
                    string[] replaceArgs = current.Split('_');
                    if (replaceArgs.Length > 1)
                    {
                        string locale = replaceArgs[0];
                        string name = replaceArgs[1];
                        if (!string.IsNullOrEmpty(current))
                        {                           
                            str = str.Replace(current, GetMessageConvert(locale, name, evt).ToString());
                        }
                    }
                    i = depthItems[depth];
                    depthItems.Remove(depth);
                    depth--;
                }
            }
            str = str.Replace("[", string.Empty);
            str = str.Replace("]", string.Empty);
            return str;
        }

        private static object GetMessageConvert(string locale, string name, DialogueEvent evt)
        {
            Debug.Log(locale + "_" + name);
            if (locale == "l")
            {
                return evt.GetEventVariables(name);
            }
            return string.Empty;
        }
    }
}