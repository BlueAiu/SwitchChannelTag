using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagLogGraph : MonoBehaviour
{
    [SerializeField] RectTransform graphParent;
    [SerializeField] RectTransform[] blueBar;
    [SerializeField] RectTransform[] redBar;

    [SerializeField] GameObject markPrefab;
    [SerializeField] Sprite runnerSprite;
    [SerializeField] Sprite taggerSprite;

    [SerializeField] MaxTurnNumConfig _maxTurn;
    //[SerializeField] int screenWidth = 1920;
    [SerializeField] int widthMargin = 100;

    int screenWidth;

    void Start()
    {
        screenWidth = (int)graphParent.rect.width;

        var captureHistory = GameStatsManager.Instance.CaptureHistory.GetHistory();
        var playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();
        var photonPlayer = PlayersManager.PlayersPhotonPlayer;

        int maxTurn = _maxTurn.MaxTurnNum;

        Dictionary<int, int> num2idx = new();
        Dictionary<int, int> runnerTurn = new();

        // set ActorNumber to index
        for(int i = 0; i < photonPlayer.Length; i++)
        {
            num2idx[photonPlayer[i].ActorNumber] = i;
        }

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
        foreach(var turn in runnerTurn)
        {
            int idx = num2idx[turn.Key];
            float t = (float)turn.Value / maxTurn;

            int blueRight = (int)Mathf.Lerp(screenWidth - widthMargin, widthMargin, t);
            int redLeft = screenWidth - blueRight;

            blueBar[idx].offsetMax = new Vector2(-blueRight, blueBar[idx].offsetMax.y);
            redBar[idx].offsetMin = new Vector2(redLeft, redBar[idx].offsetMin.y);
        }

        // set capture mark
        foreach (var capture in captureHistory)
        {
            float t = (float)capture.CaptureTurn / maxTurn;
            float barWidth = screenWidth - widthMargin * 2;
            float Xpos = Mathf.Lerp(-barWidth / 2, barWidth / 2, t);
            float Ypos;

            foreach (var tagger in capture.CaughtTaggerActorNum)
            {
                Ypos = blueBar[num2idx[tagger]].anchoredPosition.y;

                var taggerMark = Instantiate(markPrefab, graphParent);
                taggerMark.GetComponent<Image>().sprite = taggerSprite;
                taggerMark.GetComponent<RectTransform>().anchoredPosition = new Vector2(Xpos, Ypos);
            }

            Ypos = blueBar[num2idx[capture.CaughtRunnerActorNum]].anchoredPosition.y;

            var runnerMark = Instantiate(markPrefab, graphParent);
            runnerMark.GetComponent<Image>().sprite = runnerSprite;
            runnerMark.GetComponent<RectTransform>().anchoredPosition = new Vector2(Xpos, Ypos);
        }
    }
}
