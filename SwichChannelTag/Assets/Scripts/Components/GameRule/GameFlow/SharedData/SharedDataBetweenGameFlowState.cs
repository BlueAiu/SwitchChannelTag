using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//GameFlow�X�e�[�g�Ԃŋ��L����f�[�^

public class SharedDataBetweenGameFlowState
{
    private EPlayerState _firstTurn;

    public SharedDataBetweenGameFlowState(EPlayerState firstTurn)
    {
        _firstTurn = firstTurn;
    }

    public EPlayerState FirstTurn//�ŏ��ɓ����v���C���[
    {
        get { return _firstTurn; }
    }
}
