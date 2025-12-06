using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ˆÚ“®Œo˜H‚ÌUI

public class PathUI : MonoBehaviour
{
    [SerializeField] SerializableDictionary<EPlayerState, GameObject> _pathObject;

    PlayerState _state;
    Stack<GameObject> _pathObjs = new();

    public void InstancePath(Vector3 dir,Vector3 pathPos)
    {
        var rotation = Quaternion.LookRotation(dir);
        var o = Instantiate(_pathObject[_state.State], pathPos, rotation);
        _pathObjs.Push(o);
    }

    public void UndoPath()
    {
        Destroy(_pathObjs.Pop());
    }

    public void ClearPath()
    {
        while (_pathObjs.Count > 0)
        {
            Destroy(_pathObjs.Pop());
        }
    }

    private void Awake()
    {
        _state = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
    }
}
