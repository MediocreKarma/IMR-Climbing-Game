using UnityEngine;

public class HandDirectedJump : MonoBehaviour
{
    [Tooltip("Force applied for the jump.")]
    public float jumpForce = 10f;

    [Tooltip("The Rigidbody of the player.")]
    public Rigidbody playerRigidbody;
    public void PerformJump(Transform activeHand)
    {
        if (activeHand != null && playerRigidbody != null)
        {
            Vector3 jumpDirection = activeHand.forward;
            playerRigidbody.AddForce(jumpDirection.normalized * jumpForce, ForceMode.Impulse);
        }
    }
}