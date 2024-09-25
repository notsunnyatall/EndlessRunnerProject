using RainbowAssets.Utils;
using UnityEngine;

namespace EndlessRunner.Abilities
{
    public class AbilityStore : MonoBehaviour, IAction, IPredicateEvaluator
    {
        [SerializeField] Ability[] abilities;
        Ability currentAbility = null;

        void Use(int index)
        {   
            currentAbility = abilities[index];
            currentAbility.abilityFinished += OnAbilityFinished;
            currentAbility.Use(gameObject);
        }

        void OnAbilityFinished()
        {   
            currentAbility.abilityFinished -= OnAbilityFinished;
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
            }

            return null;
        }
    }
}