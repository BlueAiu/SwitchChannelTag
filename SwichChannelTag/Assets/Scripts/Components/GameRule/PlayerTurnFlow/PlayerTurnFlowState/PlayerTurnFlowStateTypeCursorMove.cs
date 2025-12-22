using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//マス移動ステート

public class PlayerTurnFlowStateTypeCursorMove : PlayerTurnFlowStateTypeBase
{
    [SerializeField]
    DecidePath _decidePath;

    [SerializeField]
    PlayerInput _playerInput;

    [Header("UI関係")]

    [Tooltip("マス移動時のUIを表示する機能")] [SerializeField]
    ShowUITypeBase _showMoveUI;

    [Tooltip("マス移動時のUIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideMoveUI;

    [Header("効果音関係")]

    [Tooltip("決定時に鳴らす効果音")] [SerializeField]
    AudioClip _submitSE;

    [SerializeField]
    AudioSource _audioSource;

    public void ToFinish(InputAction.CallbackContext context)
    {
        if (_stateMachine == null) return;
        if (!context.performed) return;

        _audioSource.PlayOneShot(_submitSE);
        _stateMachine.ChangeState(EPlayerTurnFlowState.MovePlayer);
    }

    public override void OnEnter()
    {
        _showMoveUI.Show();

        //マス移動が可能な状態にする
        _decidePath.OnStart();
        _playerInput.SwitchCurrentActionMap(ActionMapNameDictionary.move);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _hideMoveUI.Hide();

        //マス移動が出来ない状態にする
        _decidePath.OnFinish();
        _playerInput.SwitchCurrentActionMap(ActionMapNameDictionary.unControllable);
    }

    private void Start()
    {
        _hideMoveUI.Hide();
    }
}
