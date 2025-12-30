using System;
using System.Collections;
using UnityEngine;

//作成者:杉山
//全プレイヤーのゲームイベント完了状態を監視する機能

public class AllPlayersGameEventCompletionWatcher : MonoBehaviour
{
    [Tooltip("シーン開始時に全プレイヤーのGameEventReceiverを取得するか")] [SerializeField]
    bool _getReceiversOnEnable=true;

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

    public void UpdateReceivers()
    {
        _receivers = PlayersManager.GetComponentsFromPlayers<GameEventReceiver>();

        if (_receivers == null) return;

        foreach (var receiver in _receivers)
        {
            receiver.OnSetIsFinished += UpdateValue;
        }
    }

    private void OnEnable()
    {
        if(_getReceiversOnEnable) UpdateReceivers();
    }

    private void OnDisable()
    {
        if(_receivers==null) return;

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