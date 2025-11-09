using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//お互いのターン終了時のステート

public class GameFlowStateTypeTurnFinish : GameFlowStateTypeBase
{
    [Tooltip("ゲーム終了かを判定する機能")] [SerializeField]
    JudgeGameSet _judgeGameSet;

    public override void OnEnter()
    {
        //ここで経過ターンを増やす
        GameStatsManager.Instance.SetTurn(GameStatsManager.Instance.GetTurn() + 1);
    }

    public override void OnUpdate()
    {
        //ゲーム終了判定をする
        bool isGameSet = _judgeGameSet.IsGameSet();

        //ゲーム終了であれば終了ステートへ
        if(isGameSet)
        {
            _stateMachine.ChangeState(EGameFlowState.Finish);
        }

        //そうでなければプレイヤーにターンを回す

        EGameFlowState nextState = (_stateMachine.SharedData.FirstTurn == EPlayerState.Runner) ? EGameFlowState.RunnerTurn : EGameFlowState.TaggerTurn;

        _stateMachine.ChangeState(nextState);
    }

    public override void OnExit()
    {

    }
}
