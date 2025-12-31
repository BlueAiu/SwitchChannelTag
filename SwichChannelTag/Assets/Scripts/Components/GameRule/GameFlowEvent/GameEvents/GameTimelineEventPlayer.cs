using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

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
        _eventDirecter.Play();//再生
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
        _finished = true;
    }
}
