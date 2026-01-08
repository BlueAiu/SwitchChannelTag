using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ResultSequence : MonoBehaviour
{
    [SerializeField] GameObject graph;
    [SerializeField] GameObject titleButton;
    [SerializeField] InputActionReference inputAction;

    [SerializeField] float autoTakeTime = 3f;

    bool graphActive = false;
    bool buttonPressed = false;

    void Start()
    {
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(autoTakeTime);
        yield return StartCoroutine(GraphEnable());
    }

    IEnumerator GraphEnable()
    {
        if (graphActive) yield break;

        graphActive = true;
        graph.SetActive(true);
        titleButton.SetActive(true);

        yield return null;
        EventSystem.current.SetSelectedGameObject(titleButton);
    }

    void SkipAction(InputAction.CallbackContext context)
        => StartCoroutine(GraphEnable());

    private void OnEnable()
    {
        inputAction.action.performed += SkipAction;
        inputAction.action.Enable();
    }

    private void OnDisable()
    {
        inputAction.action.performed -= SkipAction;
        inputAction.action.Disable();
    }
}
