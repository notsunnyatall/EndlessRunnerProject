using System.Collections;
using RainbowAssets.Utils;
using UnityEngine;

namespace EndlessRunner.Core
{
    public class ForceReceiver : MonoBehaviour, IPredicateEvaluator, IAction
    {
        [SerializeField] float jumpForce = 0.15f;
        [SerializeField] float jumpTime = 0.15f;
        [SerializeField] float gravityMultiplier = 3;
        [SerializeField] float edgeHitDuration = 0.2f;
        CharacterController controller;
        float verticalVelocity = 0;
        float timeSinceJumped = Mathf.Infinity;
        bool edgeHit = false;

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

            if(controller.isGrounded && verticalVelocity < 0 && !edgeHit)
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
            verticalVelocity = jumpForce;
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Edge"))
            {
                StartCoroutine(EdgeHitRoutine());
            }
        }

        IEnumerator EdgeHitRoutine()
        {
            edgeHit = true;
            yield return new WaitForSeconds(edgeHitDuration);
            edgeHit = false;
        }

        void IAction.DoAction(string actionID, string[] parameters)
        {
            switch(actionID)
            {
                case "Start Jump":
                    timeSinceJumped = 0;
                    break;

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
                    return controller.isGrounded && !edgeHit;

                case "Jump Time Finished":
                    return JumpTimeFinished();
            }

            return null;
        }
    }
}