using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;

namespace DB.Police
{
    [Serializable]
    public enum AnimationState
    {
        AIM_NORMAL,
        AIM_RUNNING,
        AIM_CROUCH,
        COVER_LEFT,
        COVER_RIGHT,
        IDLE
    }

    [Serializable]
    public class CombatState
    {
        public AnimationState animationState;
        public CharacterPlacor destinationPlacor;
        public CombatTarget[] targets;
    }

    public class CombatPlan : MonoBehaviour
    {
        [Button]
        public void GoToNext()
        {
            animationPlayer.PlayAnimation("Idle");
            chaser.Activate();
            chaser.goalT.position = states[index].destinationPlacor.transform.position;
            states[index].destinationPlacor.OnActivation += OnStartPlacing;
            states[index].destinationPlacor.OnDonePlacing += OnPlaced;
        }

        public void EnterCombatMode()
        {

        }

        [SerializeField] private AnimationPlayer animationPlayer;
        [SerializeField] private CombatState[] states;
        [SerializeField] private int index;
        [SerializeField] private GoalChaser chaser;

        private void Start()
        {
            chaser.Deactivate();
        }

        private string StateToString(AnimationState state)
        {
            switch (state)
            {
                case AnimationState.AIM_NORMAL:
                    return "Aiming";
                case AnimationState.AIM_RUNNING:
                    return "AimingRun";
                case AnimationState.COVER_LEFT:
                    return "CoverShootLeftIdle";
                case AnimationState.COVER_RIGHT:
                    return "CoverShootRight";
                case AnimationState.AIM_CROUCH:
                    return "AimingCrouch";
                case AnimationState.IDLE:
                    return "Idle";
            }
            return "";
        }

        private void OnPlaced()
        {
            print("placed");
            animationPlayer.PlayAnimation(
                StateToString(states[index].animationState)
            );

            chaser.Deactivate();

            index++;
            if (index >= states.Length)
            {

            }
        }

        private void OnStartPlacing()
        {
            print("placing");
        }
    }
}