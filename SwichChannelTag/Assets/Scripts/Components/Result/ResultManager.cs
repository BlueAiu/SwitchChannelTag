using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//���ʂ̃}�l�[�W���[(���ʂ̊m��)
//���̃}�l�[�W���[���̂̓Q�[���V�[���ɒu��

public class ResultManager : MonoBehaviour
{
    static ScoreData _score;

    public static ScoreData Score { get { return _score; } }//�X�R�A�̎擾(�X�R�A���m�肵�Ă��Ȃ��ꍇ��null���Ԃ����̂Œ���)

    //���ʂ̊m��(�Q�[�����I�������猋�ʂɏ������݂���)
    public void ConfirmResult()
    {
        _score = new ScoreData();
    }

    private void Awake()
    {
        //�Q�[���V�[���J�n���ɃX�R�A������
        _score = null;
    }
}
