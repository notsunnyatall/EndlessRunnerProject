using System;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/New Ability")]
    public class Ability : ScriptableObject
    {
        [SerializeField] TargetingStrategy targeting;
        [SerializeField] EffectStrategy[] effects;
        public Action abilityFinished;

        public void Use(GameObject user)
        {
            targeting.StartTargeting(user, TargetAcquired);
        }

        void TargetAcquired(GameObject target)
        {
            foreach(var effect in effects)
            {
                effect.StartEffect(target, abilityFinished);
            }
        }
    }
}