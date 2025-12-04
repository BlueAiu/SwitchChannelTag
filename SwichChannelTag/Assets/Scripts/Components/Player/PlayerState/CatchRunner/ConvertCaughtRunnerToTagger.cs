using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//•ß‚Ü‚Á‚½“¦‚°‚ğ‹S‚É•Ï‚¦‚é

public class ConvertCaughtRunnerToTagger : MonoBehaviour
{
    List<(int taggerActorNumber,CaughtRunnerInfo caughttRunnerInfo)> _taggerCaughtRunnerInfos=new List<(int, CaughtRunnerInfo)>();//‹S‚ÌactorNumber‚ÆCaughtRunnerInfo‚ğŠi”[‚·‚éƒŠƒXƒg

    public void Convert()//•ß‚Ü‚Á‚½“¦‚°‚ğ‹S‚É•Ï‚¦‚é
    {
        int turn=GameStatsManager.Instance.Turn.GetTurn();

        Dictionary<int, List<int>> caughtRunner_TaggersDic = SetCaughtRunner_TaggersDic();//‘æ1ˆø”:•ß‚Ü‚Á‚½“¦‚°‚ÌActorNumberA‘æ2ˆø”:‚»‚ê‚ğ•ß‚Ü‚¦‚½‹S’B‚ÌActorNumber

        int[] keys = new int[caughtRunner_TaggersDic.Keys.Count];
        caughtRunner_TaggersDic.Keys.CopyTo(keys, 0);

        for (int i = 0; i < keys.Length; i++)
        {
            int caughtRunnerActorNum = keys[i];

            if (!caughtRunner_TaggersDic.TryGetValue(caughtRunnerActorNum, out var value)) continue;
            int[] taggerActorNums = value.ToArray();

            //“¦‚°¨‹S‚É‚·‚é
            var runnerState=PlayersManager.ActorNumberPlayerInfo(caughtRunnerActorNum).GetComponent<PlayerState>();
            runnerState.ChangeState(EPlayerState.Tagger);

            //—š—ğ‚É’Ç‰Á
            GameStatsManager.Instance.CaptureHistory.AddHistory(turn, caughtRunnerActorNum, taggerActorNums);
        }
    }

    public void OnEnter()//ƒ^[ƒ“ŠJn‚ÉŒÄ‚Ô
    {
        var playerInfos = PlayersManager.PlayerInfos;

        for (int i = 0; i < playerInfos.Length; i++)
        {
            var playerState = playerInfos[i].GetComponent<PlayerState>();

            if (playerState.State != EPlayerState.Tagger) continue;

            AddCaughtRunnerInfo(playerInfos[i]);
        }
    }

    public void OnExit()//ƒ^[ƒ“I—¹‚ÉŒÄ‚Ô
    {
        _taggerCaughtRunnerInfos.Clear();
    }

    Dictionary<int, List<int>> SetCaughtRunner_TaggersDic()//‘æ1ˆø”:•ß‚Ü‚Á‚½“¦‚°‚ÌActorNumberA‘æ2ˆø”:‚»‚ê‚ğ•ß‚Ü‚¦‚½‹S’B‚ÌActorNumber‚ğ“ü‚ê‚½«‘Œ^‚ğ•Ô‚·
    {
        Dictionary<int, List<int>> ret=new();

        foreach (var info in _taggerCaughtRunnerInfos)
        {
            if (info.Item2 == null) continue;

            int taggerActorNum = info.Item1;
            int[] caughtRunnerActorNums = info.Item2.CaughtRunnerActorNumbers;

            //•ß‚Ü‚Á‚½“¦‚°‚ÌƒŠƒXƒg‚É•ß‚Ü‚¦‚½‹S‚ğ“o˜^
            for (int i = 0; i < caughtRunnerActorNums.Length; i++)
            {
                int key = caughtRunnerActorNums[i];

                if (!ret.TryGetValue(key, out List<int> caughtTaggerActorNums))
                {
                    //‚Ü‚¾List‚ª“ü‚Á‚Ä‚¢‚È‚©‚Á‚½‚çì‚é
                    caughtTaggerActorNums = new List<int>();
                    ret.Add(key, caughtTaggerActorNums);
                }

                caughtTaggerActorNums.Add(taggerActorNum);
            }
        }

        return ret;
    }

    void AddCaughtRunnerInfo(PlayerInfo playerInfo)
    {
        int actorNumber = playerInfo.Player.ActorNumber;
        CaughtRunnerInfo caughtRunnerInfo = playerInfo.GetComponent<CaughtRunnerInfo>();
        _taggerCaughtRunnerInfos.Add((actorNumber, caughtRunnerInfo));
    }
}
