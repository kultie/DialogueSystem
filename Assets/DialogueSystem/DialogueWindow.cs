using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Kultie.DialogueSystem
{
    public class DialogueWindow : MonoBehaviour
    {
        [SerializeField]
        Text text;
        DialogueModel model;
        public void Show(DialogueModel model)
        {
            this.model = model;
            text.text = model.message;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }

    public class DialogueModel
    {
        public string message;
        public JSONArray choices;
    }
}