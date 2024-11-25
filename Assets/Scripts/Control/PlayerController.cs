using System.Collections;
using EndlessRunner.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessRunner.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float jumpForce = 8;
        [SerializeField] float maxJumpTime = 0.35f;
        [SerializeField] float gravityMultiplier = 3.2f;
        [SerializeField] float edgeHitDuration = 0.4f;
        PlayerInput playerInput;
        AbilityStore abilityStore;
        CharacterController controller;
        Animator animator;
        float verticalVelocity;
        float timeSinceJumped = Mathf.Infinity;
        bool edgeHit;

        void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            abilityStore = GetComponent<AbilityStore>();
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            timeSinceJumped += Time.deltaTime;

            ApplyGravity();

            animator.SetBool("isGrounded", IsGrounded());
        }

        void OnEnable()
        {
            playerInput.actions["Jump"].performed += Jump;
            playerInput.actions["Use Ability"].performed += UseAbility;
            playerInput.actions["Scroll Ability"].performed += ScrollAbility;
        }

        void OnDisable()
        {
            playerInput.actions["Jump"].performed -= Jump;
            playerInput.actions["Use Ability"].performed -= UseAbility;
            playerInput.actions["Scroll Ability"].performed -= ScrollAbility;
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Edge"))
            {
                StartCoroutine(EdgeHitRoutine());
            }
        }

        void ApplyGravity()
        {
            float gravity = Physics.gravity.y * gravityMultiplier * Time.deltaTime;

            if(IsGrounded())
            {
                verticalVelocity = gravity;
            }
            else
            {
                verticalVelocity += gravity;
            }

            controller.Move(Vector2.up * verticalVelocity * Time.deltaTime);
        }

        bool IsGrounded()
        {
            return controller.isGrounded && verticalVelocity < 0 && !edgeHit;
        }

        void Jump(InputAction.CallbackContext context)
        {
            if(IsGrounded())
            {
                timeSinceJumped = 0;
                animator.SetTrigger("jump");
            }

            if(timeSinceJumped < maxJumpTime)
            {
                verticalVelocity = jumpForce;
            }
        }

        void UseAbility(InputAction.CallbackContext context)
        {
            abilityStore.UseAbility();
        }

        void ScrollAbility(InputAction.CallbackContext context)
        {
            abilityStore.ScrollAbility();
        }

        IEnumerator EdgeHitRoutine()
        {
            edgeHit = true;
            yield return new WaitForSeconds(edgeHitDuration);
            edgeHit = false;
        }
    }
}