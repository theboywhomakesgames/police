using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DB.Police{
    public class Interactor : MonoBehaviour
    {
        public void Interact(PlayerReference pr){
            Collider[] c = Physics.OverlapSphere(
                transform.position,
                radius,
                layer
            );

            if(c.Length > 0){
                Collider cc = c[0];
                Interactable interactive = cc.gameObject.GetComponent<Interactable>();
                if(interactive != null){
                    interactive.Interact(pr);
                }
            }
        }

        [SerializeField] private float radius = 3;
        [SerializeField] private LayerMask layer;
    }
}
