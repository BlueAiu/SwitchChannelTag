using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;


//作成者:杉山
//ゲームの流れを管理する
//ステートパターンを使用予定

public class GameFlowManager : MonoBehaviour
{
    GameFlowStateTypeBase _current;
    PlayerTurnFlow[] playerTurnFlows;

    private void Start()
    {
        Player mine = PlayersManager.MinePlayerPhotonPlayer;

        if (!mine.IsMasterClient) return;//ホスト主以外はこの処理を行わない

        playerTurnFlows = PlayersManager.GetComponentsFromPlayers<PlayerTurnFlow>();

        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        //この時点では他のコンポーネントの初期化が終わってない可能性があるため、一旦1フレーム待つ
        yield return null;

        //開始演出のステート(実装予定)
        

        //ゲーム中は逃げる側の行動ステート→ゲーム決着判定→鬼側の行動ステート→ゲーム決着判定を繰り返す(実装予定)
        

        //終了演出のステート(実装予定)
    }

    IEnumerator CurrentStateUpdate()//現在のステートの更新処理
    {
        if (_current != null) yield break;

        while (!_current.Finished)
        {
            yield return null;
            _current.OnUpdate();
        }
    }

    void ChangeState(GameFlowStateTypeBase nextState)//ステートの変更
    {
        if(_current!=null) _current.OnExit();

        _current = nextState;

        if(_current!=null) _current.OnEnter();
    }
}
