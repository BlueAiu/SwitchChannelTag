using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowStateTypeTurn : GameFlowStateTypeBase
{
    [SerializeField] EPlayerState turnSide;

    [Tooltip("次のステートを判断する機能")] [SerializeField]
    JudgeNextState_Turn _judgeNextState;

    [Tooltip("全プレイヤーの行動が終了したかを判断する機能")] [SerializeField]
    JudgeIsTurnFinishAllPlayer _judgeIsTurnFinishAllPlayer;

    [Tooltip("逃げ→鬼に変える機能")] [SerializeField]
    ConvertCaughtRunnerToTagger _convertCaughtRunnerToTagger;

    [SerializeField]
    AllPlayersGameEventCompletionWatcher _watcher;

    GameEventReceiver[] _receivers;

    bool IsTagger { get { return turnSide == EPlayerState.Tagger; } }

    public override void OnEnter()//ステートの開始処理
    {
        _judgeIsTurnFinishAllPlayer.OnEnter(turnSide);

        if (IsTagger) _convertCaughtRunnerToTagger.OnEnter();

        StartCoroutine(TurnCoroutine());
    }

    public override void OnUpdate()//ステートの毎フレーム処理
    {

    }

    public override void OnExit()//ステートの終了処理
    {
        _judgeIsTurnFinishAllPlayer.OnExit();
        if (IsTagger) _convertCaughtRunnerToTagger.OnExit();
    }

    IEnumerator TurnCoroutine()
    {
        //全員の行動が終わるまで待つ
        yield return new WaitUntil(()=> _judgeIsTurnFinishAllPlayer.IsFinishAll());

        if (IsTagger) _convertCaughtRunnerToTagger.Convert();//捕まった逃げを鬼に変える

        //全員にゲームイベントを命令
        EGameEvent gameEvent = IsTagger ? EGameEvent.TaggerTurn : EGameEvent.RunnerTurn;
        CallTurnEvent(gameEvent);

        yield return null;

        //全員のゲームイベント処理が終わるまで待つ
        yield return new WaitUntil(() => _watcher.AreAllPlayersFinished);

        _stateMachine.ChangeState(_judgeNextState.NextState(_stateMachine.SharedData.FirstTurn, turnSide));
    }

    void CallTurnEvent(EGameEvent gameEvent)
    {
        foreach (var receiver in _receivers)
        {
            if (receiver == null) continue;

            receiver.SendEventCall(gameEvent);
        }
    }

    private void Awake()
    {
        _receivers = PlayersManager.GetComponentsFromPlayers<GameEventReceiver>();
    }
}
