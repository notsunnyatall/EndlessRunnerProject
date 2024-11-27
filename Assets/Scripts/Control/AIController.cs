using EndlessRunner.Abilities;
using Unity.VisualScripting;
using UnityEngine;

namespace EndlessRunner.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float abilityStartTime = 5;
        [SerializeField] AudioSource levelAudioSource;
        AbilityStore abilityStore;
        float currentTime;

        void Awake()
        {
            abilityStore = GetComponent<AbilityStore>();
        }

        void Start()
        {
            levelAudioSource.Stop();
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