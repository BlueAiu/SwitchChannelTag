using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームの流れのスタート演出のステート

public class GameFlowStateTypeStart : GameFlowStateTypeBase
{
    public override void OnEnter()//ステートの開始処理
    {
       
    }

    public override void OnUpdate()//ステートの毎フレーム処理
    {
        //後で何か演出を挟む
        _stateMachine.ChangeState(EGameState.RunnerTurn);//最初は逃げ側のターン
    }

    public override void OnExit()//ステートの終了処理
    {

    }
}
