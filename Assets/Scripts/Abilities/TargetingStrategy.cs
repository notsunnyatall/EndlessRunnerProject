using System;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    public abstract class TargetingStrategy : ScriptableObject
    {
        public abstract void StartTargeting(AbilityData abilityData, Action finished);
    }
}