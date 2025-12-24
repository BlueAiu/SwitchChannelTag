using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームの統計情報のプレイヤー関係の情報
//プレイヤー(逃げや鬼)の人数を取得出来る

public class PlayersStateStats_GameStats
{
    //プレイヤーの人数を取得
    //allPlayersNumは全体の人数、runnersNumは逃げの人数、taggersNumは鬼の人数
    public void GetPlayersStats(out int allPlayersNum,out int runnersNum,out int taggersNum)
    {
        allPlayersNum = 0;
        runnersNum = 0;
        taggersNum = 0;

        var playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();

        foreach ( var playerState in playerStates )
        {
            allPlayersNum++;

            if(playerState.State== EPlayerState.Runner)
            {
                runnersNum++;
            }
            else
            {
                taggersNum++;
            }
        }
    }
}
