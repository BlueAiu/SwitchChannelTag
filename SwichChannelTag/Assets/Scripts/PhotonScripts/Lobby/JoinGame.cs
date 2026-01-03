using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class JoinGame : MonoBehaviourPunCallbacks
{
    [SerializeField] string roomName = "room";
    [SerializeField] TMP_Text join_Text;
    [SerializeField] bool writeJoinLog = true;

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            WriteLog("Join Server...");
        }
    }

    // ルームに入室前
    public override void OnConnectedToMaster()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
        WriteLog("Start Join Room...");
    }

    // ルームに入室後
    public override void OnJoinedRoom()
    {
        WriteLog("Joined Room.");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        string failedStr = "JoinFailed : " + message;
        WriteLog(failedStr);
        join_Text.text = failedStr;
    }

    void WriteLog(string message)
    {
        if (writeJoinLog) Debug.Log(message);
    }
}

