using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[���Ƃɍs���^�[���s��

public class PlayerTurnFlowManager : MonoBehaviour
{
    //--- �X�e�[�g�֌W ---//

    [Tooltip("�s���I���X�e�[�g")] [SerializeField]
    PlayerTurnFlowStateTypeSelectAction _selectActionState;

    [Tooltip("�_�C�X�X�e�[�g")] [SerializeField]
    PlayerTurnFlowStateTypeDice _diceState;

    [Tooltip("�ړ��X�e�[�g")] [SerializeField]
    PlayerTurnFlowStateTypeMove _moveState;

    [Tooltip("�K�w�I���X�e�[�g")] [SerializeField]
    PlayerTurnFlowStateTypeSelectHierarchy _selectHierarchyState;

    [Tooltip("�K�w�ړ��X�e�[�g")] [SerializeField]
    PlayerTurnFlowStateTypeChangeHierarchy _changeHierarchyState;

    [Tooltip("�s���I���X�e�[�g")] [SerializeField]
    PlayerTurnFlowStateTypeFinish _finishState;

    Dictionary<EPlayerTurnState, PlayerTurnFlowStateTypeBase> _playerTurnStateDic=new Dictionary<EPlayerTurnState, PlayerTurnFlowStateTypeBase>();

    EPlayerTurnState _nowEState=EPlayerTurnState.None;
    EPlayerTurnState _beforeEState=EPlayerTurnState.None;

    PlayerTurnFlowStateTypeBase _currentState=null;
    TurnIsReady _myTurnIsReady;

    public EPlayerTurnState NowState { get { return _nowEState; } }//���݂̃X�e�[�g

    public EPlayerTurnState BeforeState { get { return _beforeEState; } }//�O�̃X�e�[�g

    private void Awake()
    {
        _myTurnIsReady = PlayersManager.GetComponentFromMinePlayer<TurnIsReady>();

        _myTurnIsReady.OnStartTurn += StartMyTurn;

        //�����ɃX�e�[�g��o�^
        _playerTurnStateDic.Add(EPlayerTurnState.SelectAction, _selectActionState);
        _playerTurnStateDic.Add(EPlayerTurnState.Dice, _diceState);
        _playerTurnStateDic.Add(EPlayerTurnState.Move, _moveState);
        _playerTurnStateDic.Add(EPlayerTurnState.SelectHierarchy, _selectHierarchyState);
        _playerTurnStateDic.Add(EPlayerTurnState.ChangeHierarchy, _changeHierarchyState);
        _playerTurnStateDic.Add(EPlayerTurnState.Finish, _finishState);
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

    void ChangeState(EPlayerTurnState nextState)//�X�e�[�g�̕ύX
    {
        if (_currentState != null) _currentState.OnExit(this);

        _beforeEState = _nowEState;

        if(!_playerTurnStateDic.TryGetValue(nextState,out PlayerTurnFlowStateTypeBase value))
        {
            Debug.Log("���̃X�e�[�g�̎擾�Ɏ��s���܂���");
            return;
        }

        _currentState = value;

        _nowEState = nextState;

        if (_currentState != null) _currentState.OnEnter(this);
    }
}
