using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideStart : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject Start_button;
    [SerializeField] GameObject Ready_button;
    [SerializeField] int Limitvalue = 4;
    private int Currentplayer;

    ReadyButton readyButton;

    private void Start()
    {
        Start_button.SetActive(false); 
        Ready_button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Display_Start();
    }

    void Display_Start()
    {
        Currentplayer = PhotonNetwork.CountOfPlayers;

        //Debug.Log("Œ»Ý" + Currentplayer + "l");

        if (Currentplayer >= Limitvalue)
        {
            Start_button.SetActive(true);
        }
        else
        {
            Start_button.SetActive(false);
        }
    }

    public override void OnJoinedRoom()
    {
        Ready_button.SetActive(true);
    }
}
