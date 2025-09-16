using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーの鬼・逃げの初期化
//今のところ、プレイヤーの中からランダムに鬼を一人選出

public class SetupPlayerState : MonoBehaviour
{
    void Start()
    {
        SelectTagger();
    }

    void SelectTagger()//鬼を決める
    {
        //参加者の中からランダムに一人選出して、選ばれた人を鬼にする
        PlayerState[] players = PlayersManager.GetComponentsFromPlayers<PlayerState>();

        int taggerIndex=Random.Range(0, players.Length);

        players[taggerIndex].ChangeState(EPlayerState.Tagger);
    }
}
