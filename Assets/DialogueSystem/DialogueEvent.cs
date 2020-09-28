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
        DialogueBranch currentBranch;

        private void Awake()
        {
            interpreter = new Interpreter.DialogueInterpreter();
            currentBranch = new DialogueBranch(this, JSON.Parse(dialogueData.text).AsArray, null, interpreter);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentBranch = currentBranch.Process();
                if (currentBranch == null)
                {
                    Debug.Log("Dialogue branch has been completed");
                }
            }
        }
    }
}