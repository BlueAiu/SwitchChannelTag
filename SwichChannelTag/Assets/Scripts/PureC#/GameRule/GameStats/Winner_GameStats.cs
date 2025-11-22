using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ゲーム統計情報の勝者

public class Winner_GameStats
{
    private const string WINNER_KEY = "Winner";

    public event Action<EPlayerState> OnUpdateWinner;

    public void SetWinner(EPlayerState? winner)
    {
        Hashtable props = new Hashtable();
        props[WINNER_KEY] = winner;
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }

    public EPlayerState? GetWinner()//勝者が決まっていない場合、nullが返ってくる
    {
        if(PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("値を正しく取得できませんでした");
            return null;
        }


        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(WINNER_KEY,out object value))
        {
            return (EPlayerState)value;
        }
        else
        {
            return null;
        }
    }

    public void OnJoinedRoom()
    {
        Init();
    }

    //値変更を通知
    public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.TryGetValue(WINNER_KEY, out object value))
        {
            OnUpdateWinner?.Invoke((EPlayerState)value);
        }
    }

    void Init()//初期化
    {
        if (!PhotonNetwork.IsMasterClient) return;

        SetWinner(null);
    }
}
