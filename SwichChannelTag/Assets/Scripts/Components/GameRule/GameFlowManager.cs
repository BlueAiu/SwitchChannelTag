using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;


//�쐬��:���R
//�Q�[���̗�����Ǘ�����
//�X�e�[�g�p�^�[�����g�p�\��

public class GameFlowManager : MonoBehaviour
{
    GameFlowStateTypeBase _current;

    private void Start()
    {
        Player mine = PlayersManager.MinePlayerPhotonPlayer;

        if (!mine.IsMasterClient) return;//�z�X�g��ȊO�͂��̏������s��Ȃ�

        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        //���̎��_�ł͑��̃R���|�[�l���g�̏��������I����ĂȂ��\�������邽�߁A��U1�t���[���҂�
        yield return null;

        //�J�n���o�̃X�e�[�g(�����\��)
        

        //�Q�[�����͓����鑤�̍s���X�e�[�g���Q�[���������聨�S���̍s���X�e�[�g���Q�[������������J��Ԃ�(�����\��)
        

        //�I�����o�̃X�e�[�g(�����\��)
    }

    IEnumerator CurrentStateUpdate()//���݂̃X�e�[�g�̍X�V����
    {
        if (_current != null) yield break;

        while (!_current.Finished)
        {
            yield return null;
            _current.OnUpdate();
        }
    }

    void ChangeState(GameFlowStateTypeBase nextState)//�X�e�[�g�̕ύX
    {
        if(_current!=null) _current.OnExit();

        _current = nextState;

        if(_current!=null) _current.OnEnter();
    }
}
