using EndlessRunner.Attributes;
using EndlessRunner.Core;
using EndlessRunner.Inventories;
using UnityEngine;

namespace EndlessRunner.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] TriggerEffect effect;
        [SerializeField] Vector2 direction = Vector2.left;
        [SerializeField] int effectPoints = 1;
        [SerializeField] float speed = 5;
        [SerializeField] bool destroyAfterHit = false;
        [SerializeField] bool useGameSpeed = true;
        GameSettings gameSettings;
        GameObject user;

        public void SetData(GameObject user, Vector2 direction)
        {
            this.user = user;
            this.direction = direction;
        }

        enum TriggerEffect
        {
            Damage,
            Currency,
            None
        }

        void Awake()
        {
            gameSettings = FindObjectOfType<GameSettings>();
        }

        void Update()
        {
            speed = useGameSpeed ? gameSettings.GetGameSpeed() : speed;

            transform.Translate(direction * speed * Time.deltaTime);
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject != user)
            {
                switch(effect)
                {
                    case TriggerEffect.Damage:
                        DoDamage(other);
                        break;

                    case TriggerEffect.Currency:
                        Deposit(other);
                        break;

                    case TriggerEffect.None:
                        break;
                }

                if(destroyAfterHit)
                {
                    Destroy(gameObject);
                }
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