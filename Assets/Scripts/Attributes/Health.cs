using System;
using UnityEngine;
using UnityEngine.Events;

namespace EndlessRunner.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int initialHealthPoints = 2;
        [SerializeField] UnityEvent onDamageTaken;
        [SerializeField] UnityEvent onDie;
        bool isDead = false;
        int healthPoints = 0;

        public int GetHealthPoints()
        {
            return healthPoints;
        }

        public void TakeDamage(int damagePoints)
        {
            if(!isDead)
            {
                healthPoints = Mathf.Max(0, healthPoints - damagePoints);

                if(healthPoints <= 0)
                {
                    isDead = true;
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
            healthPoints = initialHealthPoints;
        }
    }
}
