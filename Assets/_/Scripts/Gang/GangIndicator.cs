using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DB.Police
{
    public class GangIndicator : MonoBehaviour
    {
        public UnityEvent OnActivation;
        public event Action<GangIndicator> OnDone;

        public void SetTarget(Transform target)
        {
            targetT = target;
            OnActivation?.Invoke();
            time = duration;
            isActive = true;
        }

        public void Disable()
        {
            indicatorImage.color = dc;
        }

        [SerializeField] private Transform targetT;
        [SerializeField] private Camera cam;
        [SerializeField] private Image indicatorImage;

        [SerializeField] private Color dc;
        [SerializeField] private Gradient gradient;
        [SerializeField] private float duration = 2f;

        private bool isActive = false;
        private float time;

        private void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                Color c = gradient.Evaluate(time / duration);
                indicatorImage.color = c;
                if(targetT == null)
                {
                    time = 0;
                }

                Vector2 vp = cam.WorldToViewportPoint(targetT.position);
                indicatorImage.rectTransform.position = cam.ViewportToScreenPoint(vp);
            }
            else if (isActive)
            {
                OnDone?.Invoke(this);
                isActive = false;
                time = 0;
            }
        }
    }
}