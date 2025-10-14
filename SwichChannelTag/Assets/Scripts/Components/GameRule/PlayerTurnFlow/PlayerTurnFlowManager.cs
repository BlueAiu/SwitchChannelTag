using System.Collections;
using UnityEngine;

//�v���C���[���Ƃɍs���^�[���s��

public class PlayerTurnFlowManager : MonoBehaviour
{
    [Tooltip("���E�ړ���_�C�X��I�ԍŏ��̏��")] [SerializeField]
    PlayerTurnFlowStateTypeFirstActionSelect _firstActionSelect;

    [Tooltip("���E�ړ�������̏��(���Zver�_�C�X���s����)")] [SerializeField]
    PlayerTurnFlowStateTypeAfterSwitchHierarchy _afterSwitchHierarchy;

    [Tooltip("�s�����I������̏��")] [SerializeField]
    PlayerTurnFlowStateTypeFinishAction _finishAction;

    PlayerTurnFlowStateTypeBase _current;
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

        //�ŏ��̍s���I��
        ChangeState(_firstActionSelect);
        CurrentStateUpdate();

        //���E�ړ���̏��
        bool dummy = false;//��Ő��E�ړ�������������

        if(dummy)
        {
            ChangeState(_afterSwitchHierarchy);
            CurrentStateUpdate();
        }

        //�s���I��
        ChangeState(_finishAction);
        CurrentStateUpdate();

        _current.OnExit();
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

    void ChangeState(PlayerTurnFlowStateTypeBase nextState)//�X�e�[�g�̕ύX
    {
        if (_current != null) _current.OnExit();

        _current = nextState;

        if (_current != null) _current.OnEnter();
    }
}
