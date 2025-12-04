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

    bool IsTagger { get { return turnSide == EPlayerState.Tagger; } }

    public override void OnEnter()//ステートの開始処理
    {
        _judgeIsTurnFinishAllPlayer.OnEnter(turnSide);

        if (IsTagger) _convertCaughtRunnerToTagger.OnEnter();
    }

    public override void OnUpdate()//ステートの毎フレーム処理
    {
        if (!_judgeIsTurnFinishAllPlayer.IsFinishAll()) return;

        if (IsTagger) _convertCaughtRunnerToTagger.Convert();

        _stateMachine.ChangeState(_judgeNextState.NextState(_stateMachine.SharedData.FirstTurn, turnSide));
    }

    public override void OnExit()//ステートの終了処理
    {
        _judgeIsTurnFinishAllPlayer.OnExit();
        if (IsTagger) _convertCaughtRunnerToTagger.OnExit();
    }
}
