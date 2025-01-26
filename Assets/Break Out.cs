using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    public InputActionReference action; // VR controller input action
    public Transform roomTransform; // Reference to the room's transform
    public Transform externalViewTransform; // Reference to the external viewing point's transform
    public Key teleportKey = Key.T; // Default keyboard key for teleportation

    private bool isPlayerInRoom = true;

    // Start is called before the first frame update
    void Start()
    {
        // Enable the VR controller input action
        if (action != null && action.action != null)
        {
            action.action.Enable();
            action.action.performed += (ctx) => Teleport(); // Trigger teleportation via VR controller action
        }
        else
        {
            Debug.LogWarning("No VR Input Action assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the teleport key (default: 'T') is pressed on the keyboard
        if (Keyboard.current[teleportKey].wasPressedThisFrame)
        {
            Teleport(); // Trigger teleportation via keyboard input
        }
    }

    // Teleport between the two points (room and external view)
    void Teleport()
    {
        if (isPlayerInRoom)
        {
            transform.position = externalViewTransform.position; // Move to external view
        }
        else
        {
            transform.position = roomTransform.position; // Move to room
        }

        isPlayerInRoom = !isPlayerInRoom; // Toggle the state (in room / external view)
    }

    // Cleanup the input action when the object is destroyed
    private void OnDestroy()
    {
        if (action != null && action.action != null)
        {
            action.action.performed -= (ctx) => Teleport();
            action.action.Disable();
        }
    }
}
