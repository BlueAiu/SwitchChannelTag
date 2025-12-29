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
        var photonPlayer = PlayersManager.PlayersPhotonPlayer;

        int maxTurn = GameStatsManager.Instance.Turn.GetTurn();

        Dictionary<int, int> runnerTurn = new();

        // set runnerTurn
        for (int i = 0; i < playerStates.Length; i++)
        {
            runnerTurn[photonPlayer[i].ActorNumber] = 
                (playerStates[i].State == EPlayerState.Runner) ? maxTurn : 0;
        }

        foreach(var capture in captureHistory)
        {
            runnerTurn[capture.CaughtRunnerActorNum] = capture.CaptureTurn;
        }

        // set bar Transform
        int idx = 0;
        foreach(var turn in runnerTurn.Values)
        {
            float t = (float)turn / maxTurn;

            int blueRight = (int)Mathf.Lerp(screenWidth - widthMargin, widthMargin, t);
            int redLeft = screenWidth - blueRight;

            blueBar[idx].offsetMax = new Vector2(-blueRight, blueBar[idx].offsetMax.y);
            redBar[idx].offsetMin = new Vector2(redLeft, redBar[idx].offsetMin.y);
            idx++;
        }
    }
}
