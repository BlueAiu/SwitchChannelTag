using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲーム終了を判断する

public class JudgeGameSet : MonoBehaviour
{
    [Tooltip("最大ターン数")] [SerializeField]
    int _maxTurnNum=20;

    PlayerState[] _playerStates;

    public bool IsGameSet()//ゲーム終了かの判定
    {
        return IsTimeUp() || AllPlayerIsTagger();
    }

    bool IsTimeUp()//最大ターン数経過したか(時間切れか)
    {
        return GameStatsManager.Instance.GetTurn() > _maxTurnNum;
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
