using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DB.Utils;
using PT.Utils;
using Sirenix.OdinInspector;

namespace DB.Police
{
    public class StickAnimationSetter : AnimationPlayer
    {
        public void OnStartMovement(){
            isMoving = true;
        }

        public void OnStopMovement(){
            isMoving = false;
        }

        [SerializeField] private BoolCondition isMovingCondition;

        [FoldoutGroup("Physical")]
        [SerializeField] private Rigidbody rb;
        [FoldoutGroup("Physical")]
        [SerializeField] private float maxWalkSpeed, minWalkSpeed, maxRunSpeed, minRunSpeed;

        private bool isMoving = false,
                curMovementStatus = false,
                isRunning = false;

        private void Update(){
            SetAnimation();
        }

        private void SetAnimation(){
            if(isMoving){
                if(!curMovementStatus){
                    // start of movement animation
                    curMovementStatus = true;
                    animator.SetBool("Moving", true);
                }

                // set speed and check walking
                Vector3 noYVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                float velMag = noYVel.magnitude;
                float animationSpeed = 1;

                if(velMag < maxWalkSpeed){
                    if(isRunning){
                        // stop of run
                        isRunning = false;
                        animator.SetBool("Running", false);
                    }
                    animationSpeed = velMag / maxWalkSpeed + minWalkSpeed;
                }else{
                   if(!isRunning){
                       // start of run
                       isRunning = true;
                       animator.SetBool("Running", true);
                   } 
                   animationSpeed = velMag / maxRunSpeed + minRunSpeed;
                }
                animator.SetFloat("Speed", animationSpeed);
            }
            else if(!isMoving){
                if(curMovementStatus){
                    // stop of movement animation
                    curMovementStatus = false;
                    animator.SetBool("Moving", false);
                }
            }
        }

        private void OnEnable(){
            isMovingCondition.OnActivation += OnStartMovement;
            isMovingCondition.OnDeactivation += OnStopMovement;
        }

        private void OnDisable(){
            isMovingCondition.OnActivation -= OnStartMovement;
            isMovingCondition.OnDeactivation -= OnStopMovement;
        }
    }
}
