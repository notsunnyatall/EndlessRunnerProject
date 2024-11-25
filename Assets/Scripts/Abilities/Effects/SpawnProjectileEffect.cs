using System;
using EndlessRunner.Combat;
using UnityEngine;

namespace EndlessRunner.Abilities.Effects
{
    [CreateAssetMenu(menuName = "Abilities/Effects/Spawn Projectile Effect")]
    public class SpawnProjectileEffect : EffectStrategy
    {
        [SerializeField] Projectile projectilePrefab;

        public override void StartEffect(AbilityData data, Action finished)
        {
            GameObject user = data.GetUser();
            Vector2 startPosition = user.transform.position;
            Vector2 direction;

            if(data.GetTarget() != null)
            {
                Vector2 targetPosition = data.GetTarget().transform.position;
                direction = (targetPosition - startPosition).normalized;
            }
            else
            {
                Vector2 targetPoint = data.GetTargetPoint();
                direction = (targetPoint - startPosition).normalized;
            }

            Projectile projectileInstance = Instantiate(projectilePrefab, startPosition, Quaternion.identity);
            projectileInstance.SetData(user, direction);

            finished();
        }
    }
}