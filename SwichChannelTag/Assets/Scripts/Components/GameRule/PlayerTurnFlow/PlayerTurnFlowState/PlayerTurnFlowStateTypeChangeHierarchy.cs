using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//ŠK‘wˆÚ“®ƒXƒe[ƒg

public class PlayerTurnFlowStateTypeChangeHierarchy : PlayerTurnFlowStateTypeBase
{
    [Tooltip("ŠK‘wˆÚ“®‚·‚é‹@”\")] [SerializeField]
    ChangeHierarchy _changeHierarchy;

    public override void OnEnter()
    {
        _changeHierarchy.SwitchHierarchy(_stateMachine.SharedData.DestinationHierarchyIndex);//ŠK‘wˆÚ“®ˆ—
        _stateMachine.SharedData.ChangedHierarchy();//ŠK‘wˆÚ“®‚ğ‚µ‚½
    }

    public override void OnUpdate()
    {
        _stateMachine.ChangeState(EPlayerTurnState.SelectAction);
    }

    public override void OnExit()
    {

    }
}
