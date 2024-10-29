using System;
using EndlessRunner.Control;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessRunner.Abilities
{
    public class AbilityStore : MonoBehaviour
    {
        [SerializeField] Ability[] abilities;
        InputReader inputReader;
        int currentAbilityIndex = 0;
        public event Action storeUpdated;

        public Ability GetCurrentAbility()
        {
            return abilities[currentAbilityIndex];
        }

        void Awake()
        {
            inputReader = GetComponent<InputReader>();
        }

        void Start()
        {
            inputReader.GetInputAction("Use Ability").performed += UseAbility;
            inputReader.GetInputAction("Scroll Ability").performed += ScrollAbility;
        }

        void UseAbility(InputAction.CallbackContext context)
        {   
            GetCurrentAbility().Use(gameObject);
        }

        void ScrollAbility(InputAction.CallbackContext context)
        {
            if(currentAbilityIndex == abilities.Length - 1)
            {
                currentAbilityIndex = 0;
            }
            else
            {
                currentAbilityIndex++;
            }

            storeUpdated?.Invoke();
        }
    }
}