using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideStart : MonoBehaviour
{
    [SerializeField] GameObject Start_button;
    [SerializeField] GameObject Ready_button;
    private const int Limitvalue = 4;
    private int Currentplayer;

    private void Start()
    {
        Start_button.SetActive(false);  
    }

    // Update is called once per frame
    void Update()
    {
        Display_Start();
    }

    void Display_Start()
    {
        Currentplayer = PhotonNetwork.CountOfPlayers;

        //Debug.Log("åªç›" + Currentplayer + "êl");

        if (Currentplayer == Limitvalue)
        {
            Start_button.SetActive(true);
        }
        else
        {
            Start_button.SetActive(false);
        }
    }
}
