using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class CreatePlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playerPrefab;

    public override void OnJoinedRoom()
    {
        var player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

        // ‚±‚ÌŒãplayer‚ÌComponent‚ğ•ÒW‚Å‚«‚é
    }
}
