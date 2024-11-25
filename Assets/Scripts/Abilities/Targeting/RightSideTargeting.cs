using UnityEngine;
using System;

namespace EndlessRunner.Abilities.Targeting
{
    [CreateAssetMenu(menuName = "Abilities/Targeting/Right Side Targeting")]
    public class RightSideTargeting : TargetingStrategy
    {
        public override void StartTargeting(AbilityData data, Action finished)
        {
            GameObject user = data.GetUser();

            Vector3 targetPoint = user.transform.position + user.transform.right;

            data.SetTargetPoint(targetPoint);

            finished();
        }
    }
}
