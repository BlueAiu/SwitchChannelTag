using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;


//作成者:杉山
//ゲームの流れを管理する
//ステートパターンを使用予定

public class GameFlowManager : MonoBehaviour
{
    //--- ステート関係 ---//
    [Tooltip("ゲームフローステート")] [SerializeField]
    SerializableDictionary<EGameFlowState, GameFlowStateTypeBase> _gameFlowStateDic;

    [Tooltip("最初に動くプレイヤー")] [SerializeField]
    EPlayerState _firstTurn=EPlayerState.Runner;

    SharedDataBetweenGameFlowState _sharedData;//ステート間で共有するデータ

    EGameFlowState _nowEState = EGameFlowState.None;
    EGameFlowState _beforeEState = EGameFlowState.None;

    public EGameFlowState NowState { get { return _nowEState; } }//現在のステート

    public EGameFlowState BeforeState { get { return _beforeEState; } }//前のステート

    public SharedDataBetweenGameFlowState SharedData { get { return _sharedData; } }//ステート間で共有するデータ

    GameFlowStateTypeBase _currentState =null;

    Player mine;

    private void Awake()
    {
        if(_firstTurn==EPlayerState.Length)
        {
            Debug.Log("Length以外を指定してください！");
            return;
        }

        _sharedData = new SharedDataBetweenGameFlowState(_firstTurn);
    }

    private void Start()
    {
        mine = PlayersManager.MinePlayerPhotonPlayer;

        if (!mine.IsMasterClient) return;//ホスト主以外はこの処理を行わない

        //最初のステートは開始演出から
        ChangeState(EGameFlowState.Start);
    }

    public void ChangeState(EGameFlowState nextState)//ステートの変更
    {
        if (_currentState != null) _currentState.OnExit();

        _beforeEState = _nowEState;

        if (!_gameFlowStateDic.TryGetValue(nextState, out GameFlowStateTypeBase value))
        {
            Debug.Log("次のステートの取得に失敗しました");
            return;
        }

        _currentState = value;

        _nowEState = nextState;
        ObserveGameStateManager.Instance.State = _nowEState;//他プレイヤーにも通知

        if (_currentState != null) _currentState.OnEnter();
    }

    private void Update()
    {
        if (!mine.IsMasterClient) return;//ホスト主以外はこの処理を行わない

        if (_currentState != null) _currentState.OnUpdate();
    }
}
