using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowStateTypeTurn : GameFlowStateTypeBase
{
    EPlayerState turnSide;
    PlayerTurnFlow[] players;

    List<PlayerTurnFlow> ownPlayers;


    public GameFlowStateTypeTurn(EPlayerState turnSide, PlayerTurnFlow[] players)
    {
        this.turnSide = turnSide;
        this.players = players;
    }

    public override void OnEnter()//ステートの開始処理
    {
        foreach (var player in this.players)
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
