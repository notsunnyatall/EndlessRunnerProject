using System;
using UnityEngine;

namespace EndlessRunner.Abilities.Effects
{
    [CreateAssetMenu(menuName = "Abilities/Effects/Spawn Prefab Effect")]
    public class SpawnPrefabEffect : EffectStrategy
    {
        [SerializeField] GameObject prefab;

        public override void StartEffect(AbilityData data, Action finished)
        {
            Instantiate(prefab, data.GetUser().transform);
            finished();
        }
    }
}