using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class Pun_JoinGame : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Start Join Room...");
    }

    void OnGUI()
    {
        // ログインの状態をGUIに出力
        //GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
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
        Debug.Log("Joined Room.");
    }
}

