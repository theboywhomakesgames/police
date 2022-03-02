using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace DB.Police
{
    public class SafeBox : MonoBehaviour
    {
        public UnityEvent OnSuccess;

        public void RegisterInput(string input)
        {
            curInput += input;
            screenTxt.text = curInput;

            if(curInput.Length == answer.Length)
            {
                CheckCorrectness();
            }
        }

        [SerializeField] private string curInput, answer;
        [SerializeField] private TextMeshPro screenTxt;

        private void CheckCorrectness()
        {
            if(curInput == answer)
            {
                OnSuccess?.Invoke();
            }
            else
            {
                curInput = "";
                screenTxt.text = curInput;
            }
        }
    }
}