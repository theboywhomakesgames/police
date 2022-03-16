using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police
{
    public class Scanner : MonoBehaviour
    {
        [SerializeField] private LayerMask layer;

        private void OnTriggerEnter(Collider other)
        {
            int l = 1 << other.gameObject.layer;
            if((l & layer.value) > 0)
            {
                Heli.instance.Beep(other.transform);
            }
        }
    }
}