using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:武田
//全てのプレイヤーがターン行動をし終えたか

public class JudgeIsTurnFinishAllPlayer : MonoBehaviour
{
    List<PlayerTurnStateReceiver> ownPlayers = new List<PlayerTurnStateReceiver>();

    public bool IsFinishAll()//全プレイヤーが行動し終えたか
    {
        bool isFinishAll = true;

        foreach (var player in ownPlayers)
        {
            if (player == null) continue;

            isFinishAll &= (player.CurrentState == EPlayerTurnState.TurnIsFinished);
        }

        return isFinishAll;
    }

    public void OnEnter(EPlayerState turnSide)//ターン開始時に呼ぶ
    {
        var playerTurnStateReceivers = PlayersManager.GetComponentsFromPlayers<PlayerTurnStateReceiver>();
        var playerTurnCommunicators = PlayersManager.GetComponentsFromPlayers<PlayerTurnCommunicator>();

        var playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();

        for (int i = 0; i < playerStates.Length; i++)
        {
            bool isTurnSide = playerStates[i].State == turnSide;

            //対象のプレイヤーの行動を開始させる
            //そうでないプレイヤーは待ち状態にする
            playerTurnCommunicators[i].StartTurn(isTurnSide);

            if (isTurnSide)
            {
                ownPlayers.Add(playerTurnStateReceivers[i]);
            }
        }
    }

    public void OnExit()//ターン終了時に呼ぶ
    {
        ownPlayers.Clear();
    }
}
