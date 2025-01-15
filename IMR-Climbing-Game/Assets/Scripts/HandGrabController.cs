using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class HandGrabController : MonoBehaviour
{
    private Animator animator;
    private bool isGrabbing = false;

    public Vector3 rotationAngle = Vector3.zero;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PerformGrab()
    {
        if (animator && !isGrabbing)
        {
            animator.SetTrigger("GrabTrigger");
            isGrabbing = true;
        }
    }

    public void ReleaseGrab()
    {
        if (animator && isGrabbing)
        {
            animator.SetTrigger("UngrabTrigger");
            isGrabbing = false;
        }
    }

    // maybe a "Pointing" gesture to show direction of jump should be added here
}