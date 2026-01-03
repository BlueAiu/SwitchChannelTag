using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//誰かが捕まった時にデバッグログにログ通知を出す機能(デバッグ用機能)

public class TestLog_OnCaughtRunner : MonoBehaviour
{
    CaughtRunnerInfo[] _caughtRunnerInfos;

    private void Awake()
    {
        _caughtRunnerInfos = PlayersManager.GetComponentsFromPlayers<CaughtRunnerInfo>();
    }

    private void OnEnable()
    {
        foreach(var caughtRunnerInfo in _caughtRunnerInfos)
        {
            caughtRunnerInfo.OnAddCaughtRunner += LogCaught;
        }
    }

    private void OnDisable()
    {
        foreach (var caughtRunnerInfo in _caughtRunnerInfos)
        {
            caughtRunnerInfo.OnAddCaughtRunner -= LogCaught;
        }
    }

    void LogCaught(int tagger,int caughtRunner)
    {
        var taggerInfo = PlayersManager.ActorNumberPlayerInfo(tagger);
        var runnerInfo = PlayersManager.ActorNumberPlayerInfo(caughtRunner);

        string taggerName = taggerInfo.Player.NickName;
        string runnerName = runnerInfo.Player.NickName;

        Debug.Log(taggerName + "が" +runnerName + "を捕まえた");
    }
}
