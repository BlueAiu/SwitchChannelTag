using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//•ß‚Ü‚¦‚½“¦‚°‚Ìî•ñ‚ğ“o˜^‚·‚é

public class SetCaughtRunnerInfo : MonoBehaviour
{
    [SerializeField] GetRunnerInfoOnPath _getRunnerInfoOnPath;

    CaughtRunnerInfo _myCaughtRunnerInfo;
    PlayerState _myPlayerStete;

    public void Set()//©•ª‚ª‹S‚Å‚ ‚éê‡A•ß‚Ü‚¦‚½“¦‚°‚Ìî•ñ‚ğ“o˜^
    {
        if (_myPlayerStete.State != EPlayerState.Tagger) return;

        _myCaughtRunnerInfo.SetRunnerInfo(_getRunnerInfoOnPath.RunnerInfos);
    }

    public void Clear()//•ß‚Ü‚¦‚½“¦‚°‚Ìî•ñ‚ğƒŠƒZƒbƒg
    {
        _getRunnerInfoOnPath.ClearRunnerInfo();
        _myCaughtRunnerInfo.ClearRunnerInfo();
    }

    private void Awake()
    {
        _myCaughtRunnerInfo = PlayersManager.GetComponentFromMinePlayer<CaughtRunnerInfo>();
        _myPlayerStete = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
    }
}
