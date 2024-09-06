using RainbowAssets.Utils;
using UnityEngine;

namespace EndlessRunner.Core
{
    public class AnimationPlayer : MonoBehaviour, IAction, IPredicateEvaluator
    {
        [SerializeField] float transitionTime = 0.1f;
        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        float GetAnimationTime(string tag)
        {
            var current = animator.GetCurrentAnimatorStateInfo(0);

            if(current.IsTag(tag) && !animator.IsInTransition(0))
            {
                return current.normalizedTime;
            }

            return 0;
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

        bool? IPredicateEvaluator.Evaluate(string predicate, string[] parameters)
        {
            switch(predicate)
            {
                case "Animation Over":
                    return GetAnimationTime(parameters[0]) >= 1;
            }

            return null;
        }
    }
}
