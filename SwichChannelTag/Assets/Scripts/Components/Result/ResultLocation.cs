using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultLocation : MonoBehaviour
{
    [SerializeField] Transform[] upperPoints;
    [SerializeField] Transform[] lowerPoints;

    void Start()
    {
        var players = GetTransformEachState();

        if (players[EPlayerState.Runner].Count > 0)
            RunnerWinLocation(players);
        else
            TaggerWinLocation();
    }

    Dictionary<EPlayerState, List<Transform>> GetTransformEachState()
    {
        var players = PlayersManager.GetComponentsFromPlayers<PlayerState>();
        Dictionary<EPlayerState, List<Transform>> playerTrs = new();
        playerTrs[EPlayerState.Tagger] = new();
        playerTrs[EPlayerState.Runner] = new();

        for (int i = 0; i < players.Length; i++)
        {
            var player = players[i];
            playerTrs[player.State].Add(player.transform);
        }

        return playerTrs;
    }

    void RunnerWinLocation(Dictionary<EPlayerState, List<Transform>> players)
    {
        var runners = players[EPlayerState.Runner];
        for (int i = 0; i < runners.Count; i++) 
        {
            runners[i].position = upperPoints[i].position;
        }

        var taggers = players[EPlayerState.Tagger];
        for (var i = 0; i <= taggers.Count; i++)
        {
            taggers[i].position = lowerPoints[i].position;
        }
    }

    void TaggerWinLocation()
    {
        // split firstTagger and capturedRunners

        var captureHistory = GameStatsManager.Instance.CaptureHistory.GetHistory();

        List<int> capturedRunners = new();
        foreach(var capture in captureHistory)
        {
            capturedRunners.Add(capture.CaughtRunnerActorNum);
        }

        int firstTagger = -1;
        foreach(var ph in PlayersManager.PlayersPhotonPlayer)
        {
            if (!capturedRunners.Contains(ph.ActorNumber))
            {
                firstTagger = ph.ActorNumber;
                break;
            }
        }

        // set position

        PlayersManager.ActorNumberPlayerInfo(firstTagger).
            PlayerObject.transform.position = upperPoints[0].position;

        for (int i = 0; i < capturedRunners.Count; i++) 
        {
            int num = capturedRunners[i];
            PlayersManager.ActorNumberPlayerInfo(num).
                PlayerObject.transform.position = lowerPoints[i].position;
        }
    }
}
