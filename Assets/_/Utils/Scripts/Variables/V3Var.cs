using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Utils{
    public class V3Var : MonoBehaviour
    {
        public Vector3 value;
        public event Action<Vector3> OnUpdateValue; 

        public void UpdateValue(Vector3 newVal)
        {
            value = newVal;
            OnUpdateValue?.Invoke(value);
        }
    }
}
