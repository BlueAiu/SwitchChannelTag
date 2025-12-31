using System;
using UnityEngine;

//作成者:杉山
//自分が完了状態でなおかつ、他にイベント処理が未完了のプレイヤーが存在するかを監視する
public class FinishedMeAndUnfinishedOthersWatcher : MonoBehaviour
{
    [SerializeField]
    private AllPlayersGameEventCompletionWatcher _allPlayersWatcher;

    private GameEventReceiver _myReceiver;

    private bool _hasUnfinishedOtherPlayers=false;

    public event Action OnValueChanged;

    //自分が完了状態でなおかつ、他にイベント処理が未完了のプレイヤーが存在するか
    public bool HasUnfinishedOtherPlayers
    {
        get => _hasUnfinishedOtherPlayers;
        private set
        {
            if (_hasUnfinishedOtherPlayers == value) return;

            _hasUnfinishedOtherPlayers = value;
            OnValueChanged?.Invoke();
        }
    }

    private void Awake()
    {
        _myReceiver = PlayersManager.GetComponentFromMinePlayer<GameEventReceiver>();
    }

    private void OnEnable()
    {
        _allPlayersWatcher.OnAllPlayersFinishedChanged += Recalculate;
        _myReceiver.OnSetIsFinished += Recalculate;

        Recalculate();
    }

    private void OnDisable()
    {
        _allPlayersWatcher.OnAllPlayersFinishedChanged -= Recalculate;
        _myReceiver.OnSetIsFinished -= Recalculate;
    }

    //状態を再計算する
    private void Recalculate()
    {
        bool isMyEventFinished = _myReceiver.IsFinished;
        bool areAllPlayersFinished = _allPlayersWatcher.AreAllPlayersFinished;

        HasUnfinishedOtherPlayers = isMyEventFinished && !areAllPlayersFinished;
    }
}