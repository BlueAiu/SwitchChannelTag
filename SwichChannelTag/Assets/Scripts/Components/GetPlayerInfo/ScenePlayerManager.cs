using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//そのシーン内の全プレイヤーの情報を取得することができる

//*使用上の注意*
//Awake段階ではシーン内の全プレイヤーの把握処理を行っているので、
//プレイヤーの情報取得はStartでやるようにしてください

public class ScenePlayerManager : MonoBehaviour
{
    [Tooltip("プレイヤーのオブジェクトのタグ名")] [SerializeField] string _playerObjectTagName;
    PlayerInfo[] _playerInfos;//全プレイヤーの情報
    int _myPlayerIndex;//自分のプレイヤーの要素番号

    
    public int MyPlayerIndex { get { return _myPlayerIndex; } }//自分のプレイヤー番号


    //自分のプレイヤーの情報取得関係
    public Player MyPlayer { get { return MyPlayerInfo.Player; } }//Playerの取得

    public GameObject MyPlayerObject { get { return MyPlayerInfo.PlayerObject; } }//オブジェクトの取得

    public T MyComponent<T>() where T : Component { return MyPlayerInfo.GetComponent<T>(); }//コンポーネントの取得



    //全プレイヤーの情報取得関係
    public Player[] Players //Playerの取得
    {
        get
        {
            Player[] ret = new Player[_playerInfos.Length];

            for(int i=0; i< _playerInfos.Length; i++)
            {
                ret[i]=_playerInfos[i].Player;
            }

            return ret;
        
        }
    }

    public GameObject[] PlayerObjects//オブジェクトの取得
    {
        get
        {
            GameObject[] ret = new GameObject[_playerInfos.Length];

            for(int i=0; i<_playerInfos.Length ;i++)
            {
                ret[i] = _playerInfos[i].PlayerObject;
            }

            return ret;
        }
    }

    public T[] PlayerComponents<T>() where T:Component//コンポーネントの取得
    {
        T[] ret = new T[_playerInfos.Length];

        for(int i = 0; i < _playerInfos.Length; i++)
        {
            ret[i] = _playerInfos[i].GetComponent<T>();
        }

        return ret;
    }



    //private
    PlayerInfo MyPlayerInfo { get { return _playerInfos[_myPlayerIndex]; } }

    private void Awake()
    {
        InitPlayerInfos();
    }

    void InitPlayerInfos()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag(_playerObjectTagName);

        _playerInfos = new PlayerInfo[playerObjects.Length];

        //全プレイヤーの情報を取得して格納
        for (int i = 0; i < playerObjects.Length; i++)
        {
            GameObject playerObject = playerObjects[i];

            //そのプレイヤーオブジェクトのオーナー(プレイヤー)を取得
            PhotonView photonView = playerObject.GetPhotonView();
            if (photonView == null) Debug.Log(playerObject.name+"にPhotonViewがついてません！");
            Player player = photonView.Owner;

            //そのプレイヤーの情報を取得する機能を取得
            GetPlayerInfo getPlayerInfo= playerObject.GetComponent<GetPlayerInfo>();
            if (getPlayerInfo == null) Debug.Log(playerObject.name + "にGetPlayerInfoがついてません！");

            _playerInfos[i] = new PlayerInfo(playerObject, player,getPlayerInfo);
        }


        //配列に全て格納した後、actorNumberの小さい順に並べる
        System.Array.Sort(_playerInfos, (a, b) => a.Player.ActorNumber.CompareTo(b.Player.ActorNumber));


        //自分のプレイヤーの要素番号を探す
        for (int i = 0; i < playerObjects.Length; i++)
        {
            if (_playerInfos[i].Player == PhotonNetwork.LocalPlayer)
            {
                _myPlayerIndex = i;
                return;
            }
        }
    }
}
