using System.Collections;
using EndlessRunner.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessRunner.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 4;
        [SerializeField] float jumpForce = 8;
        [SerializeField] float maxJumpTime = 0.35f;
        [SerializeField] float gravityMultiplier = 3.2f;
        [SerializeField] float edgeHitDuration = 0.4f;
        [SerializeField] bool horizontalMovement = false;
        PlayerInput playerInput;
        AbilityStore abilityStore;
        CharacterController controller;
        Animator animator;
        float verticalVelocity;
        float timeSinceJumped = Mathf.Infinity;
        bool edgeHit;

        public void ToggleInput(bool state)
        {
            playerInput.enabled = state;
        }

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

            if(horizontalMovement)
            {
                HandleMovement();
            }
            else
            {
                animator.SetFloat("movementSpeed", 1);
            }
        }

        void OnEnable()
        {
            playerInput.actions["Jump"].performed += Jump;
            playerInput.actions["Jump"].canceled += JumpCancel;
            playerInput.actions["Use Ability"].performed += UseAbility;
            playerInput.actions["Scroll Ability"].performed += ScrollAbility;
        }

        void OnDisable()
        {
            playerInput.actions["Jump"].performed -= Jump;
            playerInput.actions["Jump"].canceled -= JumpCancel;
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

        void HandleMovement()
        {
            Vector2 movement = playerInput.actions["Movement"].ReadValue<Vector2>();
            movement.y = 0;

            animator.SetFloat("movementSpeed", movement.x);

            if(movement != Vector2.zero)
            {
                transform.rotation = Quaternion.Euler(0, movement.x > 0 ? 0 : 180, 0);
            }
 
            Move(movement * movementSpeed);
        }

        void Move(Vector2 motion)
        {
            controller.Move((motion + Vector2.up * verticalVelocity) * Time.deltaTime);
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

        void JumpCancel(InputAction.CallbackContext context)
        {
            timeSinceJumped = maxJumpTime;
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