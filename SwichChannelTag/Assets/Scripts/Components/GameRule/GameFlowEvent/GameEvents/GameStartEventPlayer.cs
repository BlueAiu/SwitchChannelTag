using UnityEngine;
using UnityEngine.Playables;

//作成者:杉山
//ゲーム開始イベントの再生

public class GameStartEventPlayer : GameEventPlayer
{
    [Tooltip("プレイ中に表示するUIを表示する機能")] [SerializeField]
    ShowUITypeBase _showGameUI;

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
        _showGameUI.Show();//プレイ中に表示するUIを表示する
        _finished = true;
    }
}
