using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//行動を終えた後の状態

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
        _myTurnIsReady.IsReady = true;//行動終了したことを知らせる
    }

    private void Awake()
    {
        _myTurnIsReady = PlayersManager.GetComponentFromMinePlayer<TurnIsReady>();
    }
}
