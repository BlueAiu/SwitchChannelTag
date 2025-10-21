using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�K�w�ړ��X�e�[�g

public class PlayerTurnFlowStateTypeChangeHierarchy : PlayerTurnFlowStateTypeBase
{
    [Tooltip("�K�w�ړ�����@�\")] [SerializeField]
    ChangeHierarchy _changeHierarchy;

    public override void OnEnter()
    {
        _changeHierarchy.SwitchHierarchy(_stateMachine.SharedData.DestinationHierarchyIndex);//�K�w�ړ�����
        _stateMachine.SharedData.ChangedHierarchy();//�K�w�ړ�������
    }

    public override void OnUpdate()
    {
        _stateMachine.ChangeState(EPlayerTurnState.SelectAction);
    }

    public override void OnExit()
    {

    }
}
