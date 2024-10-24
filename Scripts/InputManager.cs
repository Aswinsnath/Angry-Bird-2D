/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput playerInput;

    private InputAction mousepositionAction;
    private InputAction mouseAction;

    public static Vector2 MousePosition;

    public static bool wasLKefMouseButtonPressed;
    public static bool wasLKefMouseButtonReleased;
    public static bool isLeftMousePressed;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        mousepositionAction = playerInput.actions["MousePosition"];
        mouseAction = playerInput.actions["Mouse"];
    }

    private void Update()
    {
        MousePosition =mousepositionAction.ReadValue<Vector2>();

        wasLKefMouseButtonPressed = mouseAction.WasPerformedThisFrame();

        isLeftMousePressed = mouseAction.IsPressed();
    }
}
*/