using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//•ßŠl‚É’Ê’m‚·‚é

public class NoticeOnCapture : MonoBehaviour
{
    [SerializeField]
    BannerNoticeManager _bannerNoticeManager;

    private void OnEnable()
    {
        GameStatsManager.Instance.CaptureHistory.OnCapture += OnCapture;
    }

    private void OnDisable()
    {
        GameStatsManager.Instance.CaptureHistory.OnCapture -= OnCapture;
    }

    void OnCapture(CaptureRecord captureRecord)
    {
        string caughtRunnerName = PlayersManager.ActorNumberPlayerInfo(captureRecord.CaughtRunnerActorNum).Player.NickName;

        string content = caughtRunnerName + "‚ª‹S‚É•ß‚Ü‚è‚Ü‚µ‚½I";

        _bannerNoticeManager.AddNotice(content);
    }
}
