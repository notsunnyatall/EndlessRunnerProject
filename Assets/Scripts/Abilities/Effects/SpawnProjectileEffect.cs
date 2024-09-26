using System;
using EndlessRunner.Combat;
using UnityEngine;

namespace EndlessRunner.Abilities.Effects
{
    [CreateAssetMenu(menuName = "Abilities/Effects/Spawn Projectile Effect")]
    public class SpawnProjectileEffect : EffectStrategy
    {
        [SerializeField] Projectile projectilePrefab;

        public override void StartEffect(AbilityData abilityData, Action finished)
        {
            GameObject user = abilityData.GetUser();

            Projectile projectileInstance = Instantiate(projectilePrefab, user.transform.position, Quaternion.identity);

            projectileInstance.SetData(user, user.transform.right);

            finished();
        }
    }
}