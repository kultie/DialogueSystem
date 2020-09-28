using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
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
            return new DialogueModel()
            {
                message = args["message"]
            };
        }
    }
}