using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//•ß‚Ü‚Á‚½“¦‚°‚ğ‹S‚É•Ï‚¦‚é

public class ConvertCaughtRunnerToTagger : MonoBehaviour
{
    List<(int taggerActorNumber,CaughtRunnerInfo caughttRunnerInfo)> _taggerCaughtRunnerInfos=new List<(int, CaughtRunnerInfo)>();//‹S‚ÌactorNumber‚ÆCaughtRunnerInfo‚ğŠi”[‚·‚éƒŠƒXƒg

    public void Convert(int turn)
    {
        Dictionary<int, List<int>> caughtRunnerDic = new();//‘æ1ˆø”:•ß‚Ü‚Á‚½“¦‚°‚ÌActorNumberA‘æ2ˆø”:•ß‚Ü‚¦‚½‹S‚ÌActorNumber

        foreach(var info in _taggerCaughtRunnerInfos)
        {
            if(info.Item2==null) continue;

            int taggerActorNum=info.Item1;
            int[] caughtRunnerActorNums = info.Item2.CaughtRunnerActorNumbers;

            //•ß‚Ü‚¦‚½—š—ğ‚É“o˜^

            //•ß‚Ü‚Á‚½“¦‚°‚ÌƒŠƒXƒg‚É•ß‚Ü‚¦‚½‹S‚ğ“o˜^
            for(int i=0; i < caughtRunnerActorNums.Length; i++)
            {
                int key=caughtRunnerActorNums[i];
                
                if(!caughtRunnerDic.TryGetValue(key,out List<int> caughtTaggerActorNums))
                {
                    //‚Ü‚¾List‚ª“ü‚Á‚Ä‚¢‚È‚©‚Á‚½‚çì‚é
                    caughtTaggerActorNums=new List<int>();
                    caughtRunnerDic.Add(key, caughtTaggerActorNums);
                }

                caughtTaggerActorNums.Add(taggerActorNum);
            }

            int[] keys= new int[caughtRunnerDic.Keys.Count];
            caughtRunnerDic.Keys.CopyTo(keys, 0);

            for(int i=0; i<keys.Length ;i++)
            {
                int caughtRunnerActorNum=keys[i];

            }

            //GameStatsManager.Instance.CaptureHistory.AddHistory
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

    void AddCaughtRunnerInfo(PlayerInfo playerInfo)
    {
        int actorNumber = playerInfo.Player.ActorNumber;
        CaughtRunnerInfo caughtRunnerInfo = playerInfo.GetComponent<CaughtRunnerInfo>();
        _taggerCaughtRunnerInfos.Add((actorNumber, caughtRunnerInfo));
    }
}
