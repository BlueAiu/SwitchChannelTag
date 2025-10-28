using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//お互いのターン終了時のステート

public class GameFlowStateTypeTurnFinish : GameFlowStateTypeBase
{
    public override void OnEnter()
    {
        //ここで経過ターンを増やす
        GameStatsManager.Instance.SetTurn(GameStatsManager.Instance.GetTurn() + 1);
    }

    public override void OnUpdate()
    {
        //指定ターン経過したらゲーム終了させる
        //そうでなければプレイヤーにターンを回す

        EGameState nextState = (_stateMachine.SharedData.FirstTurn == EPlayerState.Runner) ? EGameState.RunnerTurn : EGameState.TaggerTurn;

        _stateMachine.ChangeState(nextState);
    }

    public override void OnExit()
    {

    }
}
