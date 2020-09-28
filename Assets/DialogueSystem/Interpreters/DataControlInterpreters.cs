using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.DialogueSystem.Interpreter
{
    public static class DataControlInterpreters
    {
        const string localVariable = "local";
        const string globalVariable = "global";

        public enum Operation
        {
            set, add, sub, mul, div, mod
        }

        public static Dictionary<string, InterpreterFunc> functionMap = new Dictionary<string, InterpreterFunc>() {
            {localVariable, ChangeLocalVariable },
            {globalVariable, ChangeGlobalVariable }
        };

        private static object ChangeLocalVariable(JSONNode args, DialogueEvent evt)
        {
            foreach (KeyValuePair<string, JSONNode> kv in args.AsObject)
            {
                long original = evt.GetEventVariables(kv.Key);
                Enum.TryParse(kv.Value["operation"], out Operation _op);
                evt.SetEventVariables(kv.Key, UpdateVariable(_op, original, kv.Value["value"].AsLong));
            }
            return null;
        }

        static long UpdateVariable(Operation op, long original, long target)
        {
            switch (op)
            {
                case Operation.set:
                    return target;
                case Operation.add:
                    return original + target;
                case Operation.div:
                    return original / target;
                case Operation.mod:
                    return original % target;
                case Operation.mul:
                    return original * target;
                case Operation.sub:
                    return original - target;
                default: return 0;
            }
        }

        private static object ChangeGlobalVariable(JSONNode args, DialogueEvent evt)
        {
            return null;
        }
    }
}