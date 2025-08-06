using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class JoinGame : MonoBehaviourPunCallbacks
{
    [SerializeField] bool writeJoinLog = true;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        WriteLog("Start Join Room...");
    }

    // ルームに入室前
    public override void OnConnectedToMaster()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    // ルームに入室後
    public override void OnJoinedRoom()
    {
        WriteLog("Joined Room.");
    }

    void WriteLog(string message)
    {
        if (writeJoinLog) Debug.Log(message);
    }
}

