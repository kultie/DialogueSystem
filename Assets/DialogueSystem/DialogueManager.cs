using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        static DialogueManager Instance;
        [SerializeField]
        DialogueWindow dialogueWindow;

        DialogueEvent evt;
        Dictionary<int, int> depthMap = new Dictionary<int, int>();
        private int currentIndex;
        public void LoadCommand(JSONArray commands, DialogueEvent evt)
        {
            this.commands = commands;
            this.evt = evt;
        }

        public void StartDialogue()
        {
            currentIndex = 0;
            depthMap[currentIndex] = 0;
        }

        JSONNode CurrentDialogueNode()
        {
            return commands[currentIndex];
        }

        public object ProgressNext()
        {
            if (currentIndex >= commands.Count)
            {
                return null;
            }
            JSONNode current = CurrentDialogueNode();
            currentIndex++;
            return RunCommand(current);
        }


        private void Awake()
        {
            Instance = this;
        }

        public static void ShowWindow(DialogueModel model)
        {
            Instance.dialogueWindow.Show(model);
        }

        internal static void Hide()
        {
            Instance.dialogueWindow.Hide();
        }
    }
}