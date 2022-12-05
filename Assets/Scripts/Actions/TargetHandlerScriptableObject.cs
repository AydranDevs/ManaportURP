using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;

namespace Manapotion.Actions.Targets
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/Targets/New TargetHandlerScriptableObject")]
    public class TargetHandlerScriptableObject : ScriptableObject
    {
        public List<Enemy> enemiesInRange;
        public bool isTargeting;

        public Enemy currentlyTargetedEnemy;
        
        public virtual IEnumerator BeginTargeting(PartyMember member)
        {
            isTargeting = true;

            member.characterTargeting.OnEnemyEnteredTargetRangeEvent += OnObjectEnteredTargetRangeEvent_CheckIfEnemy;
            member.characterTargeting.OnEnemyExitTargetRangeEvent += OnObjectExitTargetRangeEvent_CheckIfEnemy;

            enemiesInRange = new List<Enemy>();

            foreach (var item in member.characterTargeting.enemiesInRange)
            {
                // if this object doesnt have an enemy component, ignore it
                if (item.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemiesInRange.Add(enemy.GetComponent<Enemy>());
                }
            }

            if (enemiesInRange.Count > 0)
            {
                currentlyTargetedEnemy = enemiesInRange[0];
            }
            else
            {
                currentlyTargetedEnemy = null;
            }
            yield break;
        }

        public void OnObjectEnteredTargetRangeEvent_CheckIfEnemy(object sender, CharacterTargeting.OnEnemyEnteredTargetRangeEventArgs e)
        {
            if (e.other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemiesInRange.Add(enemy.GetComponent<Enemy>());
            }
        }

        public void OnObjectExitTargetRangeEvent_CheckIfEnemy(object sender, CharacterTargeting.OnEnemyExitTargetRangeEventArgs e)
        {
            if (e.other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemiesInRange.Remove(enemy.GetComponent<Enemy>());
            }
        }

        public virtual void StopTargeting(PartyMember member)
        {
            isTargeting = false;
            enemiesInRange = null;
        }

        public virtual IEnumerable<Vector3> SelectTarget(PartyMembers member)
        {
            currentlyTargetedEnemy = enemiesInRange[enemiesInRange.IndexOf(currentlyTargetedEnemy) + 1];
            yield break;
        }
    }
}
