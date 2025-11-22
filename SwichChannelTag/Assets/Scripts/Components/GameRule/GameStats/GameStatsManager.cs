using Photon.Pun;
using System;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine;

//作成者:杉山
//ゲームの統計情報
//経過ターン数の管理

public class GameStatsManager : MonoBehaviourPunCallbacks
{
    public static GameStatsManager Instance { get; private set; }

    Turn_GameStats _turn=new Turn_GameStats();
    Winner_GameStats _winner =new Winner_GameStats();
    CaptureHistory_GameStats _captureHistory = new CaptureHistory_GameStats();

    public Turn_GameStats Turn
    {
        get { return _turn; }
    }

    public Winner_GameStats Winner
    {
        get { return _winner; }
    }

    public CaptureHistory_GameStats CaptureHistory
    {
        get { return _captureHistory; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _captureHistory.OnEnable();
    }

    public override void OnDisable()
    {
        _captureHistory.OnDisable();
        base.OnDisable();
    }

    public override void OnJoinedRoom()
    {
        _turn.OnJoinedRoom();
        _winner.OnJoinedRoom();
        _captureHistory.OnJoinedRoom();
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        _winner.OnRoomPropertiesUpdate(propertiesThatChanged);
    }
}
