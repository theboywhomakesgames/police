using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DB.Utils;

namespace DB.Police
{
    public class SearchProp : MonoBehaviour
    {
        public UnityEvent OnActivation, OnDeactivation;

        public void GoBack()
        {
            cam.Priority = 0;
            OnDeactivation?.Invoke();
        }

        [SerializeField] private Cinemachine.CinemachineVirtualCamera cam;
        [SerializeField] private int camPriority = 30;

        private void OnMouseDown()
        {
            cam.Priority = 30;
            OnActivation?.Invoke();
            BackStackButton.instance.RegisterAction(GoBack);
        }
    }
}