using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowStateTypeTurn : GameFlowStateTypeBase
{
    [SerializeField] EPlayerState turnSide;

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

        //ゲームが終了したなら終了ステートへ
        bool hoge=false;//後でゲームが終了したかを取得するようにする

        if(hoge)
        {
            _stateMachine.ChangeState(EGameState.Finish);
            return;
        }

        //まだゲーム終了していないなら

        EGameState opponentTurn = (turnSide == EPlayerState.Tagger) ? EGameState.RunnerTurn : EGameState.TaggerTurn;

        if(_stateMachine.BeforeState==opponentTurn)//前のターンが相手のターンだったらターン終了ステートへ
        {
            _stateMachine.ChangeState(EGameState.TurnFinish);
            return;
        }
        else//そうでなければ相手のターンへ
        {
            _stateMachine.ChangeState(opponentTurn);
            return;
        }
    }
}
