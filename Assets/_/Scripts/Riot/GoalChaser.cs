using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DB.Utils;
using System;
using UnityEngine.Events;

namespace DB.Police
{
    public class GoalChaser : MonoBehaviour
    {
        public void Activate()
        {
            isActive = true;
        }

        public void Deactivate()
        {
            isActive = true;
        }

        public Transform goalT;
        public event Action OnArrival;

        [SerializeField] private V3Var inputVector;
        [SerializeField] private float threshold = 0.2f;
        [SerializeField] private BoolCondition isMovingCondition;

        bool isActive = true;

        private void Awake()
        {
            goalT.parent = null;
        }

        private void Update()
        {
            if (isActive)
            {
                Vector3 i = goalT.position - transform.position;
                i.y = i.z;
                i.z = 0;

                if (i.magnitude > threshold)
                {
                    if (!isMovingCondition.value)
                        isMovingCondition.Activate();
                }
                else
                {
                    if (isMovingCondition.value)
                    {
                        OnArrival?.Invoke();
                        isMovingCondition.Deactivate();
                    }
                }

                inputVector.value = i.normalized;
            }
            else if (isMovingCondition.value)
            {
                isMovingCondition.Deactivate();
            }
        }
    }
}