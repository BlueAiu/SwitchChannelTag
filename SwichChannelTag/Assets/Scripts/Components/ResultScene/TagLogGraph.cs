using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TagLogGraph : MonoBehaviour
{
    [SerializeField] RectTransform[] blueBar;
    [SerializeField] RectTransform[] redBar;

    [SerializeField] int screenWidth = 1920;
    [SerializeField] int widthMargin = 100;

    void Start()
    {
        var captureHistory = GameStatsManager.Instance.CaptureHistory.GetHistory();
        var playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();

        int maxTurn = GameStatsManager.Instance.Turn.GetTurn();

        int[] runnerTurn = new int[playerStates.Length];

        // set runnerTurn
        for (int i = 0; i < playerStates.Length; i++)
        {
            runnerTurn[i] = (playerStates[i].State == EPlayerState.Runner) ? maxTurn : 0;
        }

        foreach(var capture in captureHistory)
        {
            runnerTurn[capture.CaughtRunnerActorNum] = capture.CaptureTurn;
        }

        // set bar Transform
        for(int i = 0;i< blueBar.Length; i++)
        {
            if (i >= runnerTurn.Length)
            {
                blueBar[i].sizeDelta = Vector2.zero;
                redBar[i].sizeDelta = Vector2.zero;
                continue;
            }

            float t = (float)runnerTurn[i] / maxTurn;

            int blueRight = MathfExtension.Lerp(screenWidth - widthMargin, widthMargin, t);
            int redLeft = screenWidth - blueRight;

            blueBar[i].offsetMax = new Vector2(-blueRight, blueBar[i].offsetMax.y);
            redBar[i].offsetMin = new Vector2(redLeft, redBar[i].offsetMin.y);
        }
    }
}
