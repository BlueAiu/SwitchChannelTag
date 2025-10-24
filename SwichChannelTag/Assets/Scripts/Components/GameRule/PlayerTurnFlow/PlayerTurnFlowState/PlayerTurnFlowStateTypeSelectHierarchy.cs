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

    bool _finished=true;

    //�K�w�ړ��X�e�[�g�Ɉړ�
    public void ToChangeHierarchy(int hierarchyIndex)
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.SharedData.DestinationHierarchyIndex = hierarchyIndex;//�ړ���K�w�ԍ����L�^

        _stateMachine.ChangeState(EPlayerTurnState.ChangeHierarchy);
    }

    //�s���I���X�e�[�g�ɖ߂�
    public void BackToActionSelect()
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnState.SelectAction);
    }

    public override void OnEnter()
    {
        _finished = false;

        _showSelectHierarchyUI.Show();

        EventSystem.current.SetSelectedGameObject(_defaultSelectButton.gameObject);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _finished=true;

        _hideSelectHierarchyUI.Hide();
    }

    private void Start()
    {
        _hideSelectHierarchyUI.Hide();//�V�[���J�n����UI���B��
    }
}