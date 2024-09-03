using RainbowAssets.Utils;
using UnityEngine;

namespace EndlessRunner.Core
{
    public class ForceReceiver : MonoBehaviour, IPredicateEvaluator, IAction
    {
        [SerializeField] float jumpForce = 2;
        [SerializeField] float maxJumpVelocity = 10;
        [SerializeField] float gravityMultiplier = 2;
        CharacterController controller;
        float verticalVelocity = 0;
        
        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            ApplyGravity();
        }

        void ApplyGravity()
        {
            float gravity = Physics.gravity.y * gravityMultiplier * Time.deltaTime;

            if(controller.isGrounded && verticalVelocity < 0)
            {
                verticalVelocity = gravity;
            }
            else
            {
                verticalVelocity += gravity;
            }

            controller.Move(Vector2.up * verticalVelocity * Time.deltaTime);
        }

        bool MaxJumpVelocityReached()
        {
            return verticalVelocity >= maxJumpVelocity;
        }

        void Jump()
        {
            if(!MaxJumpVelocityReached())
            {
                verticalVelocity += jumpForce;
            }
        }

        void IAction.DoAction(string actionID, string[] parameters)
        {
            switch(actionID)
            {
                case "Jump":
                    Jump();
                    break;
            }
        }

        bool? IPredicateEvaluator.Evaluate(string predicate, string[] parameters)
        {
            switch(predicate)
            {
                case "Is Grounded":
                    return controller.isGrounded;

                case "Max Jump Velocity Reached":
                    return MaxJumpVelocityReached();
            }

            return null;
        }
    }
}