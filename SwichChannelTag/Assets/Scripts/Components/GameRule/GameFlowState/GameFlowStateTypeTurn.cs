using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowStateTypeTurn : GameFlowStateTypeBase
{
    [SerializeField] EPlayerState turnSide;

    List<PlayerTurnFlow> ownPlayers;



    public override void OnEnter()//�X�e�[�g�̊J�n����
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

    public override void OnUpdate()//�X�e�[�g�̖��t���[������
    {
        bool isFinishAll = true;
        foreach (var player in ownPlayers)
        {
            isFinishAll &= player.IsTurnFinished;
        }

        if (isFinishAll) this._finished = true;
    }

    public override void OnExit()//�X�e�[�g�̏I������
    {
        // Pass
    }
}
