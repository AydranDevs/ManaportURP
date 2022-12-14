using System;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;

namespace Manapotion.Actions.Targets
{
    public class CharacterTargeting : MonoBehaviour
    {
        private PartyMember _member;
        
        #region Target Member Variables
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
        
        public CircleCollider2D targetTrigger;
        public float targetRange
        {
            get
            {
                return targetTrigger.radius;
            }
            private set
            {

            }
        }

        public bool isTargeting = false;
        public List<ITargetable> targetsInRange;

        public ITargetable currentlyTargeted;
        #endregion

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
        }

        public void DropCurrentlyTargeted()
        {
            currentlyTargeted = null;
            OnTargetLost.Invoke(this, EventArgs.Empty);
        }

        public void NextTarget()
        {
            if (!isTargeting)
            {
                return;
            }

            currentlyTargeted = targetsInRange[targetsInRange.IndexOf(currentlyTargeted) + 1];
        }
        public void PreviousTarget()
        {   
            if (!isTargeting)
            {
                return;
            }

            currentlyTargeted = targetsInRange[targetsInRange.IndexOf(currentlyTargeted) - 1];
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