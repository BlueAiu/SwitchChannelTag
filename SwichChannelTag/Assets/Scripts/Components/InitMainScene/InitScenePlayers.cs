using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//�쐬��:���R
//�V�[���J�n���Ƀv���C���[�B�̃R���|�[�l���g�̏������������s��

public class InitScenePlayers : MonoBehaviour
{
    [Tooltip("�ړ��ł���K�w�ꗗ")][SerializeField] 
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

    void InitMapTrs()//�S�v���C���[��MapTransform�̏�����
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

        _posTemp = new MapPos[_mapTrses.Length];
        InitMapHierarchy();
        InitMapVec();

        for(int i=0;i<_posTemp.Length;i++)
        {
            _mapTrses[i].Rewrite(_posTemp[i]);
        }
    }

    //�����艺�͌�ɕύX�������\����

    void InitPlayersPos()//�ʒu(Transform)�̏�����
    {
        Player mine = PlayersManager.MinePlayerPhotonPlayer;

        if (!mine.IsMasterClient) return;//�z�X�g��ȊO�͂��̏������s��Ȃ�

        for(int i=0; i<_setTransforms.Length ;i++)
        {
            MapTransform mapTrs = _mapTrses[i];
            
            if (mapTrs == null) continue;

            _setTransforms[i].Position=mapTrs.CurrentWorldPos;
        }
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
