using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class CreatePlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerPrefab;

    GameObject player;


    public override void OnJoinedRoom()
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

        GetComponent<LobbyLocation>().SetPlayer(player);
    }
}
