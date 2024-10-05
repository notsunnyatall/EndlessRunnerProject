using EndlessRunner.Abilities;
using EndlessRunner.Core;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunner.UI
{
    public class AbilitySlotUI : MonoBehaviour
    {
        [SerializeField] int slotIndex;
        [SerializeField] Image iconImage;
        [SerializeField] Image cooldownOverlay;
        AbilityStore abilityStore;
        CooldownStore cooldownStore;
        Ability ability;

        void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");

            abilityStore = player.GetComponent<AbilityStore>();
            cooldownStore = player.GetComponent<CooldownStore>();

            ability = abilityStore.GetAbility(slotIndex);
        }

        void Start()
        {
            iconImage.sprite = ability.GetIcon();
        }

        void Update()
        {
            cooldownOverlay.fillAmount = cooldownStore.GetFractionRemaining(ability);
        }
    }
}