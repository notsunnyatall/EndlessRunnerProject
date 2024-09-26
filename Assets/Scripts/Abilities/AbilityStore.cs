using EndlessRunner.Core;
using RainbowAssets.Utils;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    public class AbilityStore : MonoBehaviour, IAction, IPredicateEvaluator
    {
        [SerializeField] Ability[] abilities;
        Ability currentAbility = null;
        CooldownStore cooldownStore;

        void Awake()
        {
            cooldownStore = GetComponent<CooldownStore>();
        }

        void Use(int index)
        {   
            currentAbility = abilities[index];
            currentAbility.Use(gameObject, cooldownStore, AbilityFinished);
        }

        bool CanUse(int index)
        {
            return cooldownStore.GetTimeRemaining(abilities[index]) == 0;
        }

        void AbilityFinished()
        {   
            currentAbility = null;
        }

        void IAction.DoAction(string actionID, string[] parameters)
        {
            switch(actionID)
            {
                case "Use Ability":
                    Use(int.Parse(parameters[0]));
                    break;
            }
        }

        bool? IPredicateEvaluator.Evaluate(string predicate, string[] parameters)
        {
            switch(predicate)
            {
                case "Ability Finished":
                    return currentAbility == null;

                case "Can Use Ability":
                    return CanUse(int.Parse(parameters[0]));
            }

            return null;
        }
    }
}