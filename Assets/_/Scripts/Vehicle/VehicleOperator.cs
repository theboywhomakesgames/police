using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DB.Utils;
using PT.Utils;

namespace DB.Police{
    public class VehicleOperator : MonoBehaviour
    {
        public Transform vehicleT;

        public void ApplyInput(Vector3 input, float accScale)
        {
            gotInput = true;
            supposedForward = -new Vector3(input.x, 0, input.y).normalized;

            vehicleT.forward = Vector3.Lerp(
                vehicleT.forward,
                supposedForward,
                Time.deltaTime * torque
            );

            curVelocity -= vehicleT.forward * acceleration * Time.deltaTime * accScale;
            if(curVelocity.magnitude > speed)
            {
                curVelocity = curVelocity.normalized * speed;
            }

            curVelocity = curVelocity.normalized * curVelocity.magnitude * drift - vehicleT.forward * curVelocity.magnitude * (1 - drift);
            rb.velocity = new Vector3(
                curVelocity.x,
                rb.velocity.y,
                curVelocity.z
            );

            if(accScale <= 0.5f){
                curVelocity *= decay;
            }
        }

        public void ApplyInput(Vector3 input)
        {
            ApplyInput(input, 1);
        }

        [FoldoutGroup("Physics")]
        [SerializeField] private float steerMultiplier, speed, decay, torque, turnThreshold, drift, acceleration;
        [FoldoutGroup("Physics")]
        [SerializeField] private Rigidbody rb;

        private Vector3 supposedForward, curVelocity;
        private bool gotInput = false;

        private void Update(){
            if(gotInput){
                gotInput = false;
            }
            else{
                curVelocity *= decay;
            }
        }
    }
}
