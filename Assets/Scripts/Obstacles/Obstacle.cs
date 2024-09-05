using EndlessRunner.Attributes;
using EndlessRunner.Inventories;
using UnityEngine;

namespace EndlessRunner.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] TriggerEffect effect;
        [SerializeField] int effectPoints = 1;
        [SerializeField] float speed = 5;
        [SerializeField] Vector2 moveDirection = Vector2.left;
        [SerializeField] bool destroyAfterHit = false;

        enum TriggerEffect
        {
            Damage,
            Currency
        }

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
            switch (effect)
            {
                case TriggerEffect.Damage:
                    DoDamage(other);
                    break;

                case TriggerEffect.Currency:
                    Deposit(other);
                    break;
            }

            if(destroyAfterHit)
            {
                Destroy(gameObject);
            }
        }

        void DoDamage(Collider other)
        {
            if(other.TryGetComponent(out Health health))
            {
                health.TakeDamage(effectPoints);
            }
        }

        void Deposit(Collider other)
        {
            if(other.TryGetComponent(out Purse purse))
            {
                purse.Deposit(effectPoints);
            }
        }
    }
}