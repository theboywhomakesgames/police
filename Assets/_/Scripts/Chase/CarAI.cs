using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DB.Utils;
using PT.Utils;

namespace DB.Police{    
    public class CarAI : MonoBehaviour
    {
        public UnityEvent OnPreBlow, OnBlow;

        public void CollisionDelay(Collision c){
            if(!isActive)
                return;

            isActive = false;

            rb.AddForceAtPosition(
                c.GetContact(0).normal * impactImpulse,
                c.GetContact(0).point,
                ForceMode.Impulse
            );
            
            if(--health > 0){
                TimeManager.Instance.DoWithDelay(delayTime, ()=>{
                    isActive = true;
                });
            }
            else{
                // blow up ...
                BlowUp();
            }
        }

        public void BlowUp(){
           OnPreBlow?.Invoke(); 
            TimeManager.Instance.DoWithDelay(blowUpDelay, FinalBlow);
        }

        [SerializeField] private Transform goalT, vehicleT, targetT;
        [SerializeField] private VehicleOperator vehicleOperator;
        [SerializeField] private AnimationCurve accelerationCurve, targetDistanceAccCurve;
        [SerializeField] private float maxDistance = 10, arriveDistance = 4, maxTargetDistance = 20;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float blowUpVelocity = 20, impactImpulse = 100, delayTime = 0.5f, blowUpDelay = 5;

        [SerializeField] private List<Transform> waypoints;
        [SerializeField] private int waypointIndex;

        [SerializeField] private bool isActive = false;
        [SerializeField] private int health = 3;

        private void FinalBlow(){
           OnBlow?.Invoke(); 
           rb.velocity = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
           ).normalized * blowUpVelocity; 
        }

        private void Awake(){
            goalT.parent = null;
            SetGoalPosition();
        }

        private void SetGoalPosition(){
            goalT.position = waypoints[waypointIndex].position;
        }

        private void GotoNextWaypoint(){
            if(waypointIndex < waypoints.Count - 1){
                waypointIndex ++;
                SetGoalPosition();
            }
            else{
                isActive = false;
            }
        }

        private void Update(){
            if(isActive){
                Vector3 diff = goalT.position - vehicleT.position;
                diff.y = diff.z;
                diff.z = 0;

                if(diff.magnitude <= arriveDistance){
                    GotoNextWaypoint();
                }

                float acc = Mathf.Clamp(diff.magnitude, 0, maxDistance);
                acc = accelerationCurve.Evaluate(acc / maxDistance);
                Vector3 targetDiff = targetT.position - vehicleT.position;
                float distanceAcc = Mathf.Clamp(targetDiff.magnitude, 0, maxTargetDistance);
                distanceAcc = targetDistanceAccCurve.Evaluate(distanceAcc / maxTargetDistance);

                vehicleOperator.ApplyInput(diff, acc * distanceAcc);
            }
        }
    }
}
