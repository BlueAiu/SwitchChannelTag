using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowStateTypeTurn : GameFlowStateTypeBase
{
    [SerializeField] EPlayerState turnSide;
    
    [Tooltip("ゲーム終了かを判定する機能")] [SerializeField]
    JudgeGameSet _judgeGameSet;

    List<TurnIsReady> ownPlayers=new List<TurnIsReady>();

    public override void OnEnter()//ステートの開始処理
    {
        var turnIsReadys = PlayersManager.GetComponentsFromPlayers<TurnIsReady>();
        var playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();

        for(int i=0; i<playerStates.Length ;i++)
        {
            if (playerStates[i].State==turnSide)
            {
                turnIsReadys[i].IsReady = false;//対象のプレイヤーの行動を開始させる
                ownPlayers.Add(turnIsReadys[i]);
            }
        }
    }

    public override void OnUpdate()//ステートの毎フレーム処理
    {
        bool isFinishAll = true;

        foreach (var player in ownPlayers)
        {
            if(player==null) continue;

            isFinishAll &= player.IsReady;
        }

        if (isFinishAll)//ステートを相手のターンに変更(後にゲームが終了したかを取得して終了ステートにも移行するようにする)
        {
            ChangeState();
        }
    }

    public override void OnExit()//ステートの終了処理
    {
        // Pass
    }

    void ChangeState()//ステートを変更する
    {
        //ゲーム終了判定をする
        bool isGameSet = _judgeGameSet.IsGameSet();

        if(isGameSet)
        {
            _stateMachine.ChangeState(EGameFlowState.Finish);
            return;
        }

        //まだゲーム終了していないなら

        EGameFlowState opponentTurn = (turnSide == EPlayerState.Tagger) ? EGameFlowState.RunnerTurn : EGameFlowState.TaggerTurn;

        if(_stateMachine.BeforeState==opponentTurn)//前のターンが相手のターンだったらターン終了ステートへ
        {
            _stateMachine.ChangeState(EGameFlowState.TurnFinish);
            return;
        }
        else//そうでなければ相手のターンへ
        {
            _stateMachine.ChangeState(opponentTurn);
            return;
        }
    }
}
