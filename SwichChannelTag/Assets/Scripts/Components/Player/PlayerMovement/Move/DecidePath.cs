using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class DecidePath : MonoBehaviour
{
    [SerializeField] GameObject _cursorPrefab;
    GameObject _cursor;
    MapTransform _myMapTrs;
    MapVec _cursorPos;

    int _remainingStep = 0;//残り移動可能マス数

    public int RemainingStep
    {
        get { return _remainingStep; }
        set { _remainingStep = value; }
    }

    public void MoveControl(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!enabled) return;

        //if (_playerMoveAnimation.IsMoving) return;//キャラが移動中であれば無視

        Vector2 getVec = context.ReadValue<Vector2>();

        if (!IsMovable(getVec, out MapVec newGridPos))
        {
            Debug.Log("移動に失敗");
            return;
        }

        if (_remainingStep <= 0 && !WhetherUndoMove(newGridPos)) return;    // dont move if no steps remaining

        //移動に成功
        MoveCursor(newGridPos);
    }

    void MoveCursor(MapVec newGridPos)
    {
        if (_cursor == null) return;

        Vector3 start = _cursor.transform.position;  //現在のマスの中心点
        Vector3 destination = _myMapTrs.CurrentHierarchy.MapToWorld(newGridPos);    //移動先のマスの中心点

        //移動を戻したかに基づき残り移動可能マスを更新する
        if (UpdateMoveHistory(_cursorPos, newGridPos))
        {
            _remainingStep++;
            UndoPath();
        }
        else
        {
            _remainingStep--;
            InstancePath(newGridPos, destination - start);
        }

        _cursor.transform.position = destination;
        _cursorPos = newGridPos;
    }



    bool IsMovable(Vector2 inputVec, out MapVec newGridPos)//指定方向に移動できるか
    {
        MapVec moveVec;
        moveVec.x = (int)inputVec.x;
        moveVec.y = -(int)inputVec.y;

        newGridPos = _cursorPos + moveVec;

        if (!IsMovableMass(newGridPos)) return false;
        if (_myMapTrs.CurrentHierarchy.IsBlockedByWall(_cursorPos, moveVec)) return false;

        return true;
    }

    bool IsMovableMass(MapVec newPos)//移動可能なマスか
    {
        if (!_myMapTrs.CurrentHierarchy.IsInRange(newPos)) return false;//範囲外のマスであれば移動できない
        if (_myMapTrs.CurrentHierarchy.Mass[newPos] != E_Mass.Empty) return false;//そのマスが空マスでなければ移動できない

        return true;
    }

    public void OnStart()
    {
        _cursorPos = _myMapTrs.Pos.gridPos;
        _cursor = Instantiate(_cursorPrefab, _myMapTrs.CurrentHierarchy.MapToWorld(_cursorPos), Quaternion.identity);
    }

    public void OnFinish()
    {
        Destroy(_cursor);
        _cursor = null;
    }

    private void Awake()
    {
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        _state = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
    }
}
