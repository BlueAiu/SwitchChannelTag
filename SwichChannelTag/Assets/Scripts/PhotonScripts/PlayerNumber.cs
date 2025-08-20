using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumber : MonoBehaviour
{
    Photon.Realtime.Player player = null;
    List<Photon.Realtime.Player> playerList = new();

    public int PlayerNum
    {
        get 
        {
            if (player == null) player = PhotonNetwork.LocalPlayer;
            SetPlayerList();
            return playerList.IndexOf(player);
        }
    }

    void SetPlayerList()
    {
        playerList.Clear();

        var playerArr = PhotonNetwork.PlayerList;
        foreach(var i in  playerArr)
        {
            playerList.Add(i);
        }
    }
}
