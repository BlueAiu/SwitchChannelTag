using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マップの位置情報(位置と階層を構造体としてまとめたもの)

[System.Serializable]
public struct MapPosInfo
{
    [Tooltip("階層番号")][SerializeField] int _hierarchyIndex;
    [Tooltip("位置")][SerializeField] MapVec _pos;

    public int HierarchyIndex { get { return _hierarchyIndex; } }
    public MapVec Pos { get { return _pos; } }
}
