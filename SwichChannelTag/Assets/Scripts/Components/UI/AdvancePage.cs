using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AdvancePage : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] sprites;

    DefaultInputActions inputActions;

    public int index = 0;
    private int Max_index;

    [SerializeField] float Cool_time = 0.2f;
    private float Lastmove_Time;

    private void Awake()
    {
        inputActions = new DefaultInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {

        index = 0;
        Max_index = sprites.Length - 1;

        image.sprite = sprites[index];
    }

    public void Next_page()
    {
        if(index >= Max_index)
        {
            index = 0;
        }
        else
        {
            index++;
        }

        image.sprite = sprites[index];
    }

    public void Back_page()
    {
        if (index == 0)
        {
            index = Max_index;
        }
        else
        {
            index--;
        }

        Debug.Log("sprite num is :" + index);
        image.sprite = sprites[index];
    }

    private void OnSubmit(InputAction.CallbackContext context)
    { 
        gameObject.SetActive(false);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();

        if(move.x > 0.5f)
        {
            Next_page();
            Lastmove_Time = Time.time;
        }
        else if(move.x < -0.5f)
        {
            Back_page();
            Lastmove_Time = Time.time;
        }
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Navigate.performed += OnMove;
        inputActions.UI.Submit.performed += OnSubmit;
    }

    private void OnDisable()
    {
        inputActions.UI.Navigate.performed -= OnMove;
        inputActions.UI.Submit.performed -= OnSubmit;
        inputActions.UI.Disable();
    }


}
