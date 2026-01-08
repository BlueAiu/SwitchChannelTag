using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TagLogText : MonoBehaviour
{
    [SerializeField] SerializableDictionary<EPlayerState, GameObject> winnerText;
    [SerializeField] TMP_Text runnerTurnText;
    [SerializeField] TMP_Text taggerCaptureText;


    void Start()
    {
        var winner = GameStatsManager.Instance.Winner.GetWinner();
        winnerText[winner].SetActive(true);

        int myActorNum = PlayersManager.MinePlayerPhotonPlayer.ActorNumber;
        int runnerTurn = -1;
        int captureCnt = 0;
        string tmp;

        foreach(var capture in GameStatsManager.Instance.CaptureHistory.GetHistory())
        {
            if(capture.CaughtRunnerActorNum == myActorNum)
            {
                runnerTurn = capture.CaptureTurn;
            }

            foreach(var num in capture.CaughtTaggerActorNum)
            {
                if(num == myActorNum) captureCnt++;
            }
        }

        if (runnerTurn != -1)
        {
            tmp = runnerTurn.ToString() + runnerTurnText.text;
            runnerTurnText.text = tmp;
        }
        else runnerTurnText.text = string.Empty;

        if (captureCnt > 0)
        {
            tmp = captureCnt.ToString() + taggerCaptureText.text;
            taggerCaptureText.text = tmp;
        }
        else taggerCaptureText.text = string.Empty;
    }
}
