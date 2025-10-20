using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マス移動ステート

public class PlayerTurnFlowStateTypeMove : PlayerTurnFlowStateTypeBase
{
    [Tooltip("マップ上を移動する機能")] [SerializeField]
    MoveOnMap _moveOnMap;

    [Tooltip("マス移動時のUIを表示する機能")] [SerializeField]
    ShowUITypeBase _showMoveUI;

    [Tooltip("マス移動時のUIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideMoveUI;

    PlayerTurnFlowManager _stateMachine;
    bool _finished = true;

    //移動を完了して終了ステートに
    public void ToFinish()
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnState.Finish);
    }

    public override void OnEnter(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _finished = false;
        _stateMachine=stateMachine;

        _showMoveUI.Show();

        //マス移動が可能な状態にする
        _moveOnMap.enabled = true;
    }

    public override void OnUpdate(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {

    }

    public override void OnExit(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _finished = true;

        _hideMoveUI.Hide();

        //マス移動が出来ない状態にする
        _moveOnMap.enabled = false;
    }
}
