using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunner.Core
{
    public class CooldownStore : MonoBehaviour
    {
        Dictionary<object, float> cooldownLookup = new();

        public void StartCooldown(object target, float cooldownTime)
        {
            cooldownLookup[target] = cooldownTime;
        }

        public float GetTimeRemaining(object target)
        {
            if(cooldownLookup.ContainsKey(target))
            {
                return cooldownLookup[target];
            }

            return 0;
        }

        void Update()
        {
            var keys = new List<object>(cooldownLookup.Keys);

            foreach(var key in keys)
            {
                cooldownLookup[key] -= Time.deltaTime;

                if(cooldownLookup[key] < 0)
                {
                    cooldownLookup.Remove(key);
                }
            }
        }
    }
}
