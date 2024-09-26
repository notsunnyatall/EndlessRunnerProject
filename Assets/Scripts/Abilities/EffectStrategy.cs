using System;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    public abstract class EffectStrategy : ScriptableObject
    {
        public abstract void StartEffect(AbilityData abilityData, Action finished);
    }
}