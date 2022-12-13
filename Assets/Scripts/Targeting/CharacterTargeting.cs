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
        public event EventHandler<OnTargetEnteredRangeEventArgs> OnTargetEnteredRangeEvent;
        public class OnTargetEnteredRangeEventArgs : EventArgs
        {
            public Collider2D other;
        }
        public event EventHandler<OnTargetExitRangeEventArgs> OnEnemyExitRangeEvent;
        public class OnTargetExitRangeEventArgs : EventArgs
        {
            public Collider2D other;
        }
        
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
            
        // Invoke event and add transform to list when object enters the circle collider
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<ITargetable>(out ITargetable targetable) || !isTargeting)
            {
                return;
            }
            if (targetsInRange == null)
            {
                targetsInRange = new List<ITargetable>();
            }
            
            targetsInRange.Add(targetable);
            if (currentlyTargeted == null)
            {
                currentlyTargeted = targetsInRange[0];
            }

            OnTargetEnteredRangeEvent?.Invoke(this, new OnTargetEnteredRangeEventArgs
            {
                other = other
            });
        }

        // Invoke event and add transform to list when object enters the circle collider
        private void OnTriggerExit2D(Collider2D other) {
            if (!other.TryGetComponent<ITargetable>(out ITargetable ITargetable) || !isTargeting)
            {
                return;
            }
            if (targetsInRange == null)
            {
                targetsInRange = new List<ITargetable>();
            }

            targetsInRange.Remove(ITargetable);
            if (targetsInRange.Count == 0)
            {
                currentlyTargeted = null;
            }

            OnEnemyExitRangeEvent?.Invoke(this, new OnTargetExitRangeEventArgs
            {
                other = other
            });
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