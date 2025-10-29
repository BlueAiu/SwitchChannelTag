using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�Q�[���̗���̃X�^�[�g���o�̃X�e�[�g

public class GameFlowStateTypeStart : GameFlowStateTypeBase
{
    public override void OnEnter()//�X�e�[�g�̊J�n����
    {
       
    }

    public override void OnUpdate()//�X�e�[�g�̖��t���[������
    {
        //��ŉ������o������

        //�^�[������
        EGameState nextState = (_stateMachine.SharedData.FirstTurn == EPlayerState.Runner) ? EGameState.RunnerTurn : EGameState.TaggerTurn;

        _stateMachine.ChangeState(nextState);
    }

    public override void OnExit()//�X�e�[�g�̏I������
    {

    }
}
