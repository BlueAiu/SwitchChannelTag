using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowStateTypeTurn : GameFlowStateTypeBase
{
    [SerializeField] EPlayerState turnSide;

    List<TurnIsReady> ownPlayers;

    public override void OnEnter()//�X�e�[�g�̊J�n����
    {
        var turnIsReadys = PlayersManager.GetComponentsFromPlayers<TurnIsReady>();
        var playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();

        for(int i=0; i<playerStates.Length ;i++)
        {
            if (playerStates[i].State==turnSide)
            {
                turnIsReadys[i].IsReady = false;//�Ώۂ̃v���C���[�̍s�����J�n������
                ownPlayers.Add(turnIsReadys[i]);
            }
        }
    }

    public override void OnUpdate()//�X�e�[�g�̖��t���[������
    {
        bool isFinishAll = true;
        foreach (var player in ownPlayers)
        {
            isFinishAll &= player.IsReady;
        }

        if (isFinishAll) this._finished = true;
    }

    public override void OnExit()//�X�e�[�g�̏I������
    {
        // Pass
    }
}
