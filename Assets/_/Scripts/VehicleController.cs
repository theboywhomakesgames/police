using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using e23.VehicleController;
using Cinemachine;

namespace DB.Police{
    public class VehicleController : MonoBehaviour
    {
        public Transform getoutPosT;

        public void Activate(PlayerReference pr){
           vehicleVCam.Priority = 20;
            this.pr = pr;
            pr.playerController.ActivateCarMode(this);
        }

        public void Deactivate(){
           vehicleVCam.Priority = 0;
        }

        [SerializeField] private CinemachineVirtualCamera vehicleVCam;
        [SerializeField] private PlayerReference pr;

        private bool isActive = false;
    }
}
