using UnityEngine;
using TMPro;

public class HeadInsideObjectChecker : MonoBehaviour
{
    [SerializeField] private LayerMask objectLayerMask;  // Layer mask for objects to check against
    [SerializeField] private Canvas warningCanvas;       // Canvas to display the warning
    [SerializeField] private TextMeshProUGUI warningText; // TextMeshPro text for the warning message
    [SerializeField] private CanvasGroup fadeCanvasGroup; // CanvasGroup for fade effect
    [SerializeField] private float fadeSpeed = 1f;       // Speed of fade effect

    private bool isHeadInside = false;
    private float targetAlpha = 0f; // Target alpha for the fade effect

    private void Start()
    {
        // Ensure the Canvas and fade are set up properly
        warningCanvas.enabled = false;
        fadeCanvasGroup.alpha = 0f;
    }

    private void Update()
    {
        CheckHeadInside();
        HandleFadeEffect();
    }

    private void CheckHeadInside()
    {
        bool isColliding = Physics.CheckSphere(transform.position, 0.001f, objectLayerMask);
        if (isColliding && !isHeadInside)
        {
            // Head entered an object
            isHeadInside = true;
            warningCanvas.enabled = true;
            targetAlpha = 1f; // Fade to black
        }
        else if (!isColliding && isHeadInside)
        {
            // Head exited the object
            isHeadInside = false;
            warningCanvas.enabled = false;
            targetAlpha = 0f; // Fade back to normal
        }
    }

    private void HandleFadeEffect()
    {
        // Smoothly adjust the alpha of the fade overlay
        fadeCanvasGroup.alpha = Mathf.Lerp(fadeCanvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
    }
}