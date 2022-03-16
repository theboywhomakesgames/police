using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DB.Police
{
    public class Heli : MonoBehaviour
    {
        public static Heli instance;

        public UnityEvent OnScan;

        public void Beep(Transform target)
        {
            if(indicators.Count > 0)
            {
                GangIndicator ind = indicators[0];
                indicators.RemoveAt(0);
                ind.SetTarget(target);
                ind.OnDone += OnIndicatorDisabled;
            }
            else
            {
                GameObject go = Instantiate(indicatorPrefab);
                go.transform.parent = canvas.transform;
                GangIndicator ind = go.GetComponent<GangIndicator>();
                ind.SetTarget(target);
                ind.OnDone += OnIndicatorDisabled;
            }
        }

        public void ShootTarget(Collider targetC)
        {
            gun.ShootTarget(targetC.transform);
        }

        public void OnIndicatorDisabled(GangIndicator indicator)
        {
            indicator.OnDone -= OnIndicatorDisabled;
            indicator.Disable();
            indicators.Add(indicator);
        }

        [SerializeField] private Gun gun;
        [SerializeField] private Transform scannerT;
        [SerializeField] private GameObject scannerPrefab;
        [SerializeField] private float betweenScans = 6;

        [SerializeField] private int poolSize = 10;
        [SerializeField] private GameObject indicatorPrefab;
        [SerializeField] private Canvas canvas;
        [SerializeField] private List<GangIndicator> indicators;

        private void Awake()
        {
            for(int i = 0; i < poolSize; i++)
            {
                GameObject go = Instantiate(indicatorPrefab);
                go.transform.parent = canvas.transform;
                indicators.Add(go.GetComponent<GangIndicator>());
            }
        }

        private void Start()
        {
            instance = this;
            StartCoroutine(DropScanner());
        }

        private IEnumerator DropScanner()
        {
            while (true)
            {
                GameObject go = Instantiate(scannerPrefab);
                //go.transform.parent = transform;
                go.transform.position = scannerT.position - Vector3.up;
                OnScan?.Invoke();
                yield return new WaitForSeconds(betweenScans);
            }
        }
    }
}