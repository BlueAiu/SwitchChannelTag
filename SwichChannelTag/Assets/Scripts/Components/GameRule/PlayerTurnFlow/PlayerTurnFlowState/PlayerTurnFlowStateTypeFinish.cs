using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//行動を終えた後の状態

public class PlayerTurnFlowStateTypeFinish : PlayerTurnFlowStateTypeBase
{
    [Tooltip("ターン終了時のUIを表示する機能")] [SerializeField]
    ShowUITypeBase _showFinishUI;

    [Tooltip("ターン終了時のUIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideFinishUI;

    TurnIsReady _myTurnIsReady;

    public override void OnEnter()
    {
        _showFinishUI.Show();
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _hideFinishUI.Hide();
        _myTurnIsReady.IsReady = true;//行動終了したことを知らせる
        _stateMachine.SharedData.Reset();//共有データをリセット
    }

    private void Awake()
    {
        _myTurnIsReady = PlayersManager.GetComponentFromMinePlayer<TurnIsReady>();
    }

    private void Start()
    {
        _hideFinishUI.Hide();//シーン開始時にUIを隠す
    }
}
