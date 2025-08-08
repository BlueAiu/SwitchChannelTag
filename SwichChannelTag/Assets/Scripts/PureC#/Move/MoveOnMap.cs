using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マップ上を移動する

[System.Serializable]
public class MoveOnMap
{
    [Tooltip("どのマップ上を動くか")] [SerializeField] Map_A_Hierarchy _map;
    [Tooltip("動かす対象")] [SerializeField] Transform _target;
    [Tooltip("初期位置")][SerializeField] MapVec _startPoint;

    private MapVec _currentPos;

    public Map_A_Hierarchy Map//どのマップ上を動くか
    {
        get { return _map; }
        set { _map = value; }
    }

    public Transform Target//動かす対象
    {
        get { return _target; }
        set { _target = value; }
    }

    public MapVec CurrentPos { get { return _currentPos; } }//現在の位置

    public void Start()//Start関数で呼び出す
    {
        //位置の初期化
        RewritePos(_startPoint);
    }

    public bool Move(Vector2 inputVec)//移動(移動に失敗したらfalseを返す)
    {
        MapVec moveVec;
        moveVec.x = (int)inputVec.x;
        moveVec.y = (int)inputVec.y;

        MapVec newPos = _currentPos + moveVec;

        if (!_map.IsInRange(newPos)) return false;//移動できない場合

        RewritePos(newPos);
        return true;
    }

    void RewritePos(MapVec newMapVec)//位置の書き換え
    {
        _currentPos = _map.ClampInRange(newMapVec);//範囲外の位置に行かないようにするための処置
        Vector3 newPos = _map.MapToWorld(_currentPos);
        _target.position = newPos;
    }
}
