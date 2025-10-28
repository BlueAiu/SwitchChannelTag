using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//シーン開始時にプレイヤー達のコンポーネントの初期化処理を行う

public class InitScenePlayers : MonoBehaviour
{
    [Tooltip("移動できる階層一覧")][SerializeField] 
    Maps_Hierarchies _hierarchies;

    SetTransform[] _setTransforms;
    MapTransform[] _mapTrses;

    void Awake()
    {
        InitMapTrs();

        _setTransforms = PlayersManager.GetComponentsFromPlayers<SetTransform>();
    }

    private void Start()
    {
        InitPlayersPos();
    }

    void InitMapTrs()//全プレイヤーのMapTransformの初期化
    {
        _mapTrses = PlayersManager.GetComponentsFromPlayers<MapTransform>();

        SetupPlayerState.SelectTagger();

        for(int i=0; i<_mapTrses.Length;i++)
        {
            MapTransform mapTrs = _mapTrses[i];

            if(mapTrs==null) continue;

            mapTrs.Hierarchies = _hierarchies;
        }
    }

    //これより下は後に変更が加わる可能性大

    void InitPlayersPos()//位置(Transform)の初期化
    {
        Player mine = PlayersManager.MinePlayerPhotonPlayer;

        if (!mine.IsMasterClient) return;//ホスト主以外はこの処理を行わない

        for(int i=0; i<_setTransforms.Length ;i++)
        {
            MapTransform mapTrs = _mapTrses[i];
            
            if (mapTrs == null) continue;

            _setTransforms[i].Position=mapTrs.CurrentWorldPos;
        }
    }
}
