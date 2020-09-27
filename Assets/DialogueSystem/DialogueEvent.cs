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

        private void Awake()
        {
            interpreter = new Interpreter.DialogueInterpreter();
            DialogueManager.
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               
                if (model == null)
                {
                    DialogueManager.Hide();
                }
                else {
                    DialogueManager.ShowWindow(model);
                }
            }
        }
    }
}