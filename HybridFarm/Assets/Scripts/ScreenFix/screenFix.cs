using UnityEngine;
using UnityEngine.UI; // Required for Button
using UnityEngine.UIElements;

public class screenFix : MonoBehaviour
{
    public CanvasScaler canvasScaler; // Reference to the Canvas Scaler component
    public UnityEngine.UI.Button toggleButton; // Reference to the UI Button
    private bool matchWidth = true; // Track current state (true = match width, false = match height)

    void Start()
    {
        // Ensure the Canvas Scaler is assigned
        if (canvasScaler == null)
        {
            canvasScaler = GetComponent<CanvasScaler>();
            if (canvasScaler == null)
            {
                Debug.LogError("CanvasScaler not found! Please assign it in the Inspector.");
            }
        }

        // Ensure the Button is assigned and add listener
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleScreenMatchMode);
        }
        else
        {
            Debug.LogError("Button not assigned! Please assign the toggle button in the Inspector.");
        }

        // Initialize Canvas Scaler settings
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080); // Set to your reference resolution
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = matchWidth ? 0f : 1f; // Start with Match Width (0)
    }

    void ToggleScreenMatchMode()
    {
        // Toggle between Match Width (0) and Match Height (1)
        matchWidth = !matchWidth;
        canvasScaler.matchWidthOrHeight = matchWidth ? 0f : 1f;
        Debug.Log($"Screen Match Mode set to: {(matchWidth ? "Match Width" : "Match Height")}");
    }
}