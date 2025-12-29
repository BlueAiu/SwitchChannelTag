using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventIniter : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    public void Init()
    {
        _myPhotonView.RPC(nameof(InitRPC), RpcTarget.All);
    }

    [PunRPC]
    void InitRPC()
    {
        if (!_myPhotonView.IsMine) return;

        GameObject gameEventManager = GameObject.FindWithTag(TagDictionary.gameEventManager);

        if (gameEventManager == null) return;
        
        var watcher = gameEventManager.GetComponent<AllPlayersGameEventCompletionWatcher>();
        var gameEventPlayerManager = gameEventManager.GetComponent<GameEventPlayerManager>();

        if (watcher == null || gameEventPlayerManager==null) return;

        watcher.UpdateReceivers();
        gameEventPlayerManager.UpdateMyReceiver();
    }

}
