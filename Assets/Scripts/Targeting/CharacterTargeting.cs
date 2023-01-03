using System;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.PartySystem.Cam;

namespace Manapotion.Actions.Targets
{
    [Serializable]
    public class CharacterTargeting
    {
        #region Events
        public event EventHandler<OnNewTargetSelectedEventArgs> OnNewTargetSelected;
        public class OnNewTargetSelectedEventArgs : EventArgs
        {
            public ITargetable newTarget;
        }
        public event EventHandler<OnTargetChangedEventArgs> OnTargetChanged;
        public class OnTargetChangedEventArgs : EventArgs
        {
            public ITargetable newTarget;
            public ITargetable previousTarget;
        }
        public event Action OnTargetLost;
        #endregion

        private PartyMember _member;
        [Tooltip("The range in units that this action searches within for targets.")]
        public float lockOnRange;
        public ITargetable currentlyTargeted { get; private set; }
        [SerializeField]
        private PartyCameraManagerScriptableObject _partyCameraManager;

        #region Target Management
            
        public void SetTarget(ITargetable target)
        {
            if (currentlyTargeted == null)
            {
                OnNewTargetSelected?.Invoke(this, new OnNewTargetSelectedEventArgs { newTarget = target });
            }
            else
            {
                OnTargetLost?.Invoke();
            }
            
            OnTargetChanged?.Invoke(this, new OnTargetChangedEventArgs { newTarget = target, previousTarget = currentlyTargeted });
            _member.StartCoroutine(_partyCameraManager.RemoveCameraTarget(currentlyTargeted?.GetTransform()));
            currentlyTargeted = target;
            _member.StartCoroutine(_partyCameraManager.AddCameraTarget(currentlyTargeted.GetTransform()));
        }

        public void DropCurrentlyTargeted()
        {
            _member.StartCoroutine(_partyCameraManager.RemoveCameraTarget(currentlyTargeted?.GetTransform()));
            currentlyTargeted = null;
            OnTargetLost?.Invoke();
        }

        public Vector2 GetCurrentTargetPosition()
        {
            if (currentlyTargeted == null)
            {
                return Vector2.zero;
            }

            currentlyTargeted.GetPosition(out Vector2 position);
            return position;
        }
        
        /// <summary>
        /// Get the facing state that would make the character face the target.
        /// </summary>
        /// <returns></returns>
        public Input.FacingState GetFacingStateToTarget()
        {
            if (currentlyTargeted == null)
            {
                return Input.FacingState.South; 
            }

            Vector2 targetDir = ((Vector3)_member.characterTargeting.GetCurrentTargetPosition() - _member.transform.position).normalized;

            if (targetDir.x >= .7f)
            {
                return Input.FacingState.East;
            }
            else if (targetDir.x <= -.7f)
            {
                return Input.FacingState.West;
            }
            else if (targetDir.y >= .7f)
            {
                return Input.FacingState.North;
            }
            else if (targetDir.y <= -.7f)
            {
                return Input.FacingState.South;
            }

            return Input.FacingState.South; 
        }
        
        #endregion
    
        public void Init(PartyMember member)
        {
            _member = member;
        }

        void Update()
        {
            if (currentlyTargeted == null)
            {
                return;
            }

            _member.characterController.SetFacingState(GetFacingStateToTarget());
        }
    }
}