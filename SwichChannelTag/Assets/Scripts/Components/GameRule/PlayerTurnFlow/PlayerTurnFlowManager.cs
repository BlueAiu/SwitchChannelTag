using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[���Ƃɍs���^�[���s��

public class PlayerTurnFlowManager : MonoBehaviour
{
    //--- �X�e�[�g�֌W ---//
    [Tooltip("�v���C���[�̍s���X�e�[�g")] [SerializeField]
    SerializableDictionary<EPlayerTurnState, PlayerTurnFlowStateTypeBase> _playerTurnStateDic;

    EPlayerTurnState _nowEState=EPlayerTurnState.None;
    EPlayerTurnState _beforeEState=EPlayerTurnState.None;

    PlayerTurnFlowStateTypeBase _currentState=null;
    TurnIsReady _myTurnIsReady;

    public EPlayerTurnState NowState { get { return _nowEState; } }//���݂̃X�e�[�g

    public EPlayerTurnState BeforeState { get { return _beforeEState; } }//�O�̃X�e�[�g

    public void ChangeState(EPlayerTurnState nextState)//�X�e�[�g�̕ύX
    {
        if (_currentState != null) _currentState.OnExit(this);

        _beforeEState = _nowEState;

        if (!_playerTurnStateDic.TryGetValue(nextState, out PlayerTurnFlowStateTypeBase value))
        {
            Debug.Log("���̃X�e�[�g�̎擾�Ɏ��s���܂���");
            return;
        }

        _currentState = value;

        _nowEState = nextState;

        if (_currentState != null) _currentState.OnEnter(this);
    }

    private void Awake()
    {
        _myTurnIsReady = PlayersManager.GetComponentFromMinePlayer<TurnIsReady>();

        _myTurnIsReady.OnStartTurn += StartMyTurn;
    }

    void StartMyTurn()//�����̍s���̋����o�����ɌĂяo��
    {
        //�ŏ��̃X�e�[�g�͍s���I������
        ChangeState(EPlayerTurnState.SelectAction);
    }

    private void Update()
    {
        if (_currentState != null) _currentState.OnUpdate(this);
    }
}
