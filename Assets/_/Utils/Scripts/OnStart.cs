using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DB.Utils
{
    public class OnStart : MonoBehaviour
    {
        public UnityEvent OnStartE;

        private void Start()
        {
            OnStartE?.Invoke();
        }
    }
}