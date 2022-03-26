using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class BlockWalker : MonoBehaviour
    {
        public int incrementer = 1;
        public Transform[] wps;

        public void Activate()
        {
            FirstCall();
        }

        [SerializeField] private GoalChaser chaser;

        int curGoalIdx;
        bool hasFirstGoal;

        private void GotoNext()
        {
            curGoalIdx = (curGoalIdx + incrementer) % wps.Length;
            chaser.SetGoal(wps[curGoalIdx].position);
        }

        private void FirstCall()
        {
            chaser.OnArrival += GotoNext;
            curGoalIdx = 0;
            float minDiff = (transform.position - wps[0].position).magnitude;
            for (int i = 1; i < wps.Length; i++)
            {
                float diff = (transform.position - wps[i].position).magnitude;
                if (diff < minDiff)
                {
                    minDiff = diff;
                    curGoalIdx = i;
                }
            }
            hasFirstGoal = true;
            chaser.SetGoal(wps[curGoalIdx].position);
        }
    }
}