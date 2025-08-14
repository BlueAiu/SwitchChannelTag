using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class GettingReady : MonoBehaviourPunCallbacks
{
    bool isReady = false;
    public bool IsReady 
    { 
        get => isReady;
        private set { photonView.RPC(nameof(SetIsReady), RpcTarget.All, value); }
    }


    public void SwitchReady()
    {
        IsReady = !IsReady;
    }

    //新たに参加者が増えた時、値を再同期させる
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        photonView.RPC(nameof(SetIsReady), RpcTarget.All, IsReady);
    }

    [PunRPC]
    void SetIsReady(bool value)
    {
        isReady = value;
    }
}
