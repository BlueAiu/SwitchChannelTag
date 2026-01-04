using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//バフ状態かを判定するクラス

public class BuffState : MonoBehaviour
{
    [SerializeField]
    PlayerState _myPlayerState;

    //バフ状態かを返す
    public bool IsBuff()
    {
        //自分の状態(逃げか鬼)が一人の時にバフ効果が発動
        return GameStatsManager.Instance.PlayersStateStats.IsPlayerLonely(_myPlayerState.State);
    }
}
