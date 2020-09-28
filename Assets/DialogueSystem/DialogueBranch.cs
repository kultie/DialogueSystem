using Kultie.DialogueSystem.Interpreter;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.DialogueSystem
{
    public class DialogueBranch
    {
        DialogueEvent evt;
        DialogueInterpreter interpreter;
        int currentIndex;
        JSONArray commandList;
        DialogueBranch fallbackBranch;

        public DialogueBranch(DialogueEvent evt, JSONArray commandList, DialogueBranch fallbackBranch, DialogueInterpreter interpreter)
        {
            currentIndex = 0;
            this.evt = evt;
            this.interpreter = interpreter;
            this.commandList = commandList;
            this.fallbackBranch = fallbackBranch;
        }

        public DialogueBranch Process()
        {
            //While interpreter return null continue process next command
            if (currentIndex >= commandList.Count)
            {
                return fallbackBranch;
            }
            var result = interpreter.RunCommand(commandList[currentIndex], evt);
            while (result == null)
            {
                currentIndex++;
                if (currentIndex >= commandList.Count)
                {
                    return fallbackBranch;
                }
                result = interpreter.RunCommand(commandList[currentIndex], evt);
            }
            if (result is JSONArray)
            {
                return new DialogueBranch(evt, (JSONArray)result, this, interpreter);
            }
            currentIndex++;
            return this;
        }
    }
}