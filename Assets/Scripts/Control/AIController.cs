using EndlessRunner.Abilities;
using UnityEngine;

namespace EndlessRunner.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float abilityStartTime = 5;
        AbilityStore abilityStore;
        float currentTime;

        void Awake()
        {
            abilityStore = GetComponent<AbilityStore>();
        }

        void Update()
        {
            currentTime += Time.deltaTime;

            if(currentTime >= abilityStartTime && !abilityStore.UseAbility())
            {
                abilityStore.ScrollAbility();
            }
        }
    }
}