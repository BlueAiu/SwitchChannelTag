using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;


//�쐬��:���R
//�Q�[���̗�����Ǘ�����
//�X�e�[�g�p�^�[�����g�p�\��

public class GameFlowManager : MonoBehaviour
{
    //--- �X�e�[�g�֌W ---//
    [Tooltip("�Q�[���t���[�X�e�[�g")] [SerializeField]
    SerializableDictionary<EGameState, GameFlowStateTypeBase> _gameFlowStateDic;

    [Tooltip("�ŏ��ɓ����v���C���[")] [SerializeField]
    EPlayerState _firstTurn=EPlayerState.Runner;

    SharedDataBetweenGameFlowState _sharedData;//�X�e�[�g�Ԃŋ��L����f�[�^

    EGameState _nowEState = EGameState.None;
    EGameState _beforeEState = EGameState.None;

    public EGameState NowState { get { return _nowEState; } }//���݂̃X�e�[�g

    public EGameState BeforeState { get { return _beforeEState; } }//�O�̃X�e�[�g

    public SharedDataBetweenGameFlowState SharedData { get { return _sharedData; } }//�X�e�[�g�Ԃŋ��L����f�[�^

    GameFlowStateTypeBase _currentState =null;

    Player mine;

    private void Awake()
    {
        if(_firstTurn==EPlayerState.Length)
        {
            Debug.Log("Length�ȊO���w�肵�Ă��������I");
            return;
        }

        _sharedData = new SharedDataBetweenGameFlowState(_firstTurn);
    }

    private void Start()
    {
        mine = PlayersManager.MinePlayerPhotonPlayer;

        if (!mine.IsMasterClient) return;//�z�X�g��ȊO�͂��̏������s��Ȃ�

        //�ŏ��̃X�e�[�g�͊J�n���o����
        ChangeState(EGameState.Start);
    }

    public void ChangeState(EGameState nextState)//�X�e�[�g�̕ύX
    {
        if (_currentState != null) _currentState.OnExit();

        _beforeEState = _nowEState;

        if (!_gameFlowStateDic.TryGetValue(nextState, out GameFlowStateTypeBase value))
        {
            Debug.Log("���̃X�e�[�g�̎擾�Ɏ��s���܂���");
            return;
        }

        _currentState = value;

        _nowEState = nextState;

        if (_currentState != null) _currentState.OnEnter();
    }

    private void Update()
    {
        if (!mine.IsMasterClient) return;//�z�X�g��ȊO�͂��̏������s��Ȃ�

        if (_currentState != null) _currentState.OnUpdate();
    }
}
