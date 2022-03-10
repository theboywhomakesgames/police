using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace DB.Police
{
    public class BankDoor : MonoBehaviour
    {
        [Button]
        public void Open()
        {
            leftDoorT.DOLocalMove(-moveDirection, duration).SetRelative(true);
            rightDoorT.DOLocalMove(moveDirection, duration).SetRelative(true);
        }

        [Button]
        public void Close()
        {
            leftDoorT.DOLocalMove(moveDirection, duration).SetRelative(true);
            rightDoorT.DOLocalMove(-moveDirection, duration).SetRelative(true);
        }

        [SerializeField] private Transform leftDoorT, rightDoorT;
        [SerializeField] private float duration = 1f;
        [SerializeField] private Vector3 moveDirection;
    }
}