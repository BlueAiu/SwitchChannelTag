using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//シーン開始時にプレイヤー達のコンポーネントの初期化処理を行う

public class InitScenePlayers : MonoBehaviour
{
    [Tooltip("移動できる階層一覧")][SerializeField] 
    Maps_Hierarchies _hierarchies;

    void Start()
    {
        InitMapTrs();
    }

    void InitMapTrs()//全プレイヤーのMapTransformの初期化
    {
        MapTransform[] mapTrses = PlayersManager.GetComponentsFromPlayers<MapTransform>();

        for(int i=0; i<mapTrses.Length;i++)
        {
            MapTransform mapTrs = mapTrses[i];
            if (mapTrs != null) mapTrs.Hierarchies = _hierarchies;
            mapTrs.Rewrite(mapTrs.Pos, mapTrs.HierarchyIndex, true);//位置を初期化
        }
    }
}
