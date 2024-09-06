using UnityEngine;
using UnityEngine.Events;

namespace EndlessRunner.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealthPoints = 2;
        public UnityEvent onDie;
        public UnityEvent onDamageTaken;
        int currentHealthPoints = 0;

        public int GetMaxHealthPoints()
        {
            return maxHealthPoints;
        }

        public int GetCurrentHealthPoints()
        {
            return currentHealthPoints;
        }

        public void TakeDamage(int damagePoints)
        {
            if(!IsDead())
            {
                currentHealthPoints = Mathf.Max(0, currentHealthPoints - damagePoints);

                if(currentHealthPoints <= 0)
                {
                    onDie?.Invoke();
                }
                else
                {
                    onDamageTaken?.Invoke();
                }
            }
        }

        void Awake()
        {
            currentHealthPoints = maxHealthPoints;
        }

        bool IsDead()
        {
            return currentHealthPoints <= 0;
        }
    }
}
