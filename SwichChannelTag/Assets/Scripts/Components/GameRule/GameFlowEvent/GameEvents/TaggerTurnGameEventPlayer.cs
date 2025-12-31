using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//鬼のターンでのゲームイベントの再生

public class TaggerTurnGameEventPlayer : GameEventPlayer
{
    [Tooltip("BGMを変更する機能")] [SerializeField]
    GamePhaseBGMController _gamePhaseBGMController;

    bool _finished = true;

    public override void Play()
    {
        _finished = false;
        _gamePhaseBGMController.UpdateBGM();

        //今後何か処理を入れていくかも
        _finished = true;
    }

    public override bool IsFinished()
    {
        return _finished;
    }
}
