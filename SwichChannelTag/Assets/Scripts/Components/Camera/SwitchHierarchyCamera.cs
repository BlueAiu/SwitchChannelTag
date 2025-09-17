using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//階層を移すカメラの切り替え

public class SwitchHierarchyCamera : MonoBehaviour
{
    [Tooltip("階層ごとのカメラ中心点\n階層の個数分設定してください")] [SerializeField] Transform[] _mapCenters;
    [Tooltip("カメラ")] [SerializeField] CinemachineVirtualCamera _mapCamera;
    [SerializeField] ChangeHierarchy _changeHierarchy;
    [SerializeField] Maps_Hierarchies _maps_Hierarchies;
    [SerializeField] ScenePlayerManager _scenePlayerManager;
    

    public void Switch(int hierarchyIndex)//写す階層の切り替え
    {
        if (!IsValid_MapCentersLength()) return;

        if(!_maps_Hierarchies.IsInRange(hierarchyIndex))
        {
            Debug.Log("範囲外の階層番号です！");
            return;
        }

        Transform currentHierarchy=_mapCenters[hierarchyIndex];

        _mapCamera.Follow=currentHierarchy;
        _mapCamera.LookAt=currentHierarchy;
    }



    private void Awake()
    {
        IsValid_MapCentersLength();
        _changeHierarchy.OnSwitchHierarchy_NewIndex += Switch;
    }

    private void Start()
    {
        InitCamera();
    }

    void InitCamera()//カメラの初期化処理
    {
        //プレイヤーの初期位置にカメラを合わせる
        MapTransform myMapTrs = _scenePlayerManager.MyComponent<MapTransform>();
        Switch(myMapTrs.HierarchyIndex);
    }

    bool IsValid_MapCentersLength()
    {
        if(_mapCenters.Length != _maps_Hierarchies.Length)
        {
            Debug.Log("階層の数分、中心点が設定されていません！");
            return false;
        }

        return true;
    }
}
