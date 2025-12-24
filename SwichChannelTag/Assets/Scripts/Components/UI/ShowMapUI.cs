using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowMapUI : MonoBehaviour
{
    [SerializeField] GameObject mapUI;
    [SerializeField] InputActionReference mapAction;


    void ShowMap(InputAction.CallbackContext context)
    {
        mapUI.SetActive(true);
    }
    void HideMap(InputAction.CallbackContext context)
    {
        mapUI.SetActive(false);
    }

    private void OnEnable()
    {
        mapAction.action.performed += ShowMap;
        mapAction.action.canceled += HideMap;
        mapAction.action.Enable();
    }

    private void OnDisable()
    {
        mapAction.action.performed -= ShowMap;
        mapAction.action.canceled -= HideMap;
        mapAction.action.Disable();
    }
}
