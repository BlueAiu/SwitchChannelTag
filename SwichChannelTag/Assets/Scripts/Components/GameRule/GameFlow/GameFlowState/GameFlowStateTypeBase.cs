using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームの流れのベースのステート
//新しくステートを作る際はこのクラスを継承すること

public abstract class GameFlowStateTypeBase : MonoBehaviour
{
    /// <summary>
    ///・使い方
    ///ステートを変更する時は_stateMachineのChangeStateから変更(OnEnter、OnExitで呼ばないようにしてください)
    ///sharedDataからステート間で共有するデータを取得・変更可能
    /// </summary>

    [Tooltip("ステートマシン")] [SerializeField]
    protected GameFlowManager _stateMachine;


    public abstract void OnEnter();//ステートの開始処理
    public abstract void OnUpdate();//ステートの毎フレーム処理
    public abstract void OnExit();//ステートの終了処理
}
