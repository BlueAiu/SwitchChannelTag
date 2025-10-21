using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�v���C���[���Ƃɍs���^�[���s��(�����X�e�[�g�}�V���I�Ȗ����������Ă���)

public class PlayerTurnFlowManager : MonoBehaviour
{
    //--- �X�e�[�g�֌W ---//
    [Tooltip("�v���C���[�̍s���X�e�[�g")] [SerializeField]
    SerializableDictionary<EPlayerTurnState, PlayerTurnFlowStateTypeBase> _playerTurnStateDic;

    SharedDataBetweenPlayerTurnFlowState _sharedData=new SharedDataBetweenPlayerTurnFlowState();//�X�e�[�g�Ԃŋ��L����f�[�^

    EPlayerTurnState _nowEState=EPlayerTurnState.None;
    EPlayerTurnState _beforeEState=EPlayerTurnState.None;

    PlayerTurnFlowStateTypeBase _currentState=null;
    TurnIsReady _myTurnIsReady;

    public EPlayerTurnState NowState { get { return _nowEState; } }//���݂̃X�e�[�g

    public EPlayerTurnState BeforeState { get { return _beforeEState; } }//�O�̃X�e�[�g

    public SharedDataBetweenPlayerTurnFlowState SharedData { get { return _sharedData; } }//�X�e�[�g�Ԃŋ��L����f�[�^

    public void ChangeState(EPlayerTurnState nextState)//�X�e�[�g�̕ύX
    {
        if (_currentState != null) _currentState.OnExit();

        _beforeEState = _nowEState;

        if (!_playerTurnStateDic.TryGetValue(nextState, out PlayerTurnFlowStateTypeBase value))
        {
            Debug.Log("���̃X�e�[�g�̎擾�Ɏ��s���܂���");
            return;
        }

        _currentState = value;

        _nowEState = nextState;

        if (_currentState != null) _currentState.OnEnter();
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
        if (_currentState != null) _currentState.OnUpdate();
    }
}
