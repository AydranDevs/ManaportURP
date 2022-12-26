using System.Collections;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Actions;

namespace Manapotion.Actions.Projectiles
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/Projectiles/New ProjectileHandler")]
    public class ProjectileHandler : ScriptableObject
    {
        public ProjectileElementPrefabsScriptableObject projectileElementPrefabsScriptableObject;

        public virtual IEnumerator SpawnProjectile(PartyMember member, DamageInstance damageInstance)
        {
            Transform projectile = Instantiate(projectileElementPrefabsScriptableObject.GetElementProjectilePrefab((ElementID)damageInstance.damageInstanceElement), member.transform.position, Quaternion.identity).transform;
            projectile.GetComponent<ProjectileInstance>().Setup(
                ((Vector3)member.characterInput.GetInputProvider().GetState().targetPos - member.transform.position).normalized,
                damageInstance
            );
            yield return null;
        }
    }
}
