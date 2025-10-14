using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//階層を移すカメラの切り替え

public class SwitchHierarchyCamera : MonoBehaviour
{
    [Tooltip("階層ごとのバーチャルカメラ\n0個目から天国->地上->地獄の順に入れてください")] [SerializeField] 
    CinemachineVirtualCamera[] _mapVCams;

    [SerializeField] 
    ChangeHierarchy _changeHierarchy;

    [SerializeField]
    Maps_Hierarchies _maps_Hierarchies;

    public void Switch(int hierarchyIndex)//写す階層の切り替え
    {
        if (!IsValid_VCamLength()) return;

        if(!_maps_Hierarchies.IsInRange(hierarchyIndex))
        {
            Debug.Log("範囲外の階層番号です！");
            return;
        }

        for(int i=0; i<_mapVCams.Length ;i++)
        {
            _mapVCams[i].enabled = (i == hierarchyIndex);
        }
    }



    //private
    private void Awake()
    {
        IsValid_VCamLength();
        _changeHierarchy.OnSwitchHierarchy_NewIndex += Switch;
    }

    private void Start()
    {
        InitCamera();
    }

    void InitCamera()//カメラの初期化処理
    {
        //プレイヤーの初期位置にカメラを合わせる
        MapTransform myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        Switch(myMapTrs.Pos.hierarchyIndex);
    }

    bool IsValid_VCamLength()
    {
        if(_mapVCams.Length != _maps_Hierarchies.Length)
        {
            Debug.Log("階層の数分、カメラが設定されていません！");
            return false;
        }

        return true;
    }
}
