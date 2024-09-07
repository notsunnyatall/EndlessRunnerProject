using RainbowAssets.Utils;
using UnityEngine;

namespace EndlessRunner.Core
{
    public class ForceReceiver : MonoBehaviour, IPredicateEvaluator, IAction
    {
        [SerializeField] float jumpForce = 0.15f;
        [SerializeField] float jumpTime = 0.15f;
        [SerializeField] float gravityMultiplier = 3;
        CharacterController controller;
        float verticalVelocity = 0;
        float timeSinceJumped = Mathf.Infinity;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            timeSinceJumped += Time.deltaTime;

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

        bool JumpTimeFinished()
        {
            return jumpTime < timeSinceJumped;
        }

        void Jump()
        {
            if(controller.isGrounded)
            {
                timeSinceJumped = 0;
            }

            verticalVelocity += jumpForce;
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

                case "Jump Time Finished":
                    return JumpTimeFinished();
            }

            return null;
        }
    }
}