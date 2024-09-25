using System;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Targeting/Self Targeting")]
    public class SelfTargeting : TargetingStrategy
    {
        public override void StartTargeting(GameObject user, Action<GameObject> finished)
        {
            finished(user);
        }
    }
}