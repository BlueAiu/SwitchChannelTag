using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowStateTypeTurn : GameFlowStateTypeBase
{
    [SerializeField] EPlayerState turnSide;

    List<TurnIsReady> ownPlayers=new List<TurnIsReady>();

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
            if(player==null) continue;

            isFinishAll &= player.IsReady;
        }

        if (isFinishAll)//�X�e�[�g�𑊎�̃^�[���ɕύX(��ɃQ�[�����I�����������擾���ďI���X�e�[�g�ɂ��ڍs����悤�ɂ���)
        {
            ChangeState();
        }
    }

    public override void OnExit()//�X�e�[�g�̏I������
    {
        // Pass
    }

    void ChangeState()//�X�e�[�g��ύX����
    {
        //�Q�[���I�����������

        //�Q�[�����I�������Ȃ�I���X�e�[�g��
        bool hoge=false;//��ŃQ�[�����I�����������擾����悤�ɂ���

        if(hoge)
        {
            _stateMachine.ChangeState(EGameState.Finish);
            return;
        }

        //�܂��Q�[���I�����Ă��Ȃ��Ȃ�

        EGameState opponentTurn = (turnSide == EPlayerState.Tagger) ? EGameState.RunnerTurn : EGameState.TaggerTurn;

        if(_stateMachine.BeforeState==opponentTurn)//�O�̃^�[��������̃^�[����������^�[���I���X�e�[�g��
        {
            _stateMachine.ChangeState(EGameState.TurnFinish);
            return;
        }
        else//�����łȂ���Α���̃^�[����
        {
            _stateMachine.ChangeState(opponentTurn);
            return;
        }
    }
}
