using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class CombatManager : MonoBehaviour
    {
        public void Activate()
        {
            isActive = true;
        }

        public void Deactivate()
        {
            isActive = false;
        }

        public void StartShooting()
        {
            animationPlayer.PlayAnimation("GotoShootLeft");
        }

        public void ActuallyStartShooting()
        {
            // fill when you have a gun
            isShooting = true;
        }

        public void StopShooting()
        {
            isShooting = false;
            animationPlayer.PlayAnimation("ShootLeftGoBack");
        }

        [SerializeField] private bool isMouseDown, isActive, isShooting;
        [SerializeField] private AnimationPlayer animationPlayer;
        [SerializeField] private Gun gun;

        [SerializeField] Transform target;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMouseDown = true;
                StartShooting();
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMouseDown = false;
                StopShooting();
            }

            if (isShooting)
            {
                gun.ShootTarget(target);
            }
        }
    }
}