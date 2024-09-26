using System;
using EndlessRunner.Core;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/New Ability")]
    public class Ability : ScriptableObject
    {
        [SerializeField] TargetingStrategy targeting;
        [SerializeField] EffectStrategy[] effects;
        [SerializeField] float cooldownTime = 5;

        public void Use(GameObject user, CooldownStore cooldownStore, Action abilityFinished)
        {
            if(cooldownStore.GetTimeRemaining(this) == 0)
            {
                AbilityData abilityData = new(user);

                targeting.StartTargeting(abilityData, () => TargetAcquired(abilityData, cooldownStore, abilityFinished));  
            }
        }

        void TargetAcquired(AbilityData data, CooldownStore cooldownStore, Action abilityFinished)
        {
            foreach(var effect in effects)
            {
                effect.StartEffect(data, abilityFinished);
            }
            
            cooldownStore.StartCooldown(this, cooldownTime);
        }
    }
}