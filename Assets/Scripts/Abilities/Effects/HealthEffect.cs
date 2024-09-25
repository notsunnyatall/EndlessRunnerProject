using System;
using EndlessRunner.Attributes;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Effects/Health Effect")]
    public class HealthEffect : EffectStrategy
    {
        [SerializeField] int healthPoints;

        public override void StartEffect(GameObject target, Action finished)
        {
            if(target.TryGetComponent(out Health health))
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