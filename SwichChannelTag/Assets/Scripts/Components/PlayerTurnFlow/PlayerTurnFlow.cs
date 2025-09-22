using System.Collections;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;



public class PlayerTurnFlow : MonoBehaviour
{
    GameFlowStateTypeBase _current;
    PlayerState _currentState;

    public EPlayerState PlayerState { get => _currentState.State; }

    bool _isTurnFinished = false;
    public bool IsTurnFinished
    {
        get => _isTurnFinished;
        private set { SetTurnFinished(value); }
    }

    private void Start()
    {
        Player mine = PlayersManager.MinePlayerPhotonPlayer;

        if (!mine.IsLocal) return;//プレイ者以外はこの処理を行わない

        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        //この時点では他のコンポーネントの初期化が終わってない可能性があるため、一旦1フレーム待つ
        yield return null;

        //開始演出のステート(実装予定)


        //


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
        if (_current != null) _current.OnExit();

        _current = nextState;

        if (_current != null) _current.OnEnter();
    }

    [PunRPC]
    void SetTurnFinished(bool value)
        => IsTurnFinished = value;
}
