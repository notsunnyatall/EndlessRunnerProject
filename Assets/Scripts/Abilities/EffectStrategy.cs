using System;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    public abstract class EffectStrategy : ScriptableObject
    {
        public abstract void StartEffect(GameObject target, Action effectFinished);
    }
}