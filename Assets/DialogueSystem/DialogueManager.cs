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
        [SerializeField]
        DialogueChoiceWindow choicesWindow;

        private void Awake()
        {
            Instance = this;
        }

        public static void ShowWindow(DialogueModel model, DialogueEvent evt)
        {
            Instance.dialogueWindow.Show(model, evt);
        }

        public static void ShowChoice(DialogueChoiceModel model, DialogueEvent evt)
        {
            Instance.dialogueWindow.Show(model, evt);
            Instance.choicesWindow.Show(model.choices, evt);
        }

        internal static void Hide()
        {
            Instance.choicesWindow.Hide();
            Instance.dialogueWindow.Hide();
        }
    }
}