using DG.Tweening;
using PT.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class DummyCar : MonoBehaviour
    {
        [SerializeField] private LayerMask impactorsLayer;
        [SerializeField] private float lowerWait, upperWait;
        [SerializeField] private DOTweenPath pathFollower;
        [SerializeField] private Rigidbody rb;

        [SerializeField] private Transform pathFollowerT;
        [SerializeField] private float positionLerp, rotationLerp;

        [SerializeField] private bool active = true;

        private void OnCollisionEnter(Collision collision)
        {
            int l = 1 << collision.gameObject.layer;
            if((l & impactorsLayer.value) > 0)
            {
                // collision
                rb.isKinematic = false;
                active = false;
                pathFollower.DOPause();
                TimeManager.Instance.DoWithDelay(
                    Random.Range(lowerWait, upperWait),
                    () =>
                    {
                        rb.isKinematic = true;
                        Reactivate();
                        active = true;
                    }
                );
            }
        }

        private void Reactivate()
        {
            pathFollower.DOPlay();
        }

        private void Awake()
        {
            pathFollowerT.parent = null;
            pathFollower.DOPlay();
        }

        private void Update()
        {
            if (active)
            {
                transform.position = Vector3.Lerp(transform.position, pathFollowerT.position, Time.deltaTime * positionLerp);
                transform.rotation = Quaternion.Lerp(transform.rotation, pathFollowerT.rotation, Time.deltaTime * rotationLerp);
            }
        }
    }
}