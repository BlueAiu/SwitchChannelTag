using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AdvancePage : MonoBehaviour
{
    [SerializeField] GameObject[] Panels;

    private int Currentpage;
    // Start is called before the first frame update
    void Start()
    {
        Currentpage = 0;
    }

    private void Advance_page(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Panels[Currentpage].SetActive(false);

            Currentpage++;

            if (Currentpage >= Panels.Length - 1)
            {
                Currentpage = 0;
            }
        }
    }


}
