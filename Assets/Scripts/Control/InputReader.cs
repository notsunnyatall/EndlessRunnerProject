using RainbowAssets.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessRunner.Control
{
    public class InputReader : MonoBehaviour, IPredicateEvaluator
    {
        PlayerInput playerInput;

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        InputAction GetInputAction(string actionName)
        {
            return playerInput.actions[actionName];
        } 

        bool? IPredicateEvaluator.Evaluate(string predicate, string[] parameters)
        {
            switch(predicate)
            {
                case "Input Action Pressed":
                    return GetInputAction(parameters[0]).WasPressedThisFrame();
                
                case "Input Action Released":
                    return GetInputAction(parameters[0]).WasReleasedThisFrame();
                
                case "Input Action Hold":
                    return GetInputAction(parameters[0]).IsPressed();
            }

            return null;
        }
    }
}
