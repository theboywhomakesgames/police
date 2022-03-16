using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DB.Utils
{
    public class HealthyEntity : MonoBehaviour
    {
        public UnityEvent OnDeath, OnDamage;
        public event Action OnDeathE, OnDamageE;
        public bool isdead = false;

        public void Damge(float damage)
        {
            if (isdead)
                return;

            health.Amount -= damage;
            OnDamage?.Invoke();
            OnDamageE?.Invoke();

            if(health.Amount <= 0)
            {
                OnDeath?.Invoke();
                OnDeathE?.Invoke();
                isdead = true;
            }
        }

        [SerializeField] private Health health;
    }
}