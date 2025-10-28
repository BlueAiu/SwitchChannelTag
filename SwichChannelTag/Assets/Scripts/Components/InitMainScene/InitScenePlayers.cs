using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�V�[���J�n���Ƀv���C���[�B�̃R���|�[�l���g�̏������������s��

public class InitScenePlayers : MonoBehaviour
{
    [Tooltip("�ړ��ł���K�w�ꗗ")][SerializeField] 
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

    void InitMapTrs()//�S�v���C���[��MapTransform�̏�����
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
}
