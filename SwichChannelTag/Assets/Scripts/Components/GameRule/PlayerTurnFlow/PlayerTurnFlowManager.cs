using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーごとに行うターン行動

public class PlayerTurnFlowManager : MonoBehaviour
{
    //--- ステート関係 ---//

    [Tooltip("行動選択ステート")] [SerializeField]
    PlayerTurnFlowStateTypeSelectAction _selectActionState;

    [Tooltip("ダイスステート")] [SerializeField]
    PlayerTurnFlowStateTypeDice _diceState;

    [Tooltip("移動ステート")] [SerializeField]
    PlayerTurnFlowStateTypeMove _moveState;

    [Tooltip("階層選択ステート")] [SerializeField]
    PlayerTurnFlowStateTypeSelectHierarchy _selectHierarchyState;

    [Tooltip("階層移動ステート")] [SerializeField]
    PlayerTurnFlowStateTypeChangeHierarchy _changeHierarchyState;

    [Tooltip("行動終了ステート")] [SerializeField]
    PlayerTurnFlowStateTypeFinish _finishState;

    Dictionary<EPlayerTurnState, PlayerTurnFlowStateTypeBase> _playerTurnStateDic=new Dictionary<EPlayerTurnState, PlayerTurnFlowStateTypeBase>();

    EPlayerTurnState _nowEState=EPlayerTurnState.None;
    EPlayerTurnState _beforeEState=EPlayerTurnState.None;

    PlayerTurnFlowStateTypeBase _currentState=null;
    TurnIsReady _myTurnIsReady;

    public EPlayerTurnState NowState { get { return _nowEState; } }//現在のステート

    public EPlayerTurnState BeforeState { get { return _beforeEState; } }//前のステート

    private void Awake()
    {
        _myTurnIsReady = PlayersManager.GetComponentFromMinePlayer<TurnIsReady>();

        _myTurnIsReady.OnStartTurn += StartMyTurn;

        //辞書にステートを登録
        _playerTurnStateDic.Add(EPlayerTurnState.SelectAction, _selectActionState);
        _playerTurnStateDic.Add(EPlayerTurnState.Dice, _diceState);
        _playerTurnStateDic.Add(EPlayerTurnState.Move, _moveState);
        _playerTurnStateDic.Add(EPlayerTurnState.SelectHierarchy, _selectHierarchyState);
        _playerTurnStateDic.Add(EPlayerTurnState.ChangeHierarchy, _changeHierarchyState);
        _playerTurnStateDic.Add(EPlayerTurnState.Finish, _finishState);
    }

    void StartMyTurn()//自分の行動の許可が出た時に呼び出す
    {
        //最初のステートは行動選択から
        ChangeState(EPlayerTurnState.SelectAction);
    }

    private void Update()
    {
        if (_currentState != null) _currentState.OnUpdate(this);
    }

    void ChangeState(EPlayerTurnState nextState)//ステートの変更
    {
        if (_currentState != null) _currentState.OnExit(this);

        _beforeEState = _nowEState;

        if(!_playerTurnStateDic.TryGetValue(nextState,out PlayerTurnFlowStateTypeBase value))
        {
            Debug.Log("次のステートの取得に失敗しました");
            return;
        }

        _currentState = value;

        _nowEState = nextState;

        if (_currentState != null) _currentState.OnEnter(this);
    }
}
