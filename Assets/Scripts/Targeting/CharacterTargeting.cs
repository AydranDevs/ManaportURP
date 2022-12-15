using System;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;

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
        public event EventHandler OnTargetLost;
        #endregion

        private PartyMember _member;
        [Tooltip("The range in units that this action searches within for targets.")]
        public float lockOnRange;
        public ITargetable currentlyTargeted { get; private set; }
        private InputProvider _inputProvider;

        #region Target Management
            
        public void SetTarget(ITargetable target)
        {
            if (currentlyTargeted == null)
            {
                OnNewTargetSelected?.Invoke(this, new OnNewTargetSelectedEventArgs { newTarget = target });
            }
            else
            {
                OnTargetLost?.Invoke(this, EventArgs.Empty);
            }
            
            OnTargetChanged?.Invoke(this, new OnTargetChangedEventArgs { newTarget = target, previousTarget = currentlyTargeted });
            currentlyTargeted = target;
            
            
            Debug.Log(currentlyTargeted);
        }

        public void DropCurrentlyTargeted()
        {
            currentlyTargeted = null;
            OnTargetLost.Invoke(this, EventArgs.Empty);
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
        
        #endregion
    
        public void Init(PartyMember member)
        {
            _member = member;
        }

        void Update()
        {
            
        }
    }
}