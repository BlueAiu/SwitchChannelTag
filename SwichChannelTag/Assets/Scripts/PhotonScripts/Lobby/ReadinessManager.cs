using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ReadinessManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string mainSceneName = "MainScene";
    [SerializeField] SceneTransition sceneTransition;
    [SerializeField] GameObject startButton;
    [SerializeField] int requiredNum = 4;


    void FixedUpdate()
    {
        int num = PlayersManager.PlayersGameObject.Length;

        bool canStartGame = num >= requiredNum && IsReadyAll() && PhotonNetwork.IsMasterClient;
        startButton.SetActive(canStartGame);
    }

    public void TryStartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (IsReadyAll())
        {
            sceneTransition.LoadScene(mainSceneName);
            GetComponent<JoinControl>().IsRoomOpened = false;
            enabled = false;
        }
    }

    bool IsReadyAll()
    {
        var playerReadis = PlayersManager.GetComponentsFromPlayers<GettingReady>();

        foreach (var i in playerReadis)
        {
            if (!i.IsReady) return false;
        }

        return true;
    }
}
