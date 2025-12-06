using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//マス移動ステート

public class PlayerTurnFlowStateTypeCursorMove : PlayerTurnFlowStateTypeBase
{
    [SerializeField]
    MoveOnMap _moveOnMap;

    [SerializeField]
    PlayerInput _playerInput;

    [Tooltip("マス移動時のUIを表示する機能")] [SerializeField]
    ShowUITypeBase _showMoveUI;

    [Tooltip("マス移動時のUIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideMoveUI;

    bool _finished = true;

    public void ToFinish()
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnFlowState.MovePlayer);
    }

    public override void OnEnter()
    {
        _finished = false;

        _showMoveUI.Show();

        //マス移動が可能な状態にする
        _moveOnMap.OnStart();
        _playerInput.SwitchCurrentActionMap(ActionMapNameDictionary.move);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _finished = true;

        _hideMoveUI.Hide();

        //マス移動が出来ない状態にする
        _moveOnMap.OnFinish();
        _playerInput.SwitchCurrentActionMap(ActionMapNameDictionary.unControllable);
    }

    private void Start()
    {
        _hideMoveUI.Hide();
    }
}
