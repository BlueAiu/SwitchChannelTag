using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�Q�[���̗���̏I���̃X�e�[�g

public class GameFlowStateTypeFinish : GameFlowStateTypeBase
{
    public override void OnEnter()//�X�e�[�g�̊J�n����
    {
        Debug.Log("�Q�[���I���I");
    }

    public override void OnUpdate()//�X�e�[�g�̖��t���[������
    {
        
    }

    public override void OnExit()//�X�e�[�g�̏I������
    {

    }
}
