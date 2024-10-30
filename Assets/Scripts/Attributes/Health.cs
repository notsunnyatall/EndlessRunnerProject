using UnityEngine;
using UnityEngine.Events;

namespace EndlessRunner.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int maxHealthPoints = 2;
        public UnityEvent onDie;
        public UnityEvent onDamageTaken;
        public UnityEvent onHeal;
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

        public void Heal(int healPoints)
        {
            if(!IsDead() && !HasFullHealth())
            {
                currentHealthPoints = Mathf.Min(maxHealthPoints, currentHealthPoints + healPoints);
                onHeal?.Invoke();
            }
        }

        public bool IsDead()
        {
            return currentHealthPoints <= 0;
        }

        public void Kill()
        {
            TakeDamage(currentHealthPoints);
        }

        void Awake()
        {
            currentHealthPoints = maxHealthPoints;
        }

        bool HasFullHealth()
        {
            return currentHealthPoints >= maxHealthPoints;
        }
    }
}
