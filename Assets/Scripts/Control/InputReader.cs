using System;
using RainbowAssets.Utils;
using UnityEngine;

namespace EndlessRunner.Control
{
    public class InputReader : MonoBehaviour, IPredicateEvaluator
    {
        bool? IPredicateEvaluator.Evaluate(string predicate, string[] parameters)
        {
            switch(predicate)
            {
                case "Key Pressed":
                    KeyCode keyCode = Enum.Parse<KeyCode>(parameters[0]);
                    return Input.GetKey(keyCode);
            }

            return null;
        }
    }
}
