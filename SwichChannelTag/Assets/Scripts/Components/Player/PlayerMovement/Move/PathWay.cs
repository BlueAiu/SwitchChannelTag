using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//移動経路

public class PathWay : MonoBehaviour
{
    Stack<MapVec> _pathVecs = new();

    public MapVec[] MovePath//一番最初に入れた値は現在地になるため、一番最初に入れた値のみ省く
    {
        get
        {
            var arr = _pathVecs.ToArray();
            System.Array.Reverse(arr);
            return arr.Skip(1).ToArray();
        }
    }

    public void Push(MapVec newGridPos)
    {
        _pathVecs.Push(newGridPos);
    }

    public MapVec Pop()
    {
        return _pathVecs.Pop();
    }

    public bool IsOneStepBefore(MapVec newGridPos)//一歩前の位置かを判別する
    {
        if (_pathVecs.Count == 0) return false;
        return _pathVecs.Peek() == newGridPos;
    }

    public void ClearPath()
    {
        _pathVecs.Clear();
    }
}
