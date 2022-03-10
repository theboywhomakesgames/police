using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police {
    public class CombatTarget : MonoBehaviour
    {
        public Transform selfTargetT;
        public Transform targetT;

        [Button]
        public void Activate()
        {
            animationPlayer.PlayAnimation("AimingCrouch");
        }

        public void ActivateShooting()
        {
            canShoot = true;
        }

        public void Die()
        {

        }

        [SerializeField] private AnimationPlayer animationPlayer;
        [SerializeField] private Gun gun;
        [SerializeField] private bool isActive = false, canShoot = false;

        private void Update()
        {
            if (canShoot)
            {
                gun.ShootTarget(targetT);
            }
        }
    }
}