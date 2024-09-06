using RainbowAssets.Utils;
using UnityEngine;

namespace EndlessRunner.Core
{
    public class AnimationPlayer : MonoBehaviour, IAction
    {
        [SerializeField] float transitionTime = 0.1f;
        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void IAction.DoAction(string actionID, string[] parameters)
        {
            switch(actionID)
            {
                case "Play Animation":
                    animator.CrossFadeInFixedTime(parameters[0], transitionTime);
                    break;
            }
        }
    }
}
