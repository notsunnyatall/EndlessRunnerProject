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

        public bool Use(GameObject user)
        {
            Mana mana = user.GetComponent<Mana>();

            if(mana.GetCurrentMana() < manaCost) 
            {
                return false;
            }

            CooldownStore cooldownStore = user.GetComponent<CooldownStore>();

            if(cooldownStore.GetTimeRemaining(this) > 0) 
            {
                return false;
            }

            AbilityData data = new(user);

            targeting.StartTargeting(data, () => TargetAcquired(data));

            return true;
        }

        void TargetAcquired(AbilityData data)
        {
            foreach(var effect in effects)
            {
                effect.StartEffect(data, EffectFinished);
            }

            Mana mana = data.GetUser().GetComponent<Mana>();

            if(!mana.Use(manaCost))
            {
                return;
            }

            CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>();
            
            cooldownStore.StartCooldown(this, cooldownTime);
        }

        void EffectFinished()
        {

        }
    }
}