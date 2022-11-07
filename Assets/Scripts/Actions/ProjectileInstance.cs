using System.Collections;
using UnityEngine;

namespace Manapotion.Actions.Projectiles
{
    public class ProjectileInstance : MonoBehaviour
    {
        private DamageInstance damageInstance;
        private Vector3 direction;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float lifetime;

        [SerializeField]
        private ParticleSystem[] particles;

        private bool hit = false;

        public void Setup(Vector3 direction)
        {
            StartCoroutine(DestroyProjectile(false));

            this.direction = direction;
        }

        public void Setup(Vector3 direction, DamageInstance damageInstance)
        {
            StartCoroutine(DestroyProjectile(false));

            this.direction = direction;
            this.damageInstance = damageInstance;
        }

        private void Update() {
            if (hit)
            {
                return;
            }
            
            var d = direction * speed * Time.deltaTime;
            transform.position = transform.position + d;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (hit)
            {
                return;
            }

            Debug.Log($"impact ({damageInstance.damageInstanceAmount}, {damageInstance.damageInstanceElement})");    
            hit = true;
            StartCoroutine(DestroyProjectile(hit));
        }

        IEnumerator DestroyProjectile(bool impact)
        {
            if (!impact)
            {
                yield return new WaitForSeconds(lifetime);
            }
            foreach (var p in particles)
            {
                p.Stop();
            }
            Destroy(gameObject, 2f);
        }
    }
}