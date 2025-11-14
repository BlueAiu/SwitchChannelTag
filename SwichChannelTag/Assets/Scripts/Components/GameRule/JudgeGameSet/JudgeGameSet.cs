using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲーム終了と勝敗を判断する

public class JudgeGameSet : MonoBehaviour
{
    [Tooltip("最大ターン数")] [SerializeField]
    int _maxTurnNum=20;

    PlayerState[] _playerStates;

    public bool IsGameSet(out EPlayerState? winner)//ゲーム終了かの判定、勝敗がついた時はどちらが勝ちかも伝える
    {
        winner = null;

        if(AllPlayerIsTagger())
        {
            winner = EPlayerState.Tagger;
            return true;
        }

        if(IsTimeUp())//全員鬼でないことは確定している
        {
            winner = EPlayerState.Runner;
            return true;
        }

        return false;
    }

    bool IsTimeUp()//最大ターン数経過したか(時間切れか)
    {
        return GameStatsManager.Instance.Turn.GetTurn() > _maxTurnNum;
    }

    bool AllPlayerIsTagger()//全てのプレイヤーが鬼か
    {
        bool ret = true;

        for(int i=0; i<_playerStates.Length ;i++)
        {
            if (_playerStates[i] == null) continue;

            if (_playerStates[i].State==EPlayerState.Runner)
            {
                ret = false;
                break;
            }
        }

        return ret;
    }

    private void Awake()
    {
        _playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();
    }
}
