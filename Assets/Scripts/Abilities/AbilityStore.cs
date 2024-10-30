using System;
using EndlessRunner.Attributes;
using EndlessRunner.Control;
using Unity.VisualScripting;
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
        Health health;

        public Ability GetCurrentAbility()
        {
            return abilities[currentAbilityIndex];
        }

        void Awake()
        {
            inputReader = GetComponent<InputReader>();
            health = GetComponent<Health>();
        }

        void Start()
        {
            inputReader.GetInputAction("Use Ability").performed += UseAbility;
            inputReader.GetInputAction("Scroll Ability").performed += ScrollAbility;
        }

        void UseAbility(InputAction.CallbackContext context)
        {   
            if(health.IsDead())
            {
                return;
            }

            GetCurrentAbility().Use(gameObject); 
        }

        void ScrollAbility(InputAction.CallbackContext context)
        {
            if(health.IsDead())
            {
                return;
            }

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