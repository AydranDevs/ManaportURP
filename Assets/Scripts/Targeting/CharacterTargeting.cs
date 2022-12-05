using System;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.Actions.Targets
{
    public class CharacterTargeting : MonoBehaviour
    {
        public event EventHandler<OnEnemyEnteredTargetRangeEventArgs> OnEnemyEnteredTargetRangeEvent;
        public class OnEnemyEnteredTargetRangeEventArgs : EventArgs
        {
            public Collider2D other;
        }
        public event EventHandler<OnEnemyExitTargetRangeEventArgs> OnEnemyExitTargetRangeEvent;
        public class OnEnemyExitTargetRangeEventArgs : EventArgs
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
        public List<Enemy> enemiesInRange;

        public Enemy currentlyTargetedEnemy;

        // Invoke event and add transform to list when object enters the circle collider
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<Enemy>(out Enemy enemy) || !isTargeting)
            {
                return;
            }
            if (enemiesInRange == null)
            {
                enemiesInRange = new List<Enemy>();
            }
            
            enemiesInRange.Add(enemy);
            if (currentlyTargetedEnemy == null)
            {
                currentlyTargetedEnemy = enemiesInRange[0];
            }

            OnEnemyEnteredTargetRangeEvent?.Invoke(this, new OnEnemyEnteredTargetRangeEventArgs
            {
                other = other
            });
        }

        // Invoke event and add transform to list when object enters the circle collider
        private void OnTriggerExit2D(Collider2D other) {
            if (!other.TryGetComponent<Enemy>(out Enemy enemy) || !isTargeting)
            {
                return;
            }
            if (enemiesInRange == null)
            {
                enemiesInRange = new List<Enemy>();
            }

            enemiesInRange.Remove(enemy);
            if (enemiesInRange.Count == 0)
            {
                currentlyTargetedEnemy = null;
            }

            OnEnemyExitTargetRangeEvent?.Invoke(this, new OnEnemyExitTargetRangeEventArgs
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

            currentlyTargetedEnemy = enemiesInRange[enemiesInRange.IndexOf(currentlyTargetedEnemy) + 1];
        }
        public void PreviousTarget()
        {   
            if (!isTargeting)
            {
                return;
            }

            currentlyTargetedEnemy = enemiesInRange[enemiesInRange.IndexOf(currentlyTargetedEnemy) - 1];
        }
    }
}