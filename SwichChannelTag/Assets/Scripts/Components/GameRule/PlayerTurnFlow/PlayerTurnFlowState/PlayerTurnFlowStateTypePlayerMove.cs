using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マス移動ステート

public class PlayerTurnFlowStateTypePlayerMove : PlayerTurnFlowStateTypeBase
{
    [Tooltip("マップ上を移動する機能")] [SerializeField]
    MoveOnMap _moveOnMap;

    public override void OnEnter()
    {
        StartCoroutine(_moveOnMap.MoveOnPath());
    }

    public override void OnUpdate()
    {
        if (!_moveOnMap.IsMoving)
        {
            _stateMachine.ChangeState(EPlayerTurnFlowState.Finish);
        }
    }

    public override void OnExit()
    {

    }

    private void Start()
    {

    }
}
