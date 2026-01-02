using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//捕まった逃げを鬼に変える

public class ConvertCaughtRunnerToTagger : MonoBehaviour
{
    List<(int taggerActorNumber,CaughtRunnerInfo caughttRunnerInfo)> _taggerCaughtRunnerInfos=new List<(int, CaughtRunnerInfo)>();//鬼のactorNumberとCaughtRunnerInfoを格納するリスト

    public void Convert()//捕まった逃げを鬼に変える
    {
        int turn=GameStatsManager.Instance.Turn.GetTurn();

        Dictionary<int, List<int>> caughtRunner_TaggersDic = SetCaughtRunner_TaggersDic();//第1引数:捕まった逃げのActorNumber、第2引数:それを捕まえた鬼達のActorNumber

        int[] keys = new int[caughtRunner_TaggersDic.Keys.Count];
        caughtRunner_TaggersDic.Keys.CopyTo(keys, 0);

        for (int i = 0; i < keys.Length; i++)
        {
            int caughtRunnerActorNum = keys[i];

            if (!caughtRunner_TaggersDic.TryGetValue(caughtRunnerActorNum, out var value)) continue;
            int[] taggerActorNums = value.ToArray();

            //逃げ→鬼にする
            var runnerState=PlayersManager.ActorNumberPlayerInfo(caughtRunnerActorNum).GetComponent<PlayerState>();
            runnerState.ChangeState(EPlayerState.Tagger,ETransformationPlayerStateType.Effect);//エフェクト付きで変化させる

            //履歴に追加
            GameStatsManager.Instance.CaptureHistory.AddHistory(turn, caughtRunnerActorNum, taggerActorNums);
        }
    }

    public void OnEnter()//ターン開始時に呼ぶ
    {
        var playerInfos = PlayersManager.PlayerInfos;

        for (int i = 0; i < playerInfos.Length; i++)
        {
            var playerState = playerInfos[i].GetComponent<PlayerState>();

            if (playerState.State != EPlayerState.Tagger) continue;

            AddCaughtRunnerInfo(playerInfos[i]);
        }
    }

    public void OnExit()//ターン終了時に呼ぶ
    {
        _taggerCaughtRunnerInfos.Clear();
    }

    Dictionary<int, List<int>> SetCaughtRunner_TaggersDic()//第1引数:捕まった逃げのActorNumber、第2引数:それを捕まえた鬼達のActorNumberを入れた辞書型を返す
    {
        Dictionary<int, List<int>> ret=new();

        foreach (var info in _taggerCaughtRunnerInfos)
        {
            if (info.Item2 == null) continue;

            int taggerActorNum = info.Item1;
            int[] caughtRunnerActorNums = info.Item2.CaughtRunnerActorNumbers;

            //捕まった逃げのリストに捕まえた鬼を登録
            for (int i = 0; i < caughtRunnerActorNums.Length; i++)
            {
                int key = caughtRunnerActorNums[i];

                if (!ret.TryGetValue(key, out List<int> caughtTaggerActorNums))
                {
                    //まだListが入っていなかったら作る
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
