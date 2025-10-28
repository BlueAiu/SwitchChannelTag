using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine;

//作成者:杉山
//ゲームの統計情報
//経過ターン数の管理

public class GameStatsManager : MonoBehaviourPunCallbacks
{
    private const string TURN_KEY = "TurnNum";
    const int _invalidTurnNum = -1;

    public static GameStatsManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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
}
