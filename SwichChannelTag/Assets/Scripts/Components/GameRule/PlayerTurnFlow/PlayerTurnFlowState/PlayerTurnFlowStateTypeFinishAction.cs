using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�s�����I������̏��

public class PlayerTurnFlowStateTypeFinishAction : PlayerTurnFlowStateTypeBase
{
    TurnIsReady _myTurnIsReady;

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _myTurnIsReady.IsReady = true;//�s���I���������Ƃ�m�点��
    }

    private void Awake()
    {
        _myTurnIsReady = PlayersManager.GetComponentFromMinePlayer<TurnIsReady>();
    }
}
