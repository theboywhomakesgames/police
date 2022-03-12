using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DB.Police {
    public class CombatTarget : MonoBehaviour
    {
        public Transform selfTargetT;
        public Transform targetT;

        public UnityEvent OnDeath, OnDamage;
        public event Action OnDeathE;

        [Button]
        public void Activate()
        {
            animationPlayer.PlayAnimation("AimingCrouch");
        }

        public void ActivateShooting()
        {
            canShoot = true;
        }

        public void Damage()
        {
            health--;
            OnDamage?.Invoke();
            if(health == 0)
            {
                Die();
            }
        }

        public void Die()
        {
            canShoot = false;
            animationPlayer.PlayAnimation("Dying");
            OnDeath?.Invoke();
            OnDeathE?.Invoke();
        }

        [SerializeField] private int health = 2;
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