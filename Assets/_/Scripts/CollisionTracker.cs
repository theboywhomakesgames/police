using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DB.Utils
{
    public class CollisionTracker : MonoBehaviour
    {
        public UnityEvent<Collider> UpdateAction;

        [SerializeField] private LayerMask collisionMask;
        [SerializeField] private List<Collider> colliders;

        private void OnTriggerEnter(Collider other)
        {
            int l = 1 << other.gameObject.layer;
            int test = l & collisionMask.value;
            if (test > 0)
            {
                colliders.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            int l = 1 << other.gameObject.layer;
            int test = l & collisionMask.value;
            if (test > 0)
            {
                colliders.Remove(other);
            }
        }

        private void Update()
        {
            for(int i = colliders.Count - 1; i >= 0; i--)
            {
                if(colliders[i] == null || !colliders[i].enabled)
                {
                    colliders.RemoveAt(i);
                }
            }

            if(colliders.Count > 0)
            {
                UpdateAction?.Invoke(colliders[0]);
            }
        }
    }
}