using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunner.Core
{
    public class CooldownStore : MonoBehaviour
    {
        Dictionary<object, float> cooldownTimers = new();
        Dictionary<object, float> initialCooldownTimers = new();

        public void StartCooldown(object target, float cooldownTime)
        {
            cooldownTimers[target] = cooldownTime;
            initialCooldownTimers[target] = cooldownTime;
        }

        public float GetTimeRemaining(object target)
        {
            if(cooldownTimers.ContainsKey(target))
            {
                return cooldownTimers[target];
            }

            return 0;
        }

        public float GetFractionRemaining(object target)
        {
            if(cooldownTimers.ContainsKey(target))
            {
                return cooldownTimers[target] / initialCooldownTimers[target];
            }

            return 0;
        }

        void Update()
        {
            var keys = new List<object>(cooldownTimers.Keys);

            foreach(var key in keys)
            {
                cooldownTimers[key] -= Time.deltaTime;

                if(cooldownTimers[key] < 0)
                {
                    cooldownTimers.Remove(key);
                    initialCooldownTimers.Remove(key);
                }
            }
        }
    }
}
