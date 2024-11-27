using EndlessRunner.Attributes;
using UnityEngine;

namespace EndlessRunner.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] int damage = 1;
        [SerializeField] float speed = 5;
        [SerializeField] int hits = 3;
        GameObject user;
        int currentHits = 0;

        public void SetData(GameObject user, Vector2 direction)
        {
            this.user = user;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        void Update()
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject != user)
            {
                if(other.TryGetComponent(out Health health))
                {
                    if(user.GetComponent<Health>().IsDead())
                    {
                        return;
                    }

                    health.TakeDamage(damage);

                    currentHits++;

                    if(currentHits >= hits)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}