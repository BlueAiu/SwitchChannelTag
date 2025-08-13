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
    

    public Map_A_Hierarchy Map//どのマップ上を動くか
    {
        get { return _map; }
    }

    public Transform Target//動かす対象
    {
        get { return _target; }
    }

    public bool Move(ref MapVec currentPos,Vector2 inputVec)//指定方向に移動(移動に失敗したらfalseを返す)
    {
        MapVec moveVec;
        moveVec.x = (int)inputVec.x;
        moveVec.y = -(int)inputVec.y;

        MapVec newPos = currentPos + moveVec;

        if (!_map.IsInRange(newPos) || _map.Mass[newPos] != E_Mass.Empty) return false;//移動できない場合

        RewritePos(out currentPos,newPos);
        return true;
    }

    public void RewritePos(out MapVec currentPos, MapVec newMapVec,Map_A_Hierarchy newMap)//位置とマップの書き換え(ワープ的なやつ)
    {
        _map = newMap;
        RewritePos(out currentPos, newMapVec);
    }

    public void RewritePos(out MapVec currentPos,MapVec newMapVec)//位置の書き換え(ワープ的なやつ)
    {
        currentPos = _map.ClampInRange(newMapVec);//範囲外にはみ出さない処置だけする(空マスの判定はしない)
        Vector3 newPos = _map.MapToWorld(currentPos);
        _target.position = newPos;
    }
}
