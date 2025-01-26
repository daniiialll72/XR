using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitKey : MonoBehaviour
{
    // For keyboard input
    public Key quitKey = Key.Escape;  // Default to Escape key

    // For VR Controller input (can be any VR button you want, e.g., Trigger, Menu button, etc.)
    public InputActionReference quitAction; 

    private void Start()
    {
        // Enable the VR quit action if it's assigned
        if (quitAction != null && quitAction.action != null)
        {
            quitAction.action.Enable();
            quitAction.action.performed += OnQuitActionPerformed;
        }
        else
        {
            Debug.LogWarning("No VR Quit Action assigned!");
        }
    }

    private void Update()
    {
        // Keyboard input check
        if (Keyboard.current[quitKey].wasPressedThisFrame)
        {
            QuitGame();
        }
    }

    private void OnQuitActionPerformed(InputAction.CallbackContext ctx)
    {
        QuitGame();
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stop Play Mode in the Unity Editor
#else
        Application.Quit();  // Quit the game in the build
#endif
    }

    private void OnDestroy()
    {
        // Clean up by disabling the VR action when the object is destroyed
        if (quitAction != null && quitAction.action != null)
        {
            quitAction.action.performed -= OnQuitActionPerformed;
            quitAction.action.Disable();
        }
    }
}
