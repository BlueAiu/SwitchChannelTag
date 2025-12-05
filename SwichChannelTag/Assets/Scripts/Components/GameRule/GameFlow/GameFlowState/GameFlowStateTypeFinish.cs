using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームの流れの終了のステート

public class GameFlowStateTypeFinish : GameFlowStateTypeBase
{
    [SerializeField] string resultSceneName = "ResultScene";

    [SerializeField] float _delayDuration = 1f;

    public override void OnEnter()//ステートの開始処理
    {
        Debug.Log("ゲーム終了！");

        StartCoroutine(LoadResultScene());
    }

    public override void OnUpdate()//ステートの毎フレーム処理
    {
        
    }

    public override void OnExit()//ステートの終了処理
    {

    }

    IEnumerator LoadResultScene()
    {
        yield return new WaitForSeconds(_delayDuration);

        PhotonNetwork.LoadLevel(resultSceneName);
    }

    private void Awake()
    {
        GameStatsManager.Instance.Winner.OnUpdateWinner += LogWinner;
    }

    void LogWinner(EPlayerState winner)
    {
        Debug.Log("Winner is..." + winner);
    }
}
