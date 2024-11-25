using System;
using EndlessRunner.Attributes;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    public class AbilityStore : MonoBehaviour
    {
        [SerializeField] AbilityConfig[] abilitiesConfig;
        [SerializeField] bool useHealthConditions = false;
        Health health;
        int currentAbilityIndex = 0;
        public event Action storeUpdated;

        [Serializable]
        struct AbilityConfig
        {
            [Range(0,1)] public float minHealthPercentage;
            [Range(0,1)] public float maxHealthPercentage;
            public Ability ability;
        }

        public Ability GetCurrentAbility()
        {
            return abilitiesConfig[currentAbilityIndex].ability;
        }

        public bool UseAbility()
        {   
            if(health.IsDead())
            {
                return false;
            }

            if(useHealthConditions)
            {
                float minHealth = abilitiesConfig[currentAbilityIndex].minHealthPercentage;
                float maxHealth = abilitiesConfig[currentAbilityIndex].maxHealthPercentage;

                if(health.GetHealthFraction() < minHealth)
                {
                    return false;
                }

                if(health.GetHealthFraction() > maxHealth)
                {
                    return false;
                }
            }

            return GetCurrentAbility().Use(gameObject); 
        }

        public void ScrollAbility()
        {
            if(health.IsDead())
            {
                return;
            }

            if(currentAbilityIndex == abilitiesConfig.Length - 1)
            {
                currentAbilityIndex = 0;
            }
            else
            {
                currentAbilityIndex++;
            }

            storeUpdated?.Invoke();
        }

        void Awake()
        {
            health = GetComponent<Health>();
        }
    }
}