using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

namespace DB.Police
{
    public class CharacterPlacor : MonoBehaviour
    {
        public UnityEvent OnEnter;
        public event Action OnActivation, OnDonePlacing;

        public void Release()
        {
            rb.isKinematic = false;
        }

        public void Place(Collider other)
        {
            if (isUsed)
                return;

            OnEnter?.Invoke();
            OnActivation?.Invoke();

            rb = other.attachedRigidbody;
            if (rb == null)
                return;

            rb.isKinematic = true;
            other.transform.DOMove(placeT.position, placingTime).OnComplete(() =>
            {
                OnDonePlacing?.Invoke();

                foreach (Delegate d in OnDonePlacing.GetInvocationList())
                {
                    OnDonePlacing -= (Action)d;
                }
            });
            other.transform.DORotateQuaternion(placeT.rotation, placingTime);
            Use();
        }

        public void Use()
        {
            isUsed = true;
        }

        private bool isUsed = false;

        [SerializeField] private Transform placeT;
        [SerializeField] private float placingTime = 1f;

        Rigidbody rb;
    }
}