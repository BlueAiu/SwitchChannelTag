using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowStateTypeTurn : GameFlowStateTypeBase
{
    [SerializeField] EPlayerState turnSide;

    List<PlayerTurnFlow> ownPlayers;



    public override void OnEnter()//ステートの開始処理
    {
        var players = PlayersManager.GetComponentsFromPlayers<PlayerTurnFlow>();

        foreach (var player in players)
        {
            player.StartTurn(turnSide);

            if(player.PlayerState == turnSide)
            {
                ownPlayers.Add(player);
            }
        }
    }

    public override void OnUpdate()//ステートの毎フレーム処理
    {
        bool isFinishAll = true;
        foreach (var player in ownPlayers)
        {
            isFinishAll &= player.IsTurnFinished;
        }

        if (isFinishAll) this._finished = true;
    }

    public override void OnExit()//ステートの終了処理
    {
        // Pass
    }
}
