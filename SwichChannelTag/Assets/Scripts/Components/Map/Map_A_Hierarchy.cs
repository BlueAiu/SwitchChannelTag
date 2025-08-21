using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//作成者:杉山
//1階層ごとのマップの管理
//_centerTrsを[0,0]として-Z方向に_mapSize_Y、+X方向に_mapSize_X分の広さのマップを展開
//今後の変更予定内容
//位置を対応させる系のメソッドをクラスとしてまとめる

public class Map_A_Hierarchy : MonoBehaviour
{
    [Tooltip("マップのサイズ")] [SerializeField] MapVec _mapSize;
    [Tooltip("一マスごとの間隔")] [SerializeField] float _gapDistance;
    [Tooltip("[0,0]点の位置となるTransform")] [SerializeField] Transform _centerTrs;

    MassOfMap _mass;//マスの管理

    public MassOfMap Mass { get { return _mass; } }

    //マップのサイズを教える
    public int MapSize_X { get { return _mapSize.x; } }
    public int MapSize_Y { get { return _mapSize.y; } }

    //マス座標が範囲内かを判定
    public bool IsInRange(MapVec mapVec)
    {
        if (mapVec.x < 0 || mapVec.x >= _mapSize.x) return false;

        if (mapVec.y < 0 || mapVec.y >= _mapSize.y) return false;

        return true;
    }

    //マス座標を範囲内に収める
    public MapVec ClampInRange(MapVec mapVec)
    {
        mapVec.x = Mathf.Clamp(mapVec.x, 0, _mapSize.x-1);

        mapVec.y = Mathf.Clamp(mapVec.y, 0, _mapSize.y-1);

        return mapVec;
    }

    //マス座標をワールド座標に変換(変換に失敗したらfalseを返す)
    public Vector3 MapToWorld(MapVec mapVec)
    {
        //範囲外であれば警告する
        if (!IsInRange(mapVec)) Debug.Log("その座標は範囲外です！");

        Vector3 ret = new();

        Vector3 centerVec = _centerTrs.position;
        ret = centerVec;

        ret.x += mapVec.x * _gapDistance;//X方向の計算
        ret.z -= mapVec.y * _gapDistance;//Y方向の計算

        return ret;
    }


    //private

    private void Awake()
    {
        _mass = new MassOfMap(_mapSize);
    }
}
