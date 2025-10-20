using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�v���C���[���Ƃ̃^�[���̗���̃x�[�X�̃X�e�[�g
//�V�����X�e�[�g�����ۂ͂��̃N���X���p�����邱��

public�@abstract class PlayerTurnFlowStateTypeBase : MonoBehaviour
{
    /// <summary>
    ///�E�g����
    ///�X�e�[�g��ύX���鎞��_stateMachine��ChangeState����ύX
    ///sharedData����X�e�[�g�Ԃŋ��L����f�[�^���擾�E�ύX�\
    /// </summary>


    public abstract void OnEnter(PlayerTurnFlowManager _stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData);//�X�e�[�g�̊J�n����
    public abstract void OnUpdate(PlayerTurnFlowManager _stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData);//�X�e�[�g�̖��t���[������
    public abstract void OnExit(PlayerTurnFlowManager _stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData);//�X�e�[�g�̏I������
}
