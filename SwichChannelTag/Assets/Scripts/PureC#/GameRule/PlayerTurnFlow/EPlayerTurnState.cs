using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�v���C���[�̃^�[�����(Enum�^)

public enum EPlayerTurnState
{
    None=-1,//����null�̈���

    SelectAction,//�s���I���X�e�[�g
    Dice,//�_�C�X�X�e�[�g
    Move,//�}�X�ړ��X�e�[�g
    SelectHierarchy,//�K�w�I���X�e�[�g
    ChangeHierarchy,//�K�w�ړ��X�e�[�g
    Finish,//�s���I���X�e�[�g

    Length
}