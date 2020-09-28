using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kultie.DialogueSystem
{
    public class DialogueChoice : MonoBehaviour
    {
        [SerializeField]
        Text text;
        Action onConfirm;
        public void Setup(string text, Action confirm)
        {
            this.text.text = text;
            onConfirm = confirm;
        }

        public void OnConfirm()
        {
            onConfirm?.Invoke();
        }

    }
}