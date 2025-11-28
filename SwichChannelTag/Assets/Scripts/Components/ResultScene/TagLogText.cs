using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TagLogText : MonoBehaviour
{
    [SerializeField] TMP_Text _text;


    void Start()
    {
        var captureHistory = GameStatsManager.Instance.CaptureHistory.GetHistory();

        _text.text = string.Empty;

        foreach(var capture in captureHistory)
        {
            int turn = capture.CaptureTurn;
            int runnerNum = capture.CaughtRunnerActorNum;
            var taggerNums = capture.CaughtTaggerActorNum.ToString();

            _text.text += string.Format("{0}Turn : {2} => {1} \n", turn, runnerNum, taggerNums);
        }

        if (captureHistory.Length == 0)
            _text.text = "No one was caught.";
    }
}
