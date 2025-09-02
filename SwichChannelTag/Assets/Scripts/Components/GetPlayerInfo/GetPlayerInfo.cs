using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーの情報を取得する

public class GetPlayerInfo : MonoBehaviour
{
    [Tooltip("プレイヤーのオブジェクトのタグ名")] [SerializeField] string _playerObjectTagName;

    PlayerInfo[] _playerInfos;//部屋内全てのプレイヤーの情報
    PlayerInfo _myPlayerInfo;//自分のプレイヤー情報

    //PlayerInfoで返す
    public PlayerInfo MyPlayerInfo { get { return _myPlayerInfo; } }
        
    public PlayerInfo[] PlayerInfos { get { return _playerInfos; } }

    //GameObjectで返す
    public GameObject MyPlayerObject { get { return _myPlayerInfo.PlayerObject; } }

    public GameObject[] PlayerObjects 
    {
        get
        {
            GameObject[] ret = new GameObject[_playerInfos.Length];

            for(int i=0; i<ret.Length ;i++)
            {
                ret[i]= _playerInfos[i].PlayerObject;
            }

            return ret; 
        } 
    }


    //private
    private void Awake()
    {
        //シーン内の全てのプレイヤータグがついたオブジェクトを取得
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag(_playerObjectTagName);

        //プレイヤーの人数分配列を作る
        _playerInfos = new PlayerInfo[playerObjects.Length];

        for(int i=0; i< playerObjects.Length ;i++)
        {
            //そのプレイヤーオブジェクトのオーナー(プレイヤー)を取得
            PhotonView photonView = playerObjects[i].GetPhotonView();
            Player player = photonView.Owner;

            _playerInfos[i] = new PlayerInfo(playerObjects[i],player);

            //自分のなら自分のプレイヤー情報に登録
            if(photonView.IsMine)
            {
                _myPlayerInfo = _playerInfos[i];
            }
        }

        //配列に全て格納した後、actorNumberの小さい順に並べる
        System.Array.Sort(_playerInfos, (a, b) => a.ActorNum.CompareTo(b.ActorNum));
    }
}
