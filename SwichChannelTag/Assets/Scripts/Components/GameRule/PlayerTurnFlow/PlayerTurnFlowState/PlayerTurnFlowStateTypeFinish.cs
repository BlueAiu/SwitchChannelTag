using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�s�����I������̏��

public class PlayerTurnFlowStateTypeFinish : PlayerTurnFlowStateTypeBase
{
    TurnIsReady _myTurnIsReady;

    public override void OnEnter(PlayerTurnFlowManager _stateMachine)
    {

    }

    public override void OnUpdate(PlayerTurnFlowManager _stateMachine)
    {

    }

    public override void OnExit(PlayerTurnFlowManager _stateMachine)
    {
        _myTurnIsReady.IsReady = true;//�s���I���������Ƃ�m�点��
    }

    private void Awake()
    {
        _myTurnIsReady = PlayersManager.GetComponentFromMinePlayer<TurnIsReady>();
    }
}
