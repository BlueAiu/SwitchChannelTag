using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//作成者:杉山
//ゲーム開始時の演出

public class GameEventPlayer : MonoBehaviour
{
    [SerializeField]
    EGameEvent _gameEvent;

    [Tooltip("再生するタイムライン")] [SerializeField]
    PlayableDirector _eventDirecter;

    GameEventReceiver _myReceiver;

    bool _finished = true;

    private void Awake()
    {
        _myReceiver = PlayersManager.GetComponentFromMinePlayer<GameEventReceiver>();
    }

    private void OnEnable()
    {
        _myReceiver.OnReceiveEvent += OnReceiveEvent;
        _eventDirecter.stopped += SetTimelineFinish;
    }

    private void OnDisable()
    {
        _myReceiver.OnReceiveEvent -= OnReceiveEvent;
        _eventDirecter.stopped -= SetTimelineFinish;
    }

    void OnReceiveEvent(EGameEvent gameEvent)
    {
        if (gameEvent != _gameEvent) return;
        if (!_finished) return;

        StartCoroutine(PlayCoroutine());
    }

    IEnumerator PlayCoroutine()
    {
        _finished = false;
        _eventDirecter.gameObject.SetActive(true);

        yield return new WaitUntil(() => _finished);

        _eventDirecter.gameObject.SetActive(false);
        _myReceiver.SetFinish();
    }

    void SetTimelineFinish(PlayableDirector pd)
    {
        _finished = true;
    }
}
