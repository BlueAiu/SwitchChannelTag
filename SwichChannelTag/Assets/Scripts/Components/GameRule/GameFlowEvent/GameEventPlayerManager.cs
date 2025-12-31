using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームイベント処理のマネージャー

public class GameEventPlayerManager : MonoBehaviour
{
    [Tooltip("シーン開始時に自分のGameEventReceiverを取得するか")] [SerializeField]
    bool _getReceiversOnEnable = true;

    [SerializeField]
    SerializableDictionary<EGameEvent, GameEventPlayer> _gameEventPlayers;

    GameEventReceiver _myReceiver;

    public void UpdateMyReceiver()
    {
        _myReceiver = PlayersManager.GetComponentFromMinePlayer<GameEventReceiver>();

        if (_myReceiver == null) return;

        _myReceiver.OnReceiveEvent += OnReceiveEvent;
    }

    private void OnEnable()
    {
        if (_getReceiversOnEnable) UpdateMyReceiver();
    }

    private void OnDisable()
    {
        if(_myReceiver==null) return;

        _myReceiver.OnReceiveEvent -= OnReceiveEvent;
    }

    void OnReceiveEvent(EGameEvent gameEvent)
    {
        if (!_gameEventPlayers.TryGetValue(gameEvent, out GameEventPlayer gameEventPlayer)) return;

        StartCoroutine(PlayGameEventCoroutine(gameEventPlayer));
    }

    IEnumerator PlayGameEventCoroutine(GameEventPlayer gameEventPlayer)
    {
        gameEventPlayer.Play();

        yield return new WaitUntil(()=>gameEventPlayer.IsFinished());

        _myReceiver.SetFinish();
    }
}
