using UnityEngine;
using UnityEngine.Events;

namespace EndlessRunner.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int initialHealthPoints = 2;
        [SerializeField] UnityEvent onDie;
        int healthPoints = 0;

        public int GetHealthPoints()
        {
            return healthPoints;
        }

        public void TakeDamage(int damagePoints)
        {
            healthPoints = Mathf.Max(0, healthPoints - damagePoints);

            if(healthPoints <= 0)
            {
                onDie?.Invoke();
            }
        }

        void Awake()
        {
            healthPoints = initialHealthPoints;
        }
    }
}
