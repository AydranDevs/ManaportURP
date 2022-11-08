using UnityEngine;

namespace Manapotion.Actions.Projectiles
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/Projectiles/New ProjectileElementPrefabsScriptableObject")]
    public class ProjectileElementPrefabsScriptableObject : ScriptableObject
    {
        public Transform PREFAB_arcaneProjectile;
        public Transform PREFAB_pyroProjectile;
        public Transform PREFAB_cryoProjectile;
        public Transform PREFAB_toxiProjectile;
        public Transform PREFAB_voltProjectile;

        public Transform GetElementProjectilePrefab(ElementID element)
        {
            switch (element)
            {
                default: return null;
                case ElementID.Arcane: return PREFAB_arcaneProjectile;
                case ElementID.Pyro: return PREFAB_pyroProjectile;
                case ElementID.Cryo: return PREFAB_cryoProjectile;
                case ElementID.Toxi: return PREFAB_toxiProjectile;
                case ElementID.Volt: return PREFAB_voltProjectile;
            }
        }
    }
}
