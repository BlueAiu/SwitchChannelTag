using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�Q�[���̏��(Enum)

public enum EGameState
{
    None = -1,//����null�̈���

    Start,//�J�n���o�X�e�[�g
    TaggerTurn,//�S�^�[���X�e�[�g
    RunnerTurn,//�����^�[���X�e�[�g
    TurnFinish,//(���݂���)�^�[���I���X�e�[�g
    Finish,//�I�����o�X�e�[�g

    Length
}