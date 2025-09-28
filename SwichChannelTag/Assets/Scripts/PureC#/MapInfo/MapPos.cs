using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マップの位置情報(位置と階層を構造体としてまとめたもの)

[System.Serializable]
public struct MapPos
{
    public int hierarchyIndex;//階層番号
    public MapVec pos;//位置

    public MapPos(int hierarchyIndex, MapVec pos)
    {
        this.hierarchyIndex = hierarchyIndex;
        this.pos = pos;
    }


    //演算子
    public static bool operator ==(MapPos pos1, MapPos pos2)//==演算子オーバーロード
    {
        return (pos1.hierarchyIndex == pos2.hierarchyIndex) && (pos1.pos == pos2.pos);
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
        return System.HashCode.Combine(hierarchyIndex, pos);
    }
}
