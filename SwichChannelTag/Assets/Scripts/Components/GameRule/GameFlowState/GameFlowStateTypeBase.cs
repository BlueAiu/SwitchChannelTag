using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームの流れのベースのステート

public abstract class GameFlowStateTypeBase : MonoBehaviour
{
    /// <summary>
    ///・使い方
    ///ステートを終了したい際は、OnEnterかOnUpdate内で_finishedをtrueにすればステートから抜け出せる
    ///ステートの開始(OnEnterが呼ばれる)時に_finishedをfalseにすることを推奨する
    /// </summary>
    protected bool _finished=false;

    public bool Finished { get { return _finished; } }


    public abstract void OnEnter();//ステートの開始処理
    public abstract void OnUpdate();//ステートの毎フレーム処理
    public abstract void OnExit();//ステートの終了処理
}
