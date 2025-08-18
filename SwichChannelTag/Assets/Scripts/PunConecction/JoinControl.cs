using Photon.Pun;
using UnityEngine;

public class JoinControl : MonoBehaviourPunCallbacks
{
    [SerializeField] int maxPlayer;

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.MaxPlayers = maxPlayer;
        }
    }
}
