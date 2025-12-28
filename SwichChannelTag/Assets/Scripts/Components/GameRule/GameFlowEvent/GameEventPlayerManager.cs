using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲームイベント処理のマネージャー

public class GameEventPlayerManager : MonoBehaviour
{
    [SerializeField]
    SerializableDictionary<EGameEvent, GameEventPlayer> _gameEventPlayers;

    GameEventReceiver _myReceiver;

    private void Awake()
    {
        _myReceiver = PlayersManager.GetComponentFromMinePlayer<GameEventReceiver>();
    }

    private void OnEnable()
    {
        _myReceiver.OnReceiveEvent += OnReceiveEvent;
    }

    private void OnDisable()
    {
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
