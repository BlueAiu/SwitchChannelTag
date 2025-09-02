using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//作成者:杉山
//プレイヤーの情報

public class PlayerInfo
{
    GameObject _playerObject;//プレイヤーオブジェクト
    Player _player;//プレイヤーの情報
    const int _errorNum = -1;

    public PlayerInfo(GameObject playerObject,Player player)
    {
        _playerObject = playerObject;
        _player = player;
    }

    public GameObject PlayerObject//プレイヤーオブジェクト
    {
        get { return _playerObject; }
        set { _playerObject = value; }
    }

    public Player Player//プレイヤーの情報
    {
        get { return _player; }
        set { _player = value; }
    }

    public int ActorNum//プレイヤーのアクタ―ナンバー(入室した順に並んだプレイヤーごとのIDみたいなもの)、取得に失敗したら_errorNum(-1)を返す
    {
        get
        {
            if (_player == null) return _errorNum;
            return _player.ActorNumber;
        }
    }

    public string ID//プレイヤーID(文字列型のプレイヤーごとのID)、取得に失敗したらnullを返す
    {
        get
        {
            if (_player == null) return null;
            return _player.UserId;
        }
    }
}
