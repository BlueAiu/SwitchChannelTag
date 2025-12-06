using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class DecidePath
{
    [SerializeField] SerializableDictionary<EPlayerState, GameObject> _pathObject;

    PlayerState _state;
    Stack<GameObject> _pathObjs = new();
    Stack<MapVec> _pathVecs = new();

    public MapVec[] MovePath
    {
        get
        {
            var arr = _pathVecs.ToArray();
            System.Array.Reverse(arr);
            return arr;
        }
    }

    void InstancePath(MapVec newGridPos, Vector3 dir)
    {
        var rotation = Quaternion.LookRotation(dir);
        var o = Instantiate(_pathObject[_state.State], _cursor.transform.position, rotation);
        _pathObjs.Push(o);
        _pathVecs.Push(newGridPos);
    }

    void UndoPath()
    {
        Destroy(_pathObjs.Pop());
        _pathVecs.Pop();
    }

    void ClearPath()
    {
        while(_pathObjs.Count > 0)
        {
            Destroy(_pathObjs.Pop());
        }

        _pathVecs.Clear();
    }
}
