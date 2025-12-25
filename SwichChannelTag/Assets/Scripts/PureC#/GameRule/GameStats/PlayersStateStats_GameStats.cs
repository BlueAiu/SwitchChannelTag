using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームの統計情報のプレイヤー関係の情報
//プレイヤー(逃げや鬼)の人数を取得出来る

public class PlayersStateStats_GameStats
{
    public const int invalidCount = -1;

    //プレイヤーの人数を取得
    //allPlayersNumは全体の人数、runnersNumは逃げの人数、taggersNumは鬼の人数
    public void GetPlayersCount(out int allPlayersCount,out int runnersCount,out int taggersCount)
    {
        allPlayersCount = 0;
        runnersCount = 0;
        taggersCount = 0;

        var playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();

        foreach ( var playerState in playerStates )
        {
            allPlayersCount++;

            if(playerState.State == EPlayerState.Runner)
            {
                runnersCount++;
            }
            else
            {
                taggersCount++;
            }
        }
    }

    //targetのプレイヤーの人数を取得
    public int GetPlayersCount(EPlayerState target)
    {
        GetPlayersCount(out int allPlayersCount, out int runnersCount, out int taggersCount);

        if (target == EPlayerState.Runner) return runnersCount;
        else if (target == EPlayerState.Tagger) return taggersCount;

        return invalidCount;
    }

    //targetのプレイヤーが一人かを取得
    public bool IsPlayerLonely(EPlayerState target)
    {
        const int lonelyCount = 1;

        return GetPlayersCount(target) == lonelyCount;
    }
}
