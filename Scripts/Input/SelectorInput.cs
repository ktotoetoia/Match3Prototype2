using Zenject;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectorInput : MonoBehaviour
{
    [Inject] private IPieceContainerMover containerMover;

    private MouseInput input;

    private void Start()
    {
        InstantiateInput();
    }

    private void InstantiateInput()
    {
        input = new MouseInput();

        InputAction selectAction = input.Mouse.Select;

        selectAction.started += OnSelectStarted;
        selectAction.canceled += OnSelectCanceled;

        selectAction.Enable();
    }

    private void OnSelectStarted(InputAction.CallbackContext context)
    {
        containerMover.OnSelect();
    }

    private void OnSelectCanceled(InputAction.CallbackContext context)
    {
        containerMover.OnDeselect();
    }
}