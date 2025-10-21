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

    [Tooltip("�X�e�[�g�}�V��")] [SerializeField]
    protected PlayerTurnFlowManager _stateMachine;



    public abstract void OnEnter();//�X�e�[�g�̊J�n����
    public abstract void OnUpdate();//�X�e�[�g�̖��t���[������
    public abstract void OnExit();//�X�e�[�g�̏I������
}
