using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police{    
    public class CarAI : MonoBehaviour
    {
        [SerializeField] private Transform goalT, vehicleT, targetT;
        [SerializeField] private VehicleOperator vehicleOperator;
        [SerializeField] private AnimationCurve accelerationCurve, targetDistanceAccCurve;
        [SerializeField] private float maxDistance = 10, arriveDistance = 4, maxTargetDistance = 20;

        [SerializeField] private List<Transform> waypoints;
        [SerializeField] private int waypointIndex;

        [SerializeField] private bool isActive = false;

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
                print(distanceAcc);

                vehicleOperator.ApplyInput(diff, acc * distanceAcc);
            }
        }
    }
}
