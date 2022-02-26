using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DB.Utils
{
    public class Indicator : MonoBehaviour
    {
        public void SetTarget(Transform t)
        {
            target = t;
        }

        [SerializeField] private Transform target, pT;
        [SerializeField] private bool isActive;
        [SerializeField] private Image img;
        [SerializeField] private RectTransform rt;
        [SerializeField] private Sprite offScreen, onScreen;
        [SerializeField] private Color[] colors;
        [SerializeField] private BoolCondition activationCondition;
        
        [SerializeField] private Camera mc;

        private void OnEnable(){
            activationCondition.OnActivation += Activate;
            activationCondition.OnDeactivation += Deactivate;
        }

        private void OnDisable(){
            activationCondition.OnActivation -= Activate;
            activationCondition.OnDeactivation -= Deactivate;
        }

        private void Activate(){
            isActive = true;
            img.enabled = true;
        }

        private void Deactivate(){
            isActive = false;
            img.enabled = false;
        }

        private void Update()
        {
            if (isActive)
            {
                Indicate();
            }
        }

        private void Indicate()
        {
            Vector3 tp = target.position;
            tp = (tp - pT.position).normalized * 2;
            rt.transform.right = tp.normalized;
            tp.y = -tp.z;
            tp.z = 0;
            rt.localPosition = tp;
        }
    }
}
