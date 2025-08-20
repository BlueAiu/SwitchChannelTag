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

    public bool IsRoomOpened
    {
        get => PhotonNetwork.CurrentRoom.IsOpen;
        set
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CurrentRoom.IsOpen = value;
            }
        }
    }
}
