using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//�쐬��:���R
//�_�C�X�X�e�[�g

public class PlayerTurnFlowStateTypeDice : PlayerTurnFlowStateTypeBase
{
    [Tooltip("�_�C�X�̃{�^��")] [SerializeField]
    Button _diceButton;

    [Tooltip("�_�C�XUI��\������@�\")] [SerializeField]
    ShowUITypeBase _showDiceUI;

    [Tooltip("�_�C�XUI���\���ɂ���@�\")] [SerializeField]
    HideUITypeBase _hideDiceUI;

    [Tooltip("�_�C�X��U��(������}�X�������肷��)�@�\")] [SerializeField]
    DecideMovableStep _decideMovableStep;

    PlayerTurnFlowManager _stateMachine;
    bool _finished = true;

    //�_�C�X��U���āA�ړ��X�e�[�g�Ɉڂ�
    public void ToMove()
    {
        if (_finished) return;

        _decideMovableStep.Dicide();//�_�C�X��U��

        _stateMachine.ChangeState(EPlayerTurnState.Move);
    }

    //�s���I���X�e�[�g�ɖ߂�
    public void BackToActionSelect()
    {
        if (_finished) return;

        _stateMachine.ChangeState(EPlayerTurnState.SelectAction);
    }

    public override void OnEnter(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _stateMachine=stateMachine;
        _finished = false;

        _showDiceUI.Show();
        EventSystem.current.SetSelectedGameObject(_diceButton.gameObject);
    }

    public override void OnUpdate(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {

    }

    public override void OnExit(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _finished = true;
       _hideDiceUI.Hide();
    }
}
