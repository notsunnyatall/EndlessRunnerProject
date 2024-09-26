using System;
using UnityEngine;

namespace EndlessRunner.Abilities.Targeting
{
    [CreateAssetMenu(menuName = "Abilities/Targeting/Self Targeting")]
    public class SelfTargeting : TargetingStrategy
    {
        public override void StartTargeting(AbilityData abilityData, Action finished)
        {
            abilityData.SetTarget(abilityData.GetUser());
            finished();
        }
    }
}