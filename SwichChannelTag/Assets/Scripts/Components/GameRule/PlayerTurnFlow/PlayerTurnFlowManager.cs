using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーごとに行うターン行動(実質ステートマシン的な役割を持っている)

public class PlayerTurnFlowManager : MonoBehaviour
{
    //--- ステート関係 ---//
    [Tooltip("プレイヤーの行動ステート")] [SerializeField]
    SerializableDictionary<EPlayerTurnState, PlayerTurnFlowStateTypeBase> _playerTurnStateDic;

    SharedDataBetweenPlayerTurnFlowState _sharedData=new SharedDataBetweenPlayerTurnFlowState();//ステート間で共有するデータ

    EPlayerTurnState _nowEState=EPlayerTurnState.None;
    EPlayerTurnState _beforeEState=EPlayerTurnState.None;

    PlayerTurnFlowStateTypeBase _currentState=null;
    TurnIsReady _myTurnIsReady;

    public EPlayerTurnState NowState { get { return _nowEState; } }//現在のステート

    public EPlayerTurnState BeforeState { get { return _beforeEState; } }//前のステート

    public SharedDataBetweenPlayerTurnFlowState SharedData { get { return _sharedData; } }//ステート間で共有するデータ

    public void ChangeState(EPlayerTurnState nextState)//ステートの変更
    {
        if (_currentState != null) _currentState.OnExit();

        _beforeEState = _nowEState;

        if (!_playerTurnStateDic.TryGetValue(nextState, out PlayerTurnFlowStateTypeBase value))
        {
            Debug.Log("次のステートの取得に失敗しました");
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

    void StartMyTurn()//自分の行動の許可が出た時に呼び出す
    {
        //最初のステートは行動選択から
        ChangeState(EPlayerTurnState.SelectAction);
    }

    private void Update()
    {
        if (_currentState != null) _currentState.OnUpdate();
    }
}
