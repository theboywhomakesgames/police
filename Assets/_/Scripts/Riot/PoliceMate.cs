using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DB.Utils;
using RootMotion.FinalIK;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.Events;

namespace DB.Police
{
    public class PoliceMate : MonoBehaviour
    {
        public UnityEvent OnActivation;

        public GoalChaser Activate(Squad squad)
        {
            Activate();
            shield.mySquad = squad;
            return goalChaser;
        }

        [Button]
        public void Activate()
        {
            if (isActive)
                return;

            isActive = true;
            shieldGO.SetActive(true);

            Vector3 scale = shield.transform.localScale;
            shieldGO.transform.localScale = Vector3.zero;
            shieldGO.transform.DOScale(scale, 0.5f);

            ik.solver.SetIKPositionWeight(1f);
            OnActivation?.Invoke();
        }

        [Button]
        public void Deactivate()
        {
            if (!isActive)
                return;

            isActive = false;
            shieldGO.SetActive(false);
            ik.solver.SetIKPositionWeight(0f);
        }

        [SerializeField] private bool isActive;
        [SerializeField] private FullBodyBipedIK ik;
        [SerializeField] private Shield shield;
        [SerializeField] private GameObject shieldGO;
        [SerializeField] private GoalChaser goalChaser;

        private void Awake()
        {
            if (isActive)
            {
                isActive = false;
                Activate();
            }
            else
            {
                isActive = true;
                Deactivate();
            }
        }
    }
}