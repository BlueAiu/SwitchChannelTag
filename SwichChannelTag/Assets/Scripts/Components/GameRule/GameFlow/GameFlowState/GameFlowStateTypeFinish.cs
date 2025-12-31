using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームの流れの終了のステート

public class GameFlowStateTypeFinish : GameFlowStateTypeBase
{
    [SerializeField] string resultSceneName = "ResultScene";

    [SerializeField]
    AllPlayersGameEventCompletionWatcher _watcher;

    GameEventReceiver[] _receivers;

    public override void OnEnter()//ステートの開始処理
    {
        StartCoroutine(FinishEventCoroutine());
    }

    public override void OnUpdate()//ステートの毎フレーム処理
    {
        
    }

    public override void OnExit()//ステートの終了処理
    {

    }

    IEnumerator FinishEventCoroutine()
    {
        CallFinishEvent();

        yield return null;

        //全員のゲームイベント処理が終わるまで待つ
        yield return new WaitUntil(()=> _watcher.AreAllPlayersFinished);

        PhotonNetwork.LoadLevel(resultSceneName);//シーン遷移
    }

    private void Awake()
    {
        _receivers = PlayersManager.GetComponentsFromPlayers<GameEventReceiver>();
    }

    private void OnEnable()
    {
        GameStatsManager.Instance.Winner.OnUpdateWinner += LogWinner;
    }

    private void OnDisable()
    {
        GameStatsManager.Instance.Winner.OnUpdateWinner -= LogWinner;
    }

    void LogWinner(EPlayerState winner)
    {
        Debug.Log("Winner is..." + winner);
    }

    void CallFinishEvent()
    {
        foreach (var receiver in _receivers)
        {
            if (receiver == null) continue;

            receiver.SendEventCall(EGameEvent.GameFinish);
        }
    }
}
