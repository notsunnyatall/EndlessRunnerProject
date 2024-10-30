using System.Collections.Generic;
using EndlessRunner.Inventories;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    public class CooldownStore : MonoBehaviour
    {
        Dictionary<InventoryItem, float> cooldownTimers = new();
        Dictionary<InventoryItem, float> initialCooldownTimers = new();

        public void StartCooldown(InventoryItem target, float cooldownTime)
        {
            cooldownTimers[target] = cooldownTime;
            initialCooldownTimers[target] = cooldownTime;
        }

        public float GetTimeRemaining(InventoryItem target)
        {
            if(cooldownTimers.ContainsKey(target))
            {
                return cooldownTimers[target];
            }

            return 0;
        }

        public float GetFractionRemaining(InventoryItem target)
        {
            if(cooldownTimers.ContainsKey(target))
            {
                return cooldownTimers[target] / initialCooldownTimers[target];
            }

            return 0;
        }

        void Update()
        {
            var keys = new List<InventoryItem>(cooldownTimers.Keys);

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
