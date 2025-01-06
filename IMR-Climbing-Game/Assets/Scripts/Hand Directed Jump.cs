using UnityEngine;

public class HandDirectedJump : MonoBehaviour
{
    [Tooltip("Force applied for the jump.")]
    public float jumpForce = 10f;

    [Tooltip("The CharacterController of the player.")]
    public CharacterController characterController;

    private Vector3 velocity;
    private bool jumping = false;

    public void Start()
    {
        if (characterController == null)
        {
            characterController = this.GetComponent<CharacterController>();
        }
    }

    public void PerformJump(Transform activeHand)
    {
        if (activeHand != null)
        {
            Vector3 jumpDirection = activeHand.forward.normalized;
            jumpDirection.y = Mathf.Max(jumpDirection.y, 0.4f);
            velocity = jumpDirection.normalized * jumpForce;
            jumping = true;
        }
    }

    public void Update()
    {
        if (!jumping)
        {
            return;
        }
        // Apply the jump velocity to the CharacterController
        characterController.Move(velocity * Time.deltaTime);

        // Apply gravity to the jump over time
        velocity.y += Physics.gravity.y * Time.deltaTime;

        // Check for collisions to stop the jump
        if (characterController.isGrounded)
        {
            ResetJump();
        }
    }

    public void ResetJump()
    {
        velocity = Vector3.zero;
        jumping = false;
    }
}