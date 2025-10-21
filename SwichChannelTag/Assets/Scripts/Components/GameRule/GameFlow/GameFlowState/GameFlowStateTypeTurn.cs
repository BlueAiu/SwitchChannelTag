using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowStateTypeTurn : GameFlowStateTypeBase
{
    [SerializeField] EPlayerState turnSide;

    List<TurnIsReady> ownPlayers;

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
            isFinishAll &= player.IsReady;
        }

        if (isFinishAll)//ステートを相手のターンに変更(後にゲームが終了したかを取得して終了ステートにも移行するようにする)
        {
            EGameState opponentTurn = (turnSide == EPlayerState.Tagger) ? EGameState.RunnerTurn : EGameState.TaggerTurn;

            _stateMachine.ChangeState(opponentTurn);
        }
    }

    public override void OnExit()//ステートの終了処理
    {
        // Pass
    }
}
