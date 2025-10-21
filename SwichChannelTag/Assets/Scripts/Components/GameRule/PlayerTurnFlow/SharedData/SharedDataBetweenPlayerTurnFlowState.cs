using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//PlayerTurnFlowステート間で共有するデータ

public class SharedDataBetweenPlayerTurnFlowState
{
    private int _destinationHierarchyIndex;

    private bool _isChangedHierarchy = false;

    public SharedDataBetweenPlayerTurnFlowState() { }

    public int DestinationHierarchyIndex//移動先階層番号
    {
        get { return _destinationHierarchyIndex; }
        set { _destinationHierarchyIndex = value; }
    }

    public bool IsChangedHierarchy { get { return _isChangedHierarchy; } }//階層移動したか

    public void ChangedHierarchy()//階層移動をした
    {
        _isChangedHierarchy = true;
    }

    public void Reset()//データを初期化(ターンが変わるごとにする)
    {
        _isChangedHierarchy = false;
    }
    
}
