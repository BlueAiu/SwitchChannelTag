using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AdvancePage : MonoBehaviour
{
    [SerializeField] GameObject[] Panels;
    DefaultInputActions inputActions;

    public int Currentpage = 0;
    private int Maxpage;

    [SerializeField] float Cool_time = 0.2f;
    private float Lastmove_Time;

    private void Awake()
    {
        inputActions = new DefaultInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Panels.Length; i++)
        {
            if (Panels[i] != null)
            {
                Panels[i].SetActive(false);
            }
        }

        Maxpage = Panels.Length - 1;
    }

    public void Next_page()
    {
        Panels[Currentpage]?.SetActive(false);

        if(Currentpage >= Maxpage)
        {
            Currentpage = 0;
        }
        else
        {
            Currentpage++;
        }

        Panels[Currentpage]?.SetActive(true);

    }

    public void Back_page()
    {
        Panels[Currentpage]?.SetActive(false);

        if (Currentpage <= 0)
        {
            Currentpage = Maxpage;
        }
        else
        {
            Currentpage--;
        }

        Panels[Currentpage]?.SetActive(true);
    }

    private void OnSubmit(InputAction.CallbackContext context)
    { 
        Panels[Currentpage]?.SetActive(false);
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
