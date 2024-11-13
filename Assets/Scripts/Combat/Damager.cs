using EndlessRunner.Attributes;
using UnityEngine;

namespace EndlessRunner.Combat
{
    public class Damager : MonoBehaviour
    {   
        [SerializeField] int damagePoints = 1;

        void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Health health))
            {
                health.TakeDamage(damagePoints);
            }
        }
    }
}