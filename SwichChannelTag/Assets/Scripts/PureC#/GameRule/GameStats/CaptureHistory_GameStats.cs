using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲーム統計情報の捕まえた履歴
//追加する要素を全員に送って、リストへの追加は各々する

public class CaptureHistory_GameStats
{
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

        Hashtable hash = CaptureRecordToHash(newRecord);

        PhotonNetwork.RaiseEvent(
        EventCodeDictionary.EVENTCODE_CAPTURECORD,       // イベントコード
        hash,                 // 送るデータ
        new RaiseEventOptions { Receivers = ReceiverGroup.All },
        SendOptions.SendReliable
        );
    }

    //以下はGameStatsManager以外のクラスは使用禁止

    public void OnJoinedRoom()
    {
        _history = new List<CaptureRecord>();
    }

    public void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code != EventCodeDictionary.EVENTCODE_CAPTURECORD) return;

        Hashtable hash = photonEvent.CustomData as Hashtable;

        // 復元できるならデコード
        var data = HashToCaptureRecord(hash);

        if (data == null) return;
        
        _history.Add(data);
    }

    //HashとCaptureRecordの変換系

    Hashtable CaptureRecordToHash(CaptureRecord record)
    {
        var hash = new Hashtable();

        hash[CAPTURE_TURN_KEY] = record.CaptureTurn;
        hash[CAUGHT_RUNNER_ACTOR_NUM_KEY] = record.CaughtRunnerActorNum;
        hash[CAUGHT_TAGGER_ACTOR_NUM_KEY] = record.CaughtTaggerActorNum;

        return hash;
    }

    CaptureRecord HashToCaptureRecord(Hashtable hash)
    {
        try
        {
            if (hash == null)
            {
                Debug.Log("変換に失敗");
                return null;
            }

            // 必須キーが揃っているかチェック（任意）
            if (!hash.TryGetValue(CAPTURE_TURN_KEY,out object value1) ||
                !hash.TryGetValue(CAUGHT_RUNNER_ACTOR_NUM_KEY,out object value2) ||
                !hash.TryGetValue(CAUGHT_TAGGER_ACTOR_NUM_KEY,out object value3))
            {
                Debug.Log("変換に失敗");
                return null;
            }

            int captureTurn = (int)value1;

            return new CaptureRecord((int)value1, (int)value2, (int[])value3);
        }
        catch
        {
            // キャスト失敗した場合などは null を返す
            Debug.Log("変換に失敗");
            return null;
        }
    }
}
