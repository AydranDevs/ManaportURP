using Manapotion.PartySystem;

using System.Collections;
using UnityEngine;

namespace Manapotion.Actions.Projectiles
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/Projectiles/New MultiProjectileHandlerScriptableObject")]
    public class MultiProjectileHandlerScriptableObject : ProjectileHandlerScriptableObject
    {
        public int projectileCount = 3;

        [Header("Fire Rate")]
        public float fireRate_base = 0.1f;
        public Stats.StatID fireRate_affectorStat;
        public float fireRate_affectorFactor = 0.01f;

        [Header("Projectile Deviation")]
        public bool firstShotAccurate = false;
        public Vector2 projectileDeviation;

        public override IEnumerator SpawnProjectile(PartyMember member, DamageInstance damageInstance)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                Transform projectile = Instantiate(PREFAB_projectile, member.transform.position, Quaternion.identity).transform;
                Vector3 direction = ((Vector3)member.inputProvider.GetState().targetPos - member.transform.position).normalized;
                
                float rand = UnityEngine.Random.Range(projectileDeviation.x, projectileDeviation.y);

                Vector3 deviatedDirection = Quaternion.Euler(direction.x, direction.y, direction.z + rand) * direction; 
                if (firstShotAccurate && i == 0)
                {
                    deviatedDirection = direction;
                }          
                      
                projectile.GetComponent<ProjectileInstance>().Setup(
                    // ((Vector3)member.inputProvider.GetState().targetPos - member.transform.position).normalized,
                    deviatedDirection.normalized,
                    damageInstance
                );

                yield return new WaitForSeconds(fireRate_base - (fireRate_affectorFactor * member.statsManagerScriptableObject.GetStat(fireRate_affectorStat).value.modifiedValue));
            }
        }
    }
}