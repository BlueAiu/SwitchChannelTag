using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームの流れの終了のステート

public class GameFlowStateTypeFinish : GameFlowStateTypeBase
{
    [SerializeField] string resultSceneName = "ResultScene";

    public override void OnEnter()//ステートの開始処理
    {
        Debug.Log("ゲーム終了！");

        PhotonNetwork.LoadLevel(resultSceneName);
    }

    public override void OnUpdate()//ステートの毎フレーム処理
    {
        
    }

    public override void OnExit()//ステートの終了処理
    {

    }
}
