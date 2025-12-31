using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームの流れのスタート演出のステート

public class GameFlowStateTypeStart : GameFlowStateTypeBase
{
    [SerializeField]
    AllPlayersGameEventCompletionWatcher _watcher;

    GameEventReceiver[] _receivers;

    public override void OnEnter()//ステートの開始処理
    {
        CallStartEvent();
    }

    public override void OnUpdate()//ステートの毎フレーム処理
    {
        //初期化処理が終わるまで待つ
        if (!CheckIsInitManager.Instance.GetIsInited()) return;

        //全員のスタート演出が終わるまで待つ
        if (!_watcher.AreAllPlayersFinished) return;

        //ターンを回す
        EGameFlowState nextState = (_stateMachine.SharedData.FirstTurn == EPlayerState.Runner) ? EGameFlowState.RunnerTurn : EGameFlowState.TaggerTurn;

        _stateMachine.ChangeState(nextState);
    }

    public override void OnExit()//ステートの終了処理
    {

    }

    private void Awake()
    {
        _receivers = PlayersManager.GetComponentsFromPlayers<GameEventReceiver>();
    }

    void CallStartEvent()
    {
        foreach (var receiver in _receivers)
        {
            if (receiver == null) continue;

            receiver.SendEventCall(EGameEvent.GameStart);
        }
    }
}
