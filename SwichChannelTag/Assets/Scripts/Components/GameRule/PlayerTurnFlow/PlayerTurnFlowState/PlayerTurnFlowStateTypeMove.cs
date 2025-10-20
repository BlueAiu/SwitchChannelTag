using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�}�X�ړ��X�e�[�g

public class PlayerTurnFlowStateTypeMove : PlayerTurnFlowStateTypeBase
{
    [Tooltip("�}�b�v����ړ�����@�\")] [SerializeField]
    MoveOnMap _moveOnMap;

    [Tooltip("�}�X�ړ�����UI��\������@�\")] [SerializeField]
    ShowUITypeBase _showMoveUI;

    [Tooltip("�}�X�ړ�����UI���\���ɂ���@�\")] [SerializeField]
    HideUITypeBase _hideMoveUI;

    PlayerTurnFlowManager _stateMachine;
    bool _finished = true;

    //�ړ����������ďI���X�e�[�g��
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

        //�}�X�ړ����\�ȏ�Ԃɂ���
        _moveOnMap.enabled = true;
    }

    public override void OnUpdate(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {

    }

    public override void OnExit(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _finished = true;

        _hideMoveUI.Hide();

        //�}�X�ړ����o���Ȃ���Ԃɂ���
        _moveOnMap.enabled = false;
    }
}
