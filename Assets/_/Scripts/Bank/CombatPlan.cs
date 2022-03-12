using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine.Events;

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
        public UnityEvent OnPhaseStart;
        public AnimationState animationState;
        public CharacterPlacor destinationPlacor;
        public Transform[] path;
        public CombatTarget target;

        public int pathidx = 0;
    }

    public class CombatPlan : MonoBehaviour
    {
        [Button]
        public void GoToNext()
        {
            animationPlayer.PlayAnimation("Idle");

            states[index].OnPhaseStart?.Invoke();
            states[index].destinationPlacor.OnActivation += OnStartPlacing;
            states[index].destinationPlacor.OnDonePlacing += OnPlaced;

            isWalkingPath = true;
            chaser.OnArrival += GotoNextPathLoc;
            GotoNextPathLoc();
        }

        public void GotoNextPathLoc()
        {
            if (!isWalkingPath || states[index].pathidx >= states[index].path.Length)
                return;

            chaser.Activate();
            chaser.goalT.position = states[index].path[states[index].pathidx].position;
            states[index].pathidx++;
        }

        public void EnterCombatMode()
        {
            states[index].target.Activate();
            manager.SetTarget(states[index].target.selfTargetT);
            states[index].target.OnDeathE += ExitCombatMode;
        }

        public void ExitCombatMode()
        {
            chaser.Activate();
            states[index].destinationPlacor.Release();
            states[index].target.OnDeathE -= ExitCombatMode;

            index++;
            if (index >= states.Length)
            {
                // end combat

            }
            else
            {
                animationPlayer.PlayAnimation("Idle");
                GoToNext();
            }
        }

        [SerializeField] private AnimationPlayer animationPlayer;
        [SerializeField] private CombatState[] states;
        [SerializeField] private int index;
        [SerializeField] private GoalChaser chaser;
        [SerializeField] private CombatManager manager;
        [SerializeField] private bool isWalkingPath = false;

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
            EnterCombatMode();
        }

        private void OnStartPlacing()
        {
            isWalkingPath = false;
            chaser.OnArrival -= GotoNextPathLoc;
        }
    }
}