using EndlessRunner.Attributes;
using UnityEngine;

namespace EndlessRunner.Combat
{
    public class HealthPickup : MonoBehaviour
    {
        [SerializeField] int healthPoints = 1;

        void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Health health))
            {
                health.Heal(healthPoints);
                Destroy(gameObject);
            }
        }
    }
}