using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーごとのターンの流れのベースのステート
//新しくステートを作る際はこのクラスを継承すること

public　abstract class PlayerTurnFlowStateTypeBase : MonoBehaviour
{
    /// <summary>
    ///・使い方
    ///ステートを変更する時は_stateMachineのChangeStateから変更
    ///sharedDataからステート間で共有するデータを取得・変更可能
    /// </summary>


    public abstract void OnEnter(PlayerTurnFlowManager _stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData);//ステートの開始処理
    public abstract void OnUpdate(PlayerTurnFlowManager _stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData);//ステートの毎フレーム処理
    public abstract void OnExit(PlayerTurnFlowManager _stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData);//ステートの終了処理
}
