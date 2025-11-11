using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MoveOnMap : MonoBehaviour
{
    [SerializeField] SerializableDictionary<EPlayerState, GameObject> _pathObject;

    PlayerState _state;
    Stack<GameObject> _path = new();

    void InstancePath(Vector3 dir)
    {
        var rotation = Quaternion.LookRotation(dir);
        var o = Instantiate(_pathObject[_state.State], _myMapTrs.CurrentWorldPos, rotation);
        _path.Push(o);
    }

    void UndoPath()
    {
        Destroy(_path.Pop());
    }

    void ClearPath()
    {
        while(_path.Count > 0)
        {
            Destroy( _path.Pop());
        }
    }
}
