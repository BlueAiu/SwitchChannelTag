using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//作成者:杉山
//シーン開始時にプレイヤー達のコンポーネントの初期化処理を行う

public class InitScenePlayers : MonoBehaviour
{
    [Tooltip("移動できる階層一覧")][SerializeField] 
    Maps_Hierarchies _hierarchies;

    [SerializeField]
    int playersGap = 3;
    

    SetTransform[] _setTransforms;
    MapTransform[] _mapTrses;
    PlayerState[] _states;

    MapPos[] _posTemp;

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
        _states = PlayersManager.GetComponentsFromPlayers<PlayerState>();

        for(int i=0; i<_mapTrses.Length;i++)
        {
            MapTransform mapTrs = _mapTrses[i];

            if(mapTrs==null) continue;

            mapTrs.Hierarchies = _hierarchies;
        }
    }

    void InitPlayersPos()//位置(Transform)の初期化
    {
        Player mine = PlayersManager.MinePlayerPhotonPlayer;

        if (!mine.IsMasterClient) return;//ホスト主以外はこの処理を行わない

        _posTemp = new MapPos[_mapTrses.Length];
        InitMapHierarchy();
        InitMapVec();

        for (int i = 0; i < _posTemp.Length; i++)
        {
            _mapTrses[i].Rewrite(_posTemp[i]);
        }

        for (int i=0; i<_setTransforms.Length ;i++)
        {
            MapTransform mapTrs = _mapTrses[i];
            
            if (mapTrs == null) continue;

            _setTransforms[i].Position=mapTrs.CurrentWorldPos;
        }

        CheckIsInitManager.Instance.CompletedInit();//初期化処理を完了したことを伝える
    }

    void InitMapHierarchy()
    {
        int taggerIndex = Random.Range(0, _hierarchies.Length);

        for(int i=0;i<_states.Length ;i++)
        {
            if (_states[i].State == EPlayerState.Tagger)
            {
                _posTemp[i].hierarchyIndex = taggerIndex;
            }
            else
            {
                int runnerIndex = Random.Range(0, _hierarchies.Length - 1);
                runnerIndex = MathfExtension.CircularWrapping_Delta(runnerIndex, taggerIndex + 1, _hierarchies.Length - 1);
                _posTemp[i].hierarchyIndex = runnerIndex;
            }
        }
    }

    void InitMapVec()
    {
        List<MapVec> mapVecs = new();
        for (int i = 0; i < _hierarchies[0].MapSize_X; i++)
        {
            for(int j = 0; j < _hierarchies[0].MapSize_Y; j++)
            {
                mapVecs.Add(new MapVec(i, j));
            }
        }
        
        for (int i = 0; i < _mapTrses.Length; i++)
        {
            var spawnPos = mapVecs[Random.Range(0, mapVecs.Count)];
            _posTemp[i].gridPos = spawnPos;

            // remove around spawnPos
            for(int x = -playersGap;x <= playersGap; x++)
            {
                for(int y = -playersGap;y <= playersGap; y++)
                {
                    var del = spawnPos + new MapVec(x, y);
                    mapVecs.Remove(del);
                }
            }
        }
    }
}
