using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DB.Utils;

namespace DB.Police
{
    public class LookProp : MonoBehaviour
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCamera cam;
        [SerializeField] private float distanceToCam = 1f;

        private Vector3 initPos;
        private Quaternion initRot;

        private void Awake()
        {
            initPos = transform.position;
            initRot = transform.rotation;
        }

        private void OnMouseDown()
        {
            Activate();
        }

        [Button]
        private void Activate()
        {
            Vector3 pos = cam.transform.position + cam.transform.forward * distanceToCam;
            transform.DOMove(pos, 1f);
            Quaternion rot = Quaternion.LookRotation(cam.transform.up);
            transform.DORotateQuaternion(rot, 1f);

            BackStackButton.instance.RegisterAction(Deactivate);
        }

        [Button]
        private void Deactivate()
        {
            transform.DOMove(initPos, 1f);
            transform.DORotateQuaternion(initRot, 1f);
        }
    }
}