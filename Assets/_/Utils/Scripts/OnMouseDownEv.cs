using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DB.Utils
{ 
    public class OnMouseDownEv : MonoBehaviour
    {
        public UnityEvent OnActivation;

        private void OnMouseDown()
        {
            OnActivation?.Invoke();
        }
    }
}