using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideStart : MonoBehaviour
{
    [SerializeField] GameObject Start_button;
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
        Currentplayer = PhotonNetwork.CountOfPlayersInRooms;

        if(Currentplayer == Limitvalue)
        {
            Start_button.SetActive(true);
        }
        else
        {
            Start_button.SetActive(false);
        }
    }
}
