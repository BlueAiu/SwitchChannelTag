using System.Collections;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;



public class PlayerTurnFlow : MonoBehaviour
{
    GameFlowStateTypeBase _current;
    PlayerState _currentState;
    bool _isLocal;

    public EPlayerState PlayerState { get => _currentState.State; }

    bool _isTurnFinished = false;
    public bool IsTurnFinished
    {
        get => _isTurnFinished;
        private set { SetTurnFinished(value); }
    }

    private void Start()
    {
        Player mine = PlayersManager.MinePlayerPhotonPlayer;
        _isLocal = mine.IsLocal;

        if (!_isLocal) return;//���[�J���ȊO�͂��̏������s��Ȃ�

        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        //���̎��_�ł͑��̃R���|�[�l���g�̏��������I����ĂȂ��\�������邽�߁A��U1�t���[���҂�
        yield return null;

        //�J�n���o�̃X�e�[�g


        //


        //�I�����o�̃X�e�[�g
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

    [PunRPC]
    void SetTurnFinished(bool value)
        => IsTurnFinished = value;

    [PunRPC]
    public void StartTurn(EPlayerState turnSide)
    {
        if(!_isLocal) return;

        if(PlayerState == turnSide)
        {
            ChangeState(null);
        }
        else
        {
            IsTurnFinished = false;
        }
    }
}
