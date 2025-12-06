using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マス移動ステート

public class PlayerTurnFlowStateTypePlayerMove : PlayerTurnFlowStateTypeBase
{
    [Tooltip("移動経路")] [SerializeField]
    PathWay _pathWay;

    [Tooltip("移動経路UI")] [SerializeField]
    PathUI _pathUI;

    [Tooltip("経路上を移動する機能")] [SerializeField]
    MoveOnPath _moveOnPath;

    public override void OnEnter()
    {
        _moveOnPath.StartMove(_pathWay.MovePath);
    }

    public override void OnUpdate()
    {
        if (!_moveOnPath.IsMoving)
        {
            _pathWay.ClearPath();
            _pathUI.ClearPath();
            _stateMachine.ChangeState(EPlayerTurnFlowState.Finish);
        }
    }

    public override void OnExit()
    {

    }
}
