using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumber : MonoBehaviour
{
    const int masterNum = 1;
    Photon.Realtime.Player player;

    public int PlayerNum
    {
        get 
        {
            if (player == null) SetLocalPlayer();
            return player.ActorNumber - masterNum;
        }
        //set;
    }

    void SetLocalPlayer()
    {
        player = PhotonNetwork.LocalPlayer;
    }
}
