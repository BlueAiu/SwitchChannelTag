using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class CreatePlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform WaitingPoint;
    [SerializeField] Vector3 shift;

    const int masterNum = 1;

    public override void OnJoinedRoom()
    {
        var player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

        int playerNum = player.GetComponent<PlayerNumber>().PlayerNum;
        player.transform.position = WaitingPoint.position + (playerNum - masterNum) * shift;
    }
}
