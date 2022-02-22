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
            playerController.inputVector.OnUpdateValue += vehicleOperator.ApplyInput;

            isActive = true;
        }

        public void Deactivate(){
            vehicleVCam.Priority = 0;
            isActive = false;

            playerController.isMovingCondition.OnActivation -= OnDragStart;
            playerController.isMovingCondition.OnDeactivation -= OnDragEnd;
            playerController.inputVector.OnUpdateValue -= vehicleOperator.ApplyInput;
        }

        [FoldoutGroup("CarProperties")]
        [SerializeField] private CinemachineVirtualCamera vehicleVCam;
        [FoldoutGroup("CarProperties")]
        [SerializeField] private PlayerReference pr;
        [FoldoutGroup("CarProperties")]
        [SerializeField] private PlayerController playerController;
        
        [SerializeField] private VehicleOperator vehicleOperator;

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
    }
}
