using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
namespace Kultie.DialogueSystem
{
    public class DialogueEvent : MonoBehaviour
    {
        [SerializeField]
        TextAsset dialogueData;
        private Interpreter.DialogueInterpreter interpreter;
        DialogueBranch rootBranch;
        DialogueBranch currentBranch;

        private void Awake()
        {
            interpreter = new Interpreter.DialogueInterpreter();
            rootBranch = new DialogueBranch(this, JSON.Parse(dialogueData.text).AsArray, null, interpreter);
            currentBranch = rootBranch;
        }

        public void ChangeBranch(JSONArray data)
        {
            currentBranch = new DialogueBranch(this, data, currentBranch, interpreter);
            currentBranch.Process();
        }

        public void ChangeBranch(DialogueBranch branch)
        {
            currentBranch = branch;
            currentBranch.Process();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (rootBranch.isFinished())
                {
                    Debug.Log("Finish event dialogue");
                }
                currentBranch.Process();
            }
        }
    }
}