using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//ŠK‘wˆÚ“®ƒXƒe[ƒg

public class PlayerTurnFlowStateTypeChangeHierarchy : PlayerTurnFlowStateTypeBase
{
    [Tooltip("ŠK‘wˆÚ“®‚·‚é‹@”\")] [SerializeField]
    ChangeHierarchy _changeHierarchy;

    public override void OnEnter(PlayerTurnFlowManager stateMachine)
    {
        //_changeHierarchy.SwitchHierarchy//ŠK‘wˆÚ“®ˆ—
        stateMachine.ChangeState(EPlayerTurnState.SelectAction);
    }

    public override void OnUpdate(PlayerTurnFlowManager stateMachine)
    {
        
    }

    public override void OnExit(PlayerTurnFlowManager stateMachine)
    {

    }
}
