using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine;

//作成者:杉山
//ゲーム統計情報のターンの値

public class Turn_GameStats
{
    private const string TURN_KEY = "TurnNum";
    const int _defaultTurnNum = 1;
    const int _invalidTurnNum = -1;

    public void OnJoinedRoom()
    {
        Init();
    }

    public void SetTurn(int newTurnNum)//ターン数を設定
    {
        Hashtable props = new Hashtable();
        props[TURN_KEY] = newTurnNum;
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }

    public int GetTurn()//ターン数を取得
    {
        if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(TURN_KEY))
        {
            return (int)PhotonNetwork.CurrentRoom.CustomProperties[TURN_KEY];
        }

        Debug.Log("ターン数の取得に失敗");
        return _invalidTurnNum;
    }

    void Init()//初期化
    {
        if (!PhotonNetwork.IsMasterClient) return;

        SetTurn(_defaultTurnNum);
    }
}
