using DB.Utils;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private TouchStick joystick;
        [SerializeField] private BoolCondition isMovingCondition;
        [SerializeField] private V3Var inputVector;

        private bool isTouching;

        private void OnEnable()
        {
            joystick.OnBegin += OnTouchBegin;
            joystick.OnEnd += OnTouchEnd;
            joystick.OnInput += OnInput;
        }

        private void OnDisable(){
            joystick.OnBegin -= OnTouchBegin;
            joystick.OnEnd -= OnTouchEnd;
            joystick.OnInput -= OnInput;
        }

        private void OnTouchBegin()
        {
            isTouching = true;
            isMovingCondition.Activate();
        }

        private void OnTouchEnd()
        {
            isTouching = false;
            isMovingCondition.Deactivate();
        }

        private void OnInput(Vector3 input){
            inputVector.value = -(input / Screen.width);
        }
    }
}
