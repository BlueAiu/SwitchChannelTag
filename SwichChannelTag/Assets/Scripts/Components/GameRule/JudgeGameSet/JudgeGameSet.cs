using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲーム終了と勝敗を判断する

public class JudgeGameSet : MonoBehaviour
{
    [Tooltip("最大ターン数")] [SerializeField]
    MaxTurnNumConfig _maxTurn;

    PlayerState[] _playerStates;

    public bool IsTimeUp(out EPlayerState winner)//最大ターン数経過したか(時間切れか)、経過ターン数が最大ターン数以上になっていたらゲーム終了
    {
        winner = EPlayerState.None;

        bool isGameSet = GameStatsManager.Instance.Turn.GetTurn() >= _maxTurn.MaxTurnNum;

        if(isGameSet)//決着がついてるなら勝者を決める
        {
            winner = AllPlayerIsTagger() ? EPlayerState.Tagger : EPlayerState.Runner; 
        }

        return isGameSet;
    }

    public bool AllPlayerIsTagger()//全てのプレイヤーが鬼か
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
