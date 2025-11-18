using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲーム統計情報の捕まえた履歴

public class CaptureHistory_GameStats
{
    private const string HISTORY_KEY = "History";
    private const string CAPTURE_TURN_KEY = "CaptureTurn";
    private const string CAUGHT_RUNNER_ACTOR_NUM_KEY = "CaughtRunnerActorNum";
    private const string CAUGHT_TAGGER_ACTOR_NUM_KEY = "CaughtTaggerActorNum";


    List<CaptureRecord> _history;

    public CaptureRecord[] GetHistory()//履歴の取得
    {
        return _history.ToArray();
    }

    public void AddHistory(int captureTurn, int caughtRunnerActorNum, int[] caughtTaggerActorNum)//履歴の追加
    {
        CaptureRecord newRecord = new CaptureRecord(captureTurn, caughtRunnerActorNum, caughtTaggerActorNum);

        _history.Add(newRecord);

        var tableList = new Hashtable[_history.Count];

        for (int i = 0; i < _history.Count; i++)
        {
            var t = new Hashtable();
            t[CAPTURE_TURN_KEY] = _history[i].CaptureTurn;
            t[CAUGHT_RUNNER_ACTOR_NUM_KEY] = _history[i].CaughtRunnerActorNum;
            t[CAUGHT_TAGGER_ACTOR_NUM_KEY] = _history[i].CaughtTaggerActorNum;
            tableList[i] = t;
        }

        var roomProps = new Hashtable();
        roomProps[HISTORY_KEY] = tableList;
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);
    }

    public void OnJoinedRoom()
    {
        _history = new List<CaptureRecord>();
    }

    public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        // 履歴部分が変更されてなければ無視
        if (!propertiesThatChanged.ContainsKey(HISTORY_KEY)) return;

        // 履歴データを受け取る
        var tableList = propertiesThatChanged[HISTORY_KEY] as Hashtable[];
        
        if (tableList == null) return;

        var newHistory = new List<CaptureRecord>();

        foreach (var t in tableList)
        {
            // 個々の値を取り出し
            int captureTurn = (int)t[CAPTURE_TURN_KEY];
            int caughtRunnerActorNum = (int)t[CAUGHT_RUNNER_ACTOR_NUM_KEY];
            int[] caughtTaggerActorNum = t[CAUGHT_TAGGER_ACTOR_NUM_KEY] as int[];

            // CaptureRecord へ復元
            var record = new CaptureRecord(
                captureTurn,
                caughtRunnerActorNum,
                caughtTaggerActorNum
            );

            newHistory.Add(record);
        }

        _history = newHistory;
    }
}
