using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//作成者:杉山
//タイムラインでのゲームイベントの再生

public class GameTimelineEventPlayer : GameEventPlayer
{
    [Tooltip("再生するタイムライン")] [SerializeField]
    PlayableDirector _eventDirecter;

    bool _finished = true;

    public override void Play()
    {
        _finished = false;
        _eventDirecter.gameObject.SetActive(true);
    }

    public override bool IsFinished()
    {
        return _finished;
    }

    private void OnEnable()
    {
        _eventDirecter.stopped += SetTimelineFinish;
    }

    private void OnDisable()
    {
        _eventDirecter.stopped -= SetTimelineFinish;
    }

    void SetTimelineFinish(PlayableDirector pd)
    {
        _eventDirecter.gameObject.SetActive(false);
        _finished = true;
    }
}
