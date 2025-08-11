using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class CreatePlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform WaitingPoint;
    [SerializeField] Vector3 shift;

    GameObject player;


    public override void OnJoinedRoom()
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

        int playerNum = player.GetComponent<PlayerNumber>().PlayerNum;
        Debug.Log(playerNum);
        player.transform.position = WaitingPoint.position + playerNum * shift;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int playerNum = player.GetComponent<PlayerNumber>().PlayerNum;
        player.transform.position = WaitingPoint.position + playerNum * shift;
    }
}
