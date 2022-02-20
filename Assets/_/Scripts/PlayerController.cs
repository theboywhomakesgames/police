using DB.Utils;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DB.Police
{
    public class PlayerController : MonoBehaviour
    {
        public UnityEvent<PlayerReference> OnTap;
        public UnityEvent OnCarModeActivation, OnCarModeDeactivation;
        public BoolCondition isMovingCondition;
        public V3Var inputVector;

        public void ActivateCarMode(VehicleController vc)
        {
            vehicle = vc;
            carMode = true;
            OnCarModeActivation?.Invoke();
        }

        public void DeactivateCarMode()
        {
            vehicle.Deactivate();
            carMode = false;
            playerT.position = vehicle.getoutPosT.position;
            OnCarModeDeactivation?.Invoke();
        }

        [SerializeField] private Transform playerT;
        [SerializeField] private TouchStick joystick;
        [SerializeField] private float tapTime = 0.1f;
        [SerializeField] private PlayerReference pr;

        private bool isTouching, tap, carMode;
        private VehicleController vehicle;

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

            StartCoroutine(CheckTap());
        }

        private void OnTouchEnd()
        {
            isTouching = false;
            isMovingCondition.Deactivate();

            if (tap)
            {
                Tap();
            }

            StopCoroutine(CheckTap());
            tap = false;
        }

        private void Tap() 
        {
            if (!carMode)
            {
                OnTap?.Invoke(pr);
            }
            else
            {
                DeactivateCarMode();
            }
        }

        private IEnumerator CheckTap()
        {
            tap = true;
            yield return new WaitForSeconds(tapTime);
            tap = false;
        }

        private void OnInput(Vector3 input){
            inputVector.UpdateValue(-(input / Screen.width));
        }
    }
}
