using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マップ上の座標(位置と階層を構造体としてまとめたもの)

[System.Serializable]
public struct MapPos
{
    public int hierarchyIndex;//階層番号
    public MapVec gridPos;//盤面上の位置

    public MapPos(int hierarchyIndex, MapVec pos)
    {
        this.hierarchyIndex = hierarchyIndex;
        this.gridPos = pos;
    }

    public MapPos(int hierarchyIndex, int x,int y)
    {
        this.hierarchyIndex = hierarchyIndex;
        this.gridPos.x = x;
        this.gridPos.y = y;
    }


    //演算子
    public static bool operator ==(MapPos pos1, MapPos pos2)//==演算子オーバーロード
    {
        return (pos1.hierarchyIndex == pos2.hierarchyIndex) && (pos1.gridPos == pos2.gridPos);
    }

    public static bool operator !=(MapPos pos1, MapPos pos2)//!=演算子オーバーロード
    {
        return !(pos1 == pos2);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is MapPos)) return false;
        MapPos other = (MapPos)obj;
        return this == other;
    }

    public override int GetHashCode()
    {
        return System.HashCode.Combine(hierarchyIndex, gridPos);
    }
}
