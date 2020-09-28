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