using UnityEngine;

namespace EndlessRunner.Attributes
{
    public class Mana : MonoBehaviour
    {
        [SerializeField] float maxMana = 10;
        [SerializeField] float regenRate = 0.1f;
        [SerializeField] float currentMana;

        public float GetManaFraction()
        {
            return currentMana / maxMana;
        }

        public bool CanUse(float manaToUse)
        {
            return manaToUse <= currentMana;
        }

        public void Use(float manaToUse)
        {
            if(CanUse(manaToUse))
            {
                currentMana -= manaToUse;
            }
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