using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DB.Police{
    public class VehicleOperator : MonoBehaviour
    {
        public Transform vehicleT;

        public void ApplyInput(Vector3 input)
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

            curVelocity = curVelocity.normalized * curVelocity.magnitude * drift - vehicleT.forward * curVelocity.magnitude * (1 - drift);
            rb.velocity = new Vector3(
                curVelocity.x,
                rb.velocity.y,
                curVelocity.z
            );
        }

        [FoldoutGroup("Physics")]
        [SerializeField] private float steerMultiplier, speed, decay, torque, turnThreshold, drift, acceleration;
        [FoldoutGroup("Physics")]
        [SerializeField] private Rigidbody rb;

        private Vector3 supposedForward, curVelocity;
    }
}
