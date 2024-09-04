using UnityEngine;

namespace EndlessRunner.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int initialHealthPoints = 2;
        [SerializeField] int healthPoints = 0;

        public void TakeDamage(int damagePoints)
        {
            healthPoints -= damagePoints;
        }

        void Awake()
        {
            healthPoints = initialHealthPoints;
        }
    }
}
