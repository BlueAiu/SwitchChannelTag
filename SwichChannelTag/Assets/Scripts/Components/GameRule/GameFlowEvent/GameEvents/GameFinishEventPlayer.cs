using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//作成者:杉山
//ゲーム終了イベントの再生

public class GameFinishEventPlayer : GameEventPlayer
{
    [Tooltip("再生するタイムライン")] [SerializeField]
    PlayableDirector _eventDirecter;

    [Tooltip("プレイ中に表示するUIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideGameUI;

    [Tooltip("バフエフェクトの表示関係機能")] [SerializeField]
    AllPlayersBuffEffectActivator _allPlayersBuffEffectActivator;

    bool _finished = true;

    public override void Play()
    {
        _finished = false;

        _allPlayersBuffEffectActivator.DeactivateAllBuffEffects();
        _hideGameUI.Hide();//プレイ中に表示するUIを非表示にする
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
