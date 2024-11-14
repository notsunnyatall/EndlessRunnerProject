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
        Vector2 direction = Vector2.left;
        int currentHits = 0;

        public void SetData(GameObject user, Vector2 direction)
        {
            this.user = user;
            this.direction = direction;
        }

        void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject != user)
            {

                            print("Trigger enter");
                if(other.TryGetComponent(out Health health))
                {
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