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

    bool _finished = true;

    //�ړ����������ďI���X�e�[�g��
    public void ToFinish()
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnState.Finish);
    }

    public override void OnEnter()
    {
        _finished = false;

        _showMoveUI.Show();

        //�}�X�ړ����\�ȏ�Ԃɂ���
        _moveOnMap.enabled = true;
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _finished = true;

        _hideMoveUI.Hide();

        _moveOnMap.ClearMoveHistory();

        //�}�X�ړ����o���Ȃ���Ԃɂ���
        _moveOnMap.enabled = false;
    }

    private void Start()
    {
        _hideMoveUI.Hide();
    }
}
