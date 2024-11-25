using UnityEngine;

namespace EndlessRunner.Attributes
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] float maxMana = 10;
        [SerializeField] float regenRate = 0.1f;
        float currentMana;

        public float GetManaFraction()
        {
            return currentMana / maxMana;
        }

        public float GetCurrentMana()
        {
            return currentMana;
        }

        public bool Use(float manaToUse)
        {
            if(manaToUse > currentMana)
            {
                return false;
            }

            currentMana -= manaToUse;
            return true;
        }

        void Awake()
        {
            currentMana = maxMana;
        }

        void Update()
        {
            if(currentMana < maxMana)
            {
                currentMana += Time.deltaTime * regenRate;

                if(currentMana > maxMana)
                {
                    currentMana = maxMana;
                }
            }
        }
    }
}