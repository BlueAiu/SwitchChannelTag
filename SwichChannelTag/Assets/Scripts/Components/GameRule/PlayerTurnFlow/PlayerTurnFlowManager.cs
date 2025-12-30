using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーごとに行うターン行動(実質ステートマシン的な役割を持っている)

public class PlayerTurnFlowManager : MonoBehaviour
{
    //--- ステート関係 ---//
    [Tooltip("プレイヤーの行動ステート")] [SerializeField]
    SerializableDictionary<EPlayerTurnFlowState, PlayerTurnFlowStateTypeBase> _playerTurnStateDic;

    SharedDataBetweenPlayerTurnFlowState _sharedData=new SharedDataBetweenPlayerTurnFlowState();//ステート間で共有するデータ

    EPlayerTurnFlowState _nowEState=EPlayerTurnFlowState.None;
    EPlayerTurnFlowState _beforeEState=EPlayerTurnFlowState.None;

    PlayerTurnFlowStateTypeBase _currentState=null;
    PlayerTurnStateReceiver _myTurnReceiver;

    public EPlayerTurnFlowState NowState { get { return _nowEState; } }//現在のステート

    public EPlayerTurnFlowState BeforeState { get { return _beforeEState; } }//前のステート

    public SharedDataBetweenPlayerTurnFlowState SharedData { get { return _sharedData; } }//ステート間で共有するデータ

    public void ChangeState(EPlayerTurnFlowState nextState)//ステートの変更
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
        _myTurnReceiver = PlayersManager.GetComponentFromMinePlayer<PlayerTurnStateReceiver>();
    }

    private void OnEnable()
    {
        _myTurnReceiver.OnStartTurn += StartMyTurn;
        _myTurnReceiver.OnWaiting += SwitchWaiting;
    }

    private void OnDisable()
    {
        _myTurnReceiver.OnStartTurn -= StartMyTurn;
        _myTurnReceiver.OnWaiting -= SwitchWaiting;
    }

    void StartMyTurn()//自分の行動の許可が出た時に呼び出す
    {
        //最初のステートは行動選択から
        ChangeState(EPlayerTurnFlowState.SelectAction);
    }

    void SwitchWaiting()//待ち時間
    {
        //待ち時間ステート
        ChangeState(EPlayerTurnFlowState.Waiting);
    }

    private void Update()
    {
        if (_currentState != null) _currentState.OnUpdate();
    }
}
