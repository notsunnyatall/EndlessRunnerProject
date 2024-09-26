using System;
using EndlessRunner.Attributes;
using UnityEngine;

namespace EndlessRunner.Abilities.Effect
{
    [CreateAssetMenu(menuName = "Abilities/Effects/Health Effect")]
    public class HealthEffect : EffectStrategy
    {
        [SerializeField] int healthPoints;

        public override void StartEffect(AbilityData data, Action finished)
        {
            if(data.GetUser().TryGetComponent(out Health health))
            {
                if(healthPoints < 0)
                {
                    health.TakeDamage(healthPoints);
                }
                else
                {   
                    health.Heal(healthPoints);
                }
            }

            finished();
        }
    }
}