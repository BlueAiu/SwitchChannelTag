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

    EGameState _nowEState = EGameState.None;
    EGameState _beforeEState = EGameState.None;

    public EGameState NowState { get { return _nowEState; } }//���݂̃X�e�[�g

    public EGameState BeforeState { get { return _beforeEState; } }//�O�̃X�e�[�g

    GameFlowStateTypeBase _currentState=null;

    private void Start()
    {
        Player mine = PlayersManager.MinePlayerPhotonPlayer;

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
        if (_currentState != null) _currentState.OnUpdate();
    }
}
