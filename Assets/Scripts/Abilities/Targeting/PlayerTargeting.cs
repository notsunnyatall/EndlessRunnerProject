using System;
using UnityEngine;

namespace EndlessRunner.Abilities.Targeting
{
    [CreateAssetMenu(menuName = "Abilities/Targeting/Player Targeting")]
    public class PlayerTargeting : TargetingStrategy
    {
        public override void StartTargeting(AbilityData data, Action finished)
        {
            data.SetTarget(GameObject.FindWithTag("Player"));
            finished();
        }
    }
}