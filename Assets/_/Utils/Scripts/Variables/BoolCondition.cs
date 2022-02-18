using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Utils
{
    public class BoolCondition : MonoBehaviour
    {
        public event Action OnActivation, OnDeactivation;
        // TODO: add some functions and don't expose the value
        public bool value;

        public void Activate(){
            if(!value){
                value = true;
                OnActivation?.Invoke();
            }
        }

        public void Deactivate(){
            if(value){
                value = false;
                OnDeactivation?.Invoke();
            }
        }
        
    }
}
