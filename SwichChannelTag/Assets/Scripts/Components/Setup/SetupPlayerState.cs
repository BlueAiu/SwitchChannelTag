using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

//作成者:杉山
//プレイヤーの鬼・逃げの初期化
//今のところ、プレイヤーの中からランダムに鬼を一人選出

public static class SetupPlayerState
{

    public static void SelectTagger()//鬼を決める
    {
        Player mine = PlayersManager.MinePlayerPhotonPlayer;

        if (!mine.IsMasterClient) return;//ホスト主以外はこの処理を行わない

        //参加者の中からランダムに一人選出して、選ばれた人を鬼にする
        PlayerState[] players = PlayersManager.GetComponentsFromPlayers<PlayerState>();

        int taggerIndex=Random.Range(0, players.Length);

        for(int i=0; i<players.Length ;i++)
        {
            EPlayerState state = (i == taggerIndex) ? EPlayerState.Tagger : EPlayerState.Runner;

            players[i].ChangeState(state);
        }
    }
}
