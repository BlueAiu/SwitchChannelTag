using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//�쐬��:���R
//�K�w�I���X�e�[�g

public class PlayerTurnFlowStateTypeSelectHierarchy : PlayerTurnFlowStateTypeBase
{
    [Tooltip("�ŏ��ɑI����ԂɂȂ�{�^��")] [SerializeField]
    Button _defaultSelectButton;

    [Tooltip("�K�w�I��UI��\������@�\")] [SerializeField]
    ShowUITypeBase _showSelectHierarchyUI;

    [Tooltip("�K�w�I��UI���\���ɂ���@�\")] [SerializeField]
    HideUITypeBase _hideSelectHierarchyUI;

    PlayerTurnFlowManager _stateMachine;
    bool _finished=true;

    //�K�w�ړ��X�e�[�g�Ɉړ�
    public void ToChangeHierarchy(int hierarchyIndex)
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        //��őI�񂾊K�w���L�^����N���X������Ă����ɋL�^����

        _stateMachine.ChangeState(EPlayerTurnState.ChangeHierarchy);
    }

    public override void OnEnter(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _finished = false;
        _stateMachine = stateMachine;

        _showSelectHierarchyUI.Show();

        EventSystem.current.SetSelectedGameObject(_defaultSelectButton.gameObject);
    }

    public override void OnUpdate(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {

    }

    public override void OnExit(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _finished=true;

        _hideSelectHierarchyUI.Hide();
    }
}