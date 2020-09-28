using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.DialogueSystem
{
    public class DialogueChoiceWindow : MonoBehaviour
    {
        [SerializeField]
        DialogueChoice choiceTemplate;
        [SerializeField]
        Transform container;
        public void Show(JSONArray choices, DialogueEvent evt)
        {
            for (int i = 0; i < choices.Count; i++)
            {
                var template = Instantiate(choiceTemplate, container);
                int index = i;
                template.Setup(choices[i]["label"].Value, () =>
                {
                    Hide();
                    evt.ChangeBranch(choices[index]["list"].AsArray);
                });
                template.gameObject.SetActive(true);
            }
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}