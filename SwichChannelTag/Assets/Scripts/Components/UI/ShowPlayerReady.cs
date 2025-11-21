using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerReady : MonoBehaviour
{
    [SerializeField] GameObject Ready_Massage;
    // Start is called before the first frame update
    private void Start()
    {
        Ready_Massage.SetActive(false);
    }

    public void Show_ReadyMassage()
    {
        Ready_Massage.SetActive(true);
    }

    public void Hide_ReadyMassage()
    {
        Ready_Massage.SetActive(false);
    }
}
