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
        
    }

    public override void OnUpdate()//すぐにステート遷移されるので、実質的にはこのステートに入るたびに1度しか呼ばれない
    {
        //タイムアップでゲーム終了
        bool isGameSet = _judgeGameSet.IsTimeUp(out EPlayerState winner);

        //ゲーム終了であれば終了ステートへ
        if (isGameSet)
        {
            GameStatsManager.Instance.Winner.SetWinner(winner);//ゲームの統計情報にどちらの勝利かを書き込む
            _stateMachine.ChangeState(EGameFlowState.Finish);
            return;
        }

        //そうでなければプレイヤーにターンを回す
        GameStatsManager.Instance.Turn.SetTurn(GameStatsManager.Instance.Turn.GetTurn() + 1);//経過ターンを増やす

        EGameFlowState nextState = (_stateMachine.SharedData.FirstTurn == EPlayerState.Runner) ? EGameFlowState.RunnerTurn : EGameFlowState.TaggerTurn;

        _stateMachine.ChangeState(nextState);
    }

    public override void OnExit()
    {

    }
}
