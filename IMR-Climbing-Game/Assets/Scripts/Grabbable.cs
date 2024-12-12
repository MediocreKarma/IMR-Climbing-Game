using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableObject : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;
    private List<HandGrabController> previousHands, activeHands;

    void Start()
    {
        previousHands = new();
        activeHands = new();
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void FixedUpdate()
    {
        if (interactable == null)
        {
            return;
        }
        foreach (var interactor in interactable.interactorsSelecting)
        {
            if (interactor is not UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor)
            {
                Debug.LogError("Not XRRayInteractor");
                continue;
            }
            if (!(interactor as UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor).TryGetComponent<ActionBasedController>(out var acb))
            {
                continue;
            }
            if (!(acb.model.TryGetComponent<HandGrabController>(out var handGrabController)))
            {
                continue;
            }
            activeHands.Add(handGrabController);
            handGrabController.PerformGrab();
        }

        foreach (var hand in previousHands)
        {
            if (!activeHands.Contains(hand))
            {
                hand.ReleaseGrab();
            }
        }
        previousHands = activeHands;
        activeHands = new();
    }
}