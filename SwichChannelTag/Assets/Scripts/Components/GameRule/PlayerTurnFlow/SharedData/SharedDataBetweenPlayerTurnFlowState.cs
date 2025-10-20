using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//PlayerTurnFlow�X�e�[�g�Ԃŋ��L����f�[�^

public class SharedDataBetweenPlayerTurnFlowState
{
    private int _destinationHierarchyIndex;

    private bool _isChangedHierarchy = false;

    public SharedDataBetweenPlayerTurnFlowState() { }

    public int DestinationHierarchyIndex//�ړ���K�w�ԍ�
    {
        get { return _destinationHierarchyIndex; }
        set { _destinationHierarchyIndex = value; }
    }

    public bool IsChangedHierarchy { get { return _isChangedHierarchy; } }//�K�w�ړ�������

    public void ChangedHierarchy()//�K�w�ړ�������
    {
        _isChangedHierarchy = true;
    }

    public void Reset()//�f�[�^��������(�^�[�����ς�邲�Ƃɂ���)
    {
        _isChangedHierarchy = false;
    }
    
}
