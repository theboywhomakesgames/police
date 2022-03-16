using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DB.Utils;

namespace DB.Police
{
    public class Gang : MonoBehaviour
    {
        [SerializeField] private HealthyEntity[] entities;

        private void Start()
        {
            foreach (var entity in entities)
            {
                entity.transform.parent = null;
                entity.OnDeathE += OnDie;
                entity.OnDamageE += OnDamage;
            }
        }

        private void OnDamage()
        {
            foreach(var entity in entities)
            {
                GoalChaser chaser = entity.gameObject.GetComponentInChildren<GoalChaser>();
                if (chaser != null)
                {
                    Vector3 diff = chaser.goalT.position - transform.position;
                    chaser.goalT.position = chaser.goalT.position + diff.normalized * 10;
                }
                entity.OnDeathE -= OnDie;
                entity.OnDamageE -= OnDamage;
            }
            Destroy(gameObject);
        }

        private void OnDie()
        {

        }
    }
}