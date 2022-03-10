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

        public void Place(Collider other)
        {
            OnEnter?.Invoke();
            OnActivation?.Invoke();

            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.DOMove(placeT.position, placingTime).OnComplete(() =>
            {
                other.GetComponent<Rigidbody>().isKinematic = false;
                OnDonePlacing?.Invoke();

                foreach (Delegate d in OnDonePlacing.GetInvocationList())
                {
                    OnDonePlacing -= (Action)d;
                }
            });
            other.transform.DORotateQuaternion(placeT.rotation, placingTime);
        }

        [SerializeField] private Transform placeT;
        [SerializeField] private float placingTime = 1f;
    }
}