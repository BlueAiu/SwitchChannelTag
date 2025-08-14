using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyLocation : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform WaitingPoint;
    [SerializeField] Vector3 shift;

    Transform playerTransform;
    PlayerNumber playerNumber;

    public void SetPlayer(GameObject player)
    {
        playerTransform = player.transform;
        playerNumber = player.GetComponent<PlayerNumber>();

        SetLocation();
    }

    void SetLocation()
    {
        int playerNum = playerNumber.PlayerNum;
        playerTransform.position = WaitingPoint.position + playerNum * shift;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        SetLocation();
    }
}
