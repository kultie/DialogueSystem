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

        private Dictionary<string, long> eventVariables = new Dictionary<string, long>();
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

        public void SetEventVariables(string key, long value)
        {
            eventVariables[key] = value;
            foreach (KeyValuePair<string, long> var in eventVariables)
            {
                Debug.Log(var.Key + ": " + var.Value);
            }
        }

        public long GetEventVariables(string key)
        {
            if (!eventVariables.TryGetValue(key, out long value))
            {
                eventVariables[key] = 0;
            }
            return eventVariables[key];
        }
    }
}