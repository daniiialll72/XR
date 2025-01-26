using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light light;
    public InputActionReference action;  // VR controller input action
    public Key switchKey = Key.Space;  // Default keyboard key to switch light color

    private Color[] colors = { Color.white, Color.red, Color.green, Color.blue };
    private int currentColorIndex = 0;

    void Start()
    {
        light = GetComponent<Light>();

        // Enable the VR controller action if it's assigned
        if (action != null && action.action != null)
        {
            action.action.Enable();
            action.action.performed += OnActionPerformed;
        }
        else
        {
            Debug.LogWarning("No VR Light Switch Action assigned!");
        }
    }

    void Update()
    {
        // Check for keyboard input to switch light color
        if (Keyboard.current[switchKey].wasPressedThisFrame)
        {
            SwitchLightColor();
        }
    }

    private void OnActionPerformed(InputAction.CallbackContext ctx)
    {
        // Handle VR controller input for light color switch
        SwitchLightColor();
    }

    private void SwitchLightColor()
    {
        // Cycle through the colors
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        light.color = colors[currentColorIndex];
    }

    private void OnDestroy()
    {
        // Clean up by disabling the action when the object is destroyed
        if (action != null && action.action != null)
        {
            action.action.performed -= OnActionPerformed;
            action.action.Disable();
        }
    }
}
