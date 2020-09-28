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
        //[SerializeField]
        //Choice Window
        //[SerializeField]
        //Input window
        DialogueModel model;
        public void Show(DialogueModel model, DialogueEvent evt)
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
    }

    public class DialogueChoiceModel : DialogueModel
    {
        public JSONArray choices;
    }
    public enum InputType { Text, Number }
    public class DialogueInputModel : DialogueModel
    {
        public InputType inputType;
    }
}