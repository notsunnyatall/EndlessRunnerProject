using EndlessRunner.Abilities;
using UnityEngine;

namespace EndlessRunner.Control
{
    public class AIController : MonoBehaviour
    {
        AbilityStore abilityStore;

        void Awake()
        {
            abilityStore = GetComponent<AbilityStore>();
        }

        void Update()
        {
            if(!abilityStore.UseAbility())
            {
                abilityStore.ScrollAbility();
            }
        }
    }
}