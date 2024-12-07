using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class HandGrabController : MonoBehaviour
{
    private Animator AnimatorComponent { get; set; }
    private bool isGrabbing = false;

    public Vector3 rotationAngle = Vector3.zero;

    private void Start()
    {
        AnimatorComponent = GetComponent<Animator>();
    }

    public void PerformGrab()
    {
        if (!isGrabbing)
        {
            AnimatorComponent.SetTrigger("Grab");
            transform.Rotate(rotationAngle);
            isGrabbing = true;
        }
    }

    public void ReleaseGrab()
    {
        if (isGrabbing)
        {
            AnimatorComponent.SetTrigger("GrabbingToIdleTrigger");
            transform.Rotate(-rotationAngle);
            isGrabbing = false;
        }
    }

    // maybe a "Pointing" gesture to show direction of jump should be added here
}