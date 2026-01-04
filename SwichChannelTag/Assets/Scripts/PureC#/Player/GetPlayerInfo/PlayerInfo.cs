using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//作成者:杉山
//プレイヤーの情報

public class PlayerInfo
{
    GetPlayerInfo _getPlayerInfo;//プレイヤーのコンポーネントを取得する機能
    GameObject _playerObject;//プレイヤーオブジェクト
    Player _player;//プレイヤーの情報

    public PlayerInfo(GameObject playerObject,Player player, GetPlayerInfo getPlayerInfo)
    {
        _playerObject = playerObject;
        _player = player;
        _getPlayerInfo = getPlayerInfo;
    }

    public GameObject PlayerObject { get { return _playerObject; } }//プレイヤーオブジェクト
    public Player Player { get { return _player; } }//プレイヤーの情報
    public T GetComponent<T>() where T: Component//プレイヤーのコンポーネントの取得
    {
        if (_getPlayerInfo == null) return null;

        return _getPlayerInfo.GetComponent<T>(); 
    }
}
