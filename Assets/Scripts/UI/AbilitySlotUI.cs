using EndlessRunner.Abilities;
using EndlessRunner.Core;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunner.UI
{
    public class AbilitySlotUI : MonoBehaviour
    {
        [SerializeField] Image iconImage;
        [SerializeField] Image cooldownOverlay;
        AbilityStore abilityStore;
        CooldownStore cooldownStore;

        void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");

            abilityStore = player.GetComponent<AbilityStore>();
            cooldownStore = player.GetComponent<CooldownStore>();
        }

        void Start()
        {
            abilityStore.storeUpdated += Refresh;
            Refresh();
        }

        void Update()
        {
            cooldownOverlay.fillAmount = cooldownStore.GetFractionRemaining(abilityStore.GetCurrentAbility());
        }

        void Refresh()
        {
            iconImage.sprite = abilityStore.GetCurrentAbility().GetIcon();
        }
    }
}