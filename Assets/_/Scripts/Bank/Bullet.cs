using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class Bullet : MonoBehaviour
    {
        public void GetShot(Vector3 direction)
        {
            rb.velocity = direction.normalized * speed;
            transform.forward = direction;
        }

        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speed, destroyAfter;
        
        private void Start()
        {
            Destroy(gameObject, destroyAfter);
        }

        private void OnCollisionEnter(Collision collision)
        {
            BlowUp();
        }

        private void BlowUp()
        {
            Destroy(gameObject);
        }
    }
}