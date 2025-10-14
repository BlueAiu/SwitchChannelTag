using System.Collections;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

//�v���C���[���Ƃɍs���^�[���s��


public class PlayerTurnFlow : MonoBehaviour
{
    GameFlowStateTypeBase _current;
    TurnIsReady _myTurnIsReady;

    private void Awake()
    {
        _myTurnIsReady = PlayersManager.GetComponentFromMinePlayer<TurnIsReady>();

        _myTurnIsReady.OnStartTurn += StartMyTurn;
    }

    void StartMyTurn()//�����̍s���̋����o�����ɌĂяo��
    {
        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        //���̎��_�ł͑��̃R���|�[�l���g�̏��������I����ĂȂ��\�������邽�߁A��U1�t���[���҂�
        yield return null;

        //�����Ƀv���C���[���Ƃ̃^�[���̏����������Ă���
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
        if (_current != null) _current.OnExit();

        _current = nextState;

        if (_current != null) _current.OnEnter();
    }
}
