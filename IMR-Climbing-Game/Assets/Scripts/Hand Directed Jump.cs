using System.Linq;
using UnityEngine;

public class HandDirectedJump : MonoBehaviour
{
    [Tooltip("Force applied for the jump.")]
    public float jumpForce = 10f;

    [Tooltip("The CharacterController of the player.")]
    public CharacterController characterController;

    private Vector3 velocity;
    private bool jumping = false;

    public bool IsSliding { get; set; }

    public bool IsJumping
    {
        get { return jumping && IsSliding; }
    }

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

    private float groundedTime = 0f;
    private const float groundedThreshold = 0.1f; // 100 milliseconds

    public void Update()
    {
        if (!jumping)
        {
            return;
        }
        characterController.Move(velocity * Time.deltaTime);
        velocity.y += Physics.gravity.y * Time.deltaTime;
        if (characterController.isGrounded && !IsSliding)
        {
            groundedTime += Time.deltaTime;
            if (groundedTime >= groundedThreshold)
            {
                ResetJump();
            }
        }
        else
        {
            groundedTime = 0f; // Reset timer if the player is not continuously grounded
        }
    }

    public void ResetJump()
    {
        velocity = Vector3.zero;
        jumping = false;
    }
}