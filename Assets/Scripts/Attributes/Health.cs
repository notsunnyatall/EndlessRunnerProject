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
        const string saveKey = "currentHealth";

        public int GetMaxHealthPoints()
        {
            return maxHealthPoints;
        }

        public int GetCurrentHealthPoints()
        {
            return currentHealthPoints;
        }

        public float GetHealthFraction()
        {
            return (float)currentHealthPoints / maxHealthPoints;
        }

        public bool IsDead()
        {
            return currentHealthPoints <= 0;
        }

        public void SaveState()
        {
            PlayerPrefs.SetInt(name + saveKey, currentHealthPoints);
            PlayerPrefs.Save();
        }

        public void LoadState()
        {
            if(PlayerPrefs.HasKey(name + saveKey))
            {
                currentHealthPoints = PlayerPrefs.GetInt(name + saveKey);
                Heal(0);
            }
            else
            {
                currentHealthPoints = maxHealthPoints;
            }
        }

        public void RemoveState()
        {
            if(PlayerPrefs.HasKey(name + saveKey))
            {
                PlayerPrefs.DeleteKey(name + saveKey);
            }
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
