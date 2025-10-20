using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�K�w�ړ��X�e�[�g

public class PlayerTurnFlowStateTypeChangeHierarchy : PlayerTurnFlowStateTypeBase
{
    [Tooltip("�K�w�ړ�����@�\")] [SerializeField]
    ChangeHierarchy _changeHierarchy;

    public override void OnEnter(PlayerTurnFlowManager stateMachine)
    {
        //_changeHierarchy.SwitchHierarchy//�K�w�ړ�����
        stateMachine.ChangeState(EPlayerTurnState.SelectAction);
    }

    public override void OnUpdate(PlayerTurnFlowManager stateMachine)
    {
        
    }

    public override void OnExit(PlayerTurnFlowManager stateMachine)
    {

    }
}
