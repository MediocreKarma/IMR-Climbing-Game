using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;

public class GrabbableObject : MonoBehaviour
{
    [Tooltip("The object associated with the left controller of the XR Rig"),]
    public HandGrabController leftHandController;
    
    [Tooltip("The object associated with the right controller of the XR Rig")]
    public HandGrabController rightHandController;

    [Tooltip("The transform associated with the left controller of the XR Rig")]
    public Transform leftHandTransform;

    [Tooltip("The transform associated with the right controller of the XR Rig")]
    public Transform rightHandTransform;

    [Tooltip("The Jumper used by the player")]
    public HandDirectedJump jumper;

    [Tooltip("The CharacterController of the player.")]
    public CharacterController characterController;

    private ClimbInteractable climbInteractable = null;
    private readonly bool[] previouslyGrabbed  = { false, false };
    private readonly bool[] activelyGrabbing   = { false, false };
    private readonly HandGrabController[] controllers = { null, null };
    private readonly Transform[] transforms = { null, null };
    private float radius;

    void Start()
    {
        climbInteractable = GetComponent<ClimbInteractable>();
        controllers[0] = leftHandController;
        controllers[1] = rightHandController;
        transforms[0] = leftHandTransform;
        transforms[1] = rightHandTransform;
        radius = characterController.radius;
    }

    private void HandleGrabbingStateChange(int index)
    {
        if (!controllers[index])
        {
            return;
        }
        if (!activelyGrabbing[index] && previouslyGrabbed[index]) 
        {
            Debug.Log("Releasing grab");
            controllers[index].ReleaseGrab();
        }
        else if (activelyGrabbing[index] && !previouslyGrabbed[index])
        {
            Debug.Log("Performing grab");
            controllers[index].PerformGrab();
        }
    }

    private void HandleJumpingStateChange() 
    {
        if (!activelyGrabbing[0] && !activelyGrabbing[1] && previouslyGrabbed[0] ^ previouslyGrabbed[1])
        {
            for (int i = 0; i < 2; i++)
            {
                if (previouslyGrabbed[i])
                {
                    jumper.PerformJump(transforms[1 - i]);
                }
            }
        }
        if (activelyGrabbing[0] || activelyGrabbing[1])
        {
            jumper.ResetJump();
        }
    }

    private void FixedUpdate()
    {
        activelyGrabbing[0] = XRSelectInteractableExtensions.IsSelectedByLeft(climbInteractable);
        activelyGrabbing[1] = XRSelectInteractableExtensions.IsSelectedByRight(climbInteractable);
        if (activelyGrabbing[0] || activelyGrabbing[1] || jumper.IsJumping) 
        {
            characterController.radius = 0.01F;
        }
        else
        {
            characterController.radius = this.radius;
        }
        HandleGrabbingStateChange(0);
        HandleGrabbingStateChange(1);

        HandleJumpingStateChange();
        previouslyGrabbed[0] = activelyGrabbing[0];
        previouslyGrabbed[1] = activelyGrabbing[1];
    }
}