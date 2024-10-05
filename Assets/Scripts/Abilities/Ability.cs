using System;
using EndlessRunner.Attributes;
using EndlessRunner.Core;
using EndlessRunner.Inventories;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/New Ability")]
    public class Ability : InventoryItem
    {
        [SerializeField] TargetingStrategy targeting;
        [SerializeField] EffectStrategy[] effects;
        [SerializeField] float cooldownTime = 5;
        [SerializeField] float manaCost = 5;

        public bool CanUse(CooldownStore cooldownStore, Mana mana)
        {
            return cooldownStore.GetTimeRemaining(this) == 0 && mana.CanUse(manaCost);
        }

        public void Use(GameObject user, CooldownStore cooldownStore, Mana mana, Action abilityFinished)
        {
            if(CanUse(cooldownStore, mana))
            {
                AbilityData abilityData = new(user);

                targeting.StartTargeting(abilityData, () => TargetAcquired
                (
                    abilityData, 
                    cooldownStore, 
                    mana, 
                    abilityFinished
                ));  
            }
        }

        void TargetAcquired(AbilityData data, CooldownStore cooldownStore, Mana mana, Action abilityFinished)
        {
            foreach(var effect in effects)
            {
                effect.StartEffect(data, abilityFinished);
            }
            
            cooldownStore.StartCooldown(this, cooldownTime);
            mana.Use(manaCost);
        }
    }
}