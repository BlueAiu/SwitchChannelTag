using System.Collections;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

//プレイヤーごとに行うターン行動


public class PlayerTurnFlow : MonoBehaviour
{
    GameFlowStateTypeBase _current;
    TurnIsReady _myTurnIsReady;

    private void Awake()
    {
        _myTurnIsReady = PlayersManager.GetComponentFromMinePlayer<TurnIsReady>();

        _myTurnIsReady.OnStartTurn += StartMyTurn;
    }

    void StartMyTurn()//自分の行動の許可が出た時に呼び出す
    {
        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        //この時点では他のコンポーネントの初期化が終わってない可能性があるため、一旦1フレーム待つ
        yield return null;

        //ここにプレイヤーごとのターンの処理を書いていく
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
        if (_current != null) _current.OnExit();

        _current = nextState;

        if (_current != null) _current.OnEnter();
    }
}
