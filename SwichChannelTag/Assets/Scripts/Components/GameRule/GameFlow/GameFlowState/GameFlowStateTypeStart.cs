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
        _stateMachine.ChangeState(EGameState.RunnerTurn);//�ŏ��͓������̃^�[��
    }

    public override void OnExit()//�X�e�[�g�̏I������
    {

    }
}
