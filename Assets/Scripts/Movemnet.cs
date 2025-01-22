using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Game
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]

    public class Movement : MonoBehaviour
    {
        private CharacterController characterController;

        [SerializeField]
        private Transform cam;

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private float moveSpeed = 5f;

        private float rotationSmoothDamping = 0.1f;
        private float currentVelocity;

        private const float GRAVITY = -9.81f;
        [SerializeField]
        private float jumpHeight = 2.0f; // Adjust as needed
        private Vector3 velocity;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            HandleMovement();

            Debug.Log(characterController.isGrounded);
        }

        private void HandleMovement()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

            if (direction.magnitude > 0.1f)
            {
                float targetDir = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float currentDir = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetDir, ref currentVelocity, rotationSmoothDamping);
                transform.rotation = Quaternion.Euler(0, currentDir, 0);

                Vector3 moveDir = Quaternion.Euler(0, targetDir, 0) * Vector3.forward;
                velocity.x = moveDir.x * moveSpeed;
                velocity.z = moveDir.z * moveSpeed;
            }
            else
            {
                velocity.x = 0f;
                velocity.z = 0f;
            }

            HandleJumping();
            characterController.Move(velocity * Time.deltaTime);
        }

        private void HandleJumping()
        {
            if (characterController.isGrounded)
            {
                velocity.y = -2f; // Reset vertical velocity when grounded

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * GRAVITY); // Apply jump force
                }
            }
            else
            {
                velocity.y += GRAVITY * Time.deltaTime; // Apply gravity when in the air
            }
        }
    }

}
