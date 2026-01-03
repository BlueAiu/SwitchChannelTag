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

    [SerializeField] SetCaughtRunnerInfo _setCaughtRunnerInfo;

    PlayerTurnCommunicator _myTurnCommunicator;


    public override void OnEnter()
    {
        _showFinishUI.Show();

        _myTurnCommunicator.FinishedTurn();//行動終了したことを知らせる
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _setCaughtRunnerInfo.Clear();
        _hideFinishUI.Hide();
        _stateMachine.SharedData.Reset();//共有データをリセット
    }

    private void Awake()
    {
        _myTurnCommunicator = PlayersManager.GetComponentFromMinePlayer<PlayerTurnCommunicator>();
    }

    private void Start()
    {
        _hideFinishUI.Hide();//シーン開始時にUIを隠す
    }
}
