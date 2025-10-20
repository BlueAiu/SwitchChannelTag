using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//�쐬��:���R
//�ŏ��̍s���I�����

public class PlayerTurnFlowStateTypeSelectAction : PlayerTurnFlowStateTypeBase
{
    [Tooltip("�ŏ��ɑI����ԂɂȂ�{�^��")] [SerializeField]
    Button _defaultSelectButton;

    [Tooltip("�K�w�I���Ɉڂ�{�^��")] [SerializeField]
    Button _selectHierarchyButton;

    [Tooltip("�s���I��UI��\������@�\")] [SerializeField]
    ShowUITypeBase _showSelectActionUI;

    [Tooltip("�s���I��UI���\���ɂ���@�\")] [SerializeField]
    HideUITypeBase _hideSelectActionUI;

    PlayerTurnFlowManager _stateMachine;
    bool _finished = true;

    //�_�C�X�X�e�[�g�Ɉڂ�{�^���ɓo�^���郁�\�b�h
    public void ToDice()
    {
        if (_finished) return; 
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnState.Dice);
    }

    //�K�w�I���X�e�[�g�Ɉڂ�{�^���ɓo�^���郁�\�b�h
    public void ToSelectHierarchy()
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnState.SelectHierarchy);
    }


    public override void OnEnter(PlayerTurnFlowManager stateMachine)
    {
        _finished = false;

        _stateMachine = stateMachine;

        _showSelectActionUI.Show();
        EventSystem.current.SetSelectedGameObject(_defaultSelectButton.gameObject);

        //�O�̃X�e�[�g���K�w�ړ���������A�K�w�I���Ɉڂ�Ȃ��悤�ɂ���
        _selectHierarchyButton.interactable = !(stateMachine.BeforeState == EPlayerTurnState.ChangeHierarchy);
    }

    public override void OnUpdate(PlayerTurnFlowManager stateMachine)
    {
        
    }

    public override void OnExit(PlayerTurnFlowManager stateMachine)
    {
        _finished = true;
        _hideSelectActionUI.Hide();
    }
}
