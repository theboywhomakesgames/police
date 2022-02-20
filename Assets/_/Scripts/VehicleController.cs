using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using e23.VehicleController;
using Cinemachine;
using DB.Utils;
using Sirenix.OdinInspector;

namespace DB.Police{
    public class VehicleController : MonoBehaviour
    {
        public Transform getoutPosT;

        public void Activate(PlayerReference pr){
           vehicleVCam.Priority = 20;
            this.pr = pr;
            playerController = pr.playerController;
            playerController.ActivateCarMode(this);

            playerController.isMovingCondition.OnActivation += OnDragStart;
            playerController.isMovingCondition.OnDeactivation += OnDragEnd;
            playerController.inputVector.OnUpdateValue += ApplyInput;

            isActive = true;
        }

        public void Deactivate(){
            vehicleVCam.Priority = 0;
            isActive = false;

            playerController.isMovingCondition.OnActivation -= OnDragStart;
            playerController.isMovingCondition.OnDeactivation -= OnDragEnd;
            playerController.inputVector.OnUpdateValue -= ApplyInput;
        }

        [FoldoutGroup("CarProperties")]
        [SerializeField] private CinemachineVirtualCamera vehicleVCam;
        [FoldoutGroup("CarProperties")]
        [SerializeField] private PlayerReference pr;
        [FoldoutGroup("CarProperties")]
        [SerializeField] private PlayerController playerController;
        [FoldoutGroup("CarProperties")]
        [SerializeField] private Transform vehicleT;

        [FoldoutGroup("Physics")]
        [SerializeField] private float steerMultiplier, speed, decay, torque, turnThreshold, drift, acceleration;
        [FoldoutGroup("Physics")]
        [SerializeField] private Rigidbody rb;

        private Vector3 supposedForward, curVelocity;

        private bool isActive = false;
        private bool isDragging = false;

        private void OnDragStart()
        {
            isDragging = true; 
        }

        private void OnDragEnd()
        {
            isDragging = false;
        }

        private void ApplyInput(Vector3 input)
        {
            if (isDragging)
            {
                supposedForward = -new Vector3(input.x, 0, input.y).normalized;

                vehicleT.forward = Vector3.Lerp(
                    vehicleT.forward,
                    supposedForward,
                    Time.deltaTime * torque
                );

                curVelocity -= vehicleT.forward * acceleration * Time.deltaTime;
                if(curVelocity.magnitude > speed)
                {
                    curVelocity = curVelocity.normalized * speed;
                }
            }
            else
            {
                curVelocity *= decay;
            }

            curVelocity = curVelocity.normalized * curVelocity.magnitude * drift -
                vehicleT.forward * curVelocity.magnitude * (1 - drift);
            rb.velocity = new Vector3(
                curVelocity.x,
                rb.velocity.y,
                curVelocity.z
            );
        }
    }
}
