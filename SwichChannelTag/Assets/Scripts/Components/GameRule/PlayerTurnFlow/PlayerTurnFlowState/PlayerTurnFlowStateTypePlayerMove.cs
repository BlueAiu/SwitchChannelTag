using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マス移動ステート

public class PlayerTurnFlowStateTypePlayerMove : PlayerTurnFlowStateTypeBase
{
    [Tooltip("移動経路を決める機能")] [SerializeField]
    DecidePath _decidePath;

    [Tooltip("経路上を移動する機能")] [SerializeField]
    MoveOnPath _moveOnPath;

    public override void OnEnter()
    {
        _moveOnPath.StartMove(_decidePath.MovePath);
    }

    public override void OnUpdate()
    {
        if (!_moveOnPath.IsMoving)
        {
            _decidePath.ClearMoveHistory();
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
