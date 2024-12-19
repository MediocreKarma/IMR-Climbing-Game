using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;

public class GrabbableObject : MonoBehaviour
{
    [Tooltip("The object associated with the left controller of the XR Rig"),]
    public HandGrabController leftController;
    
    [Tooltip("The object associated with the right controller of the XR Rig")]
    public HandGrabController rightController;

    [Tooltip("The Jump")]
    public HandDirectedJump jumper;

    private ClimbInteractable climbInteractable = null;
    private readonly bool[] previouslyGrabbed  = { false, false };
    private readonly bool[] activelyGrabbing   = { false, false };
    private readonly HandGrabController[] controllers = { null, null };

    void Start()
    {
        climbInteractable = GetComponent<ClimbInteractable>();
        controllers[0] = leftController;
        controllers[1] = rightController;
    }

    private void HandleGrabbingStateChange(int index)
    {
        if (!controllers[index])
        {
            return;
        }
        if (!activelyGrabbing[index] && previouslyGrabbed[index]) 
        {
            controllers[index].ReleaseGrab();
        }
        else if (activelyGrabbing[index] && !previouslyGrabbed[index])
        {
            controllers[index].PerformGrab();
        }
    }

    private void HandleJumpingStateChange() 
    {
        if (!activelyGrabbing[0] && !activelyGrabbing[1] && previouslyGrabbed[0] ^ previouslyGrabbed[1])
        {
            for (int i = 0; i < controllers.Length; i++)
            {
                if (previouslyGrabbed[i])
                {
                    jumper.PerformJump(controllers[1 - i].transform);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        activelyGrabbing[0] = XRSelectInteractableExtensions.IsSelectedByLeft(climbInteractable);
        activelyGrabbing[1] = XRSelectInteractableExtensions.IsSelectedByRight(climbInteractable);
        HandleGrabbingStateChange(0);
        HandleGrabbingStateChange(1);

        HandleJumpingStateChange();
        previouslyGrabbed[0] = activelyGrabbing[0];
        previouslyGrabbed[1] = activelyGrabbing[1];
    }
}