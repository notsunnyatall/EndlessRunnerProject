using UnityEngine;

namespace EndlessRunner.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int initialHealthPoints = 2;
        int healthPoints = 0;

        public int GetHealthPoints()
        {
            return healthPoints;
        }

        public void TakeDamage(int damagePoints)
        {
            healthPoints = Mathf.Max(0, healthPoints - damagePoints);
        }

        void Awake()
        {
            healthPoints = initialHealthPoints;
        }
    }
}
