using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class RandomWalker : MonoBehaviour
    {
        public void GotoNext()
        {
            if (!hasFirstGoal)
            {
                curGoalIdx = 0;
                float minDiff = (transform.position - wps[0].position).magnitude;
                for(int i = 1; i < wps.Count; i++)
                {
                    float diff = (transform.position - wps[i].position).magnitude;
                    if (diff < minDiff)
                    {
                        minDiff = diff;
                        curGoalIdx = i;
                    }
                }
                hasFirstGoal = true;
            }
            else
            {
                float minDiff = (transform.position - wps[curGoalIdx].position).magnitude;
                for (int i = 1; i < wps.Count; i++)
                {
                    float diff = (transform.position - wps[i].position).magnitude;
                    if (diff < minDiff)
                    {
                        minDiff = diff;
                        curGoalIdx = i;
                    }
                }
            }
        }

        [SerializeField] private Rigidbody rb;
        [SerializeField] private AnimationPlayer animationPlayer;
        [SerializeField] private Transform goalT;
        [SerializeField] private float walkSpeed;
        [SerializeField] private List<Transform> wps;

        private int curGoalIdx;
        private bool hasFirstGoal;
    }
}