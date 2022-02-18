using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DB.Utils;
using PT.Utils;

namespace DB.Police{
    public class RBMover : MonoBehaviour
    {
       [SerializeField] private BoolCondition isMovingCondition;
       [SerializeField] private V3Var inputVector;
       [SerializeField] private FVar speedMultiplier;
       [SerializeField] private Rigidbody rb;
       [SerializeField] private Transform bodyT;
       [SerializeField] private float rotationSpeed = 1f;

       private void Update(){
           if(isMovingCondition.value){
               Vector3 input = inputVector.value;
               float speed = speedMultiplier.value;
               input *= speed;
               rb.velocity = new Vector3(input.x, rb.velocity.y, input.y);

               bodyT.forward = Vector3.Lerp(
                       bodyT.forward,
                       new Vector3(input.x, 0, input.y),
                       rotationSpeed * Time.deltaTime
               );
           }
       }
    }
}
