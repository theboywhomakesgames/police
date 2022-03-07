using PT.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{ 
    public class SquadWalker : MonoBehaviour
    {
        [SerializeField] private GoalChaser goalChaser;

        private void Start()
        {
            TimeManager.Instance.DoWithDelay(
                1f, 
                () =>
                {
                    goalChaser.goalT.position = RoadSystem.instance.GetNeighbour(goalChaser.goalT.position);
                    goalChaser.OnArrival += Arrived;
                }
            );
        }

        private void Arrived()
        {
            goalChaser.goalT.position = RoadSystem.instance.GetNeighbour(goalChaser.goalT.position);
        }
    }
}