using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ReadinessManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string mainSceneName = "MainScene";
    [SerializeField] SceneTransition sceneTransition;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject fallbackButton;
    [SerializeField] int requiredNum = 4;
    [SerializeField] AllPlayersGameEventCompletionWatcher _watcher;


    void FixedUpdate()
    {
        int num = PlayersManager.PlayersGameObject.Length;

        bool canStartGame = num >= requiredNum && IsReadyAll() && PhotonNetwork.IsMasterClient;
        startButton.SetActive(canStartGame);
        if(!EventSystem.current.currentSelectedGameObject.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(fallbackButton);
        }
    }

    public void TryStartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        EventSystem.current.enabled = false;//ボタンを押せなくする

        if (IsReadyAll())
        {
            StartCoroutine(LoadMainScene());
        }
    }

    IEnumerator LoadMainScene()
    {
        GetComponent<JoinControl>().IsRoomOpened = false;

        //初期化処理
        InitGameEvent();

        yield return null;

        //フェードアウト演出
        CallFadeOutEvent();

        yield return null;

        //全員がフェードアウトを完了するまで待つ
        yield return new WaitUntil(()=>_watcher.AreAllPlayersFinished);

        sceneTransition.LoadScene(mainSceneName);
        enabled = false;
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

    void InitGameEvent()
    {
        var gameEventIniters = PlayersManager.GetComponentsFromPlayers<GameEventIniter>();

        foreach (var Initer in gameEventIniters)
        {
            Initer.Init();
        }
    }

    void CallFadeOutEvent()//フェードアウト演出を行うよう全プレイヤーに命令
    {
        var gameEventReceivers = PlayersManager.GetComponentsFromPlayers<GameEventReceiver>();

        foreach(var receiver in gameEventReceivers)
        {
            if (receiver == null) continue;

            receiver.SendEventCall(EGameEvent.FromLobbyToGame);
        }
    }
}
