using EndlessRunner.Attributes;
using UnityEngine;

namespace EndlessRunner.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] int damagePoints = 1;
        [SerializeField] float speed = 5;
        [SerializeField] Vector2 moveDirection = Vector2.left;

        void Update()
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponent<Health>();

            if(health != null)
            {
                health.TakeDamage(damagePoints);
            }
        }
    }
}