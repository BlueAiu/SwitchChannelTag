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
    ///�X�e�[�g���I���������ۂ́AOnEnter��OnUpdate����_finished��true�ɂ���΃X�e�[�g���甲���o����
    ///�X�e�[�g�̊J�n(OnEnter���Ă΂��)����_finished��false�ɂ��邱�Ƃ𐄏�����
    /// </summary>
    protected bool _finished = false;

    public bool Finished { get { return _finished; } }


    public abstract void OnEnter();//�X�e�[�g�̊J�n����
    public abstract void OnUpdate();//�X�e�[�g�̖��t���[������
    public abstract void OnExit();//�X�e�[�g�̏I������
}
