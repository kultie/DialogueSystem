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

        public void Process()
        {
            //While interpreter return null continue process next command
            if (currentIndex >= commandList.Count)
            {
                if (fallbackBranch != null)
                {
                    evt.ChangeBranch(fallbackBranch);
                }
                return;
            }
            var result = interpreter.RunCommand(commandList[currentIndex], evt);

            while (result == null)
            {
                currentIndex++;
                if (currentIndex >= commandList.Count)
                {
                    if (fallbackBranch != null)
                    {
                        evt.ChangeBranch(fallbackBranch);
                    }
                    return;
                }
                result = interpreter.RunCommand(commandList[currentIndex], evt);
            }

            if (result is DialogueChoiceModel)
            {
                DialogueManager.ShowChoice((DialogueChoiceModel)result, evt);
            }
            else if (result is DialogueModel)
            {
                DialogueManager.ShowWindow((DialogueModel)result, evt);
            }

            else if (result is JSONArray)
            {
                evt.ChangeBranch((JSONArray)result);
            }
            currentIndex++;
        }

        public bool isFinished()
        {
            return currentIndex >= commandList.Count;
        }
    }
}