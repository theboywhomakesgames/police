using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DB.Utils
{
    public class BackStackButton : MonoBehaviour
    {
        public static BackStackButton instance;

        public void Click()
        {
            Action a = actions.Pop();
            a();
            UpdateState();
        }

        public void RegisterAction(Action onBackClick)
        {
            actions.Push(onBackClick);
            UpdateState();
        }

        [SerializeField] private Stack<Action> actions = new Stack<Action>();
        [SerializeField] private Button btn;

        private void Awake()
        {
            actions = new Stack<Action>();
            UpdateState();
            instance = this;
        }

        private void UpdateState()
        {
            btn.interactable = actions.Count > 0;
        }
    }
}