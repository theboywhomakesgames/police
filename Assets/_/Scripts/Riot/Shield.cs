using DB.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class Shield : MonoBehaviour
    {
        public Squad mySquad;

        [SerializeField] private float throwSpeed = 60f;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == 11)
            {
                collision.gameObject.GetComponentInChildren<RagdollManager>().Activate(
                    -collision.GetContact(0).normal,
                    throwSpeed
                );
            }
            else if(collision.gameObject.layer == 15)
            {
                PoliceMate pm = collision.gameObject.GetComponent<PoliceMate>();
                pm.gameObject.layer = 13;
                if(pm != null)
                {
                    GoalChaser gc = pm.Activate(mySquad);
                    mySquad.AddMember(gc);
                }
            }
        }
    }
}