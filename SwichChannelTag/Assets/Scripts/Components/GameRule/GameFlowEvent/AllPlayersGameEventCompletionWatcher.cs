using System;
using UnityEngine;

//作成者:杉山
//全プレイヤーのゲームイベント完了状態を監視する機能

public class AllPlayersGameEventCompletionWatcher : MonoBehaviour
{
    private GameEventReceiver[] _receivers;

    private bool _areAllPlayersFinished = true;

    //全員の完了状態が変化した時に呼ばれる
    public event Action OnAllPlayersFinishedChanged;

    //全プレイヤーがゲームイベントを完了しているか
    public bool AreAllPlayersFinished
    {
        get => _areAllPlayersFinished;
        private set
        {
            if (_areAllPlayersFinished == value) return;

            _areAllPlayersFinished = value;
            OnAllPlayersFinishedChanged?.Invoke();
        }
    }

    private void Awake()
    {
        _receivers = PlayersManager.GetComponentsFromPlayers<GameEventReceiver>();
    }

    private void OnEnable()
    {
        foreach (var receiver in _receivers)
        {
            receiver.OnSetIsFinished += UpdateValue;
        }
    }

    private void OnDisable()
    {
        foreach (var receiver in _receivers)
        {
            receiver.OnSetIsFinished -= UpdateValue;
        }
    }

    private void UpdateValue()
    {
        foreach (var receiver in _receivers)
        {
            if (receiver == null) continue;

            if (!receiver.IsFinished)
            {
                AreAllPlayersFinished = false;
                return;
            }
        }

        AreAllPlayersFinished = true;
    }
}