using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//逃げのターンでのゲームイベントの再生

public class RunnerTurnGameEventPlayer : GameEventPlayer
{
    [Tooltip("BGMを変更する機能")] [SerializeField]
    GamePhaseBGMController _gamePhaseBGMController;

    [Tooltip("バフエフェクトの表示関係機能")] [SerializeField]
    AllPlayersBuffEffectActivator _allPlayersBuffEffectActivator;

    bool _finished = true;

    public override void Play()
    {
        _finished = false;

        _gamePhaseBGMController.UpdateBGM();
        _allPlayersBuffEffectActivator.RefreshBuffEffects();

        _finished = true;
    }

    public override bool IsFinished()
    {
        return _finished;
    }
}
