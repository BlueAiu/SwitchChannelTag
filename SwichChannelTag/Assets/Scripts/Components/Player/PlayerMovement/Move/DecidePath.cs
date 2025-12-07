using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class DecidePath : MonoBehaviour
{
    [SerializeField] PathUI _pathUI;
    [SerializeField] PathWay _pathWay;
    [SerializeField] MoveCurcorUI _moveCursorUI;
    MapTransform _myMapTrs;
    MapVec _currentPos;

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

        Vector2 getVec = context.ReadValue<Vector2>();

        if (!IsMovable(getVec, out MapVec newGridPos))
        {
            Debug.Log("移動に失敗");
            return;
        }

        bool isUndo = _pathWay.IsOneStepBefore(newGridPos);//来た道を戻っているか
        Debug.Log(isUndo);

        if (_remainingStep <= 0 && !isUndo) return;    // dont move if no steps remaining

        //移動に成功
        MoveCursor(newGridPos,isUndo);
    }

    void MoveCursor(MapVec newGridPos,bool isUndo)
    {
        Vector3 start = _myMapTrs.CurrentHierarchy.MapToWorld(_currentPos);  //現在のマスの中心点
        Vector3 destination = _myMapTrs.CurrentHierarchy.MapToWorld(newGridPos);    //移動先のマスの中心点

        //移動を戻したかに基づき残り移動可能マスを更新する
        if (isUndo)//来た道を戻っている
        {
            _pathWay.Pop();
            _remainingStep++;
            _pathUI.UndoPath();
        }
        else
        {
            _pathWay.Push(_currentPos);//移動前の位置を入れる
            _remainingStep--;
            _pathUI.InstancePath(destination - start,start);
        }

        _moveCursorUI.MoveCursor(destination);
        _currentPos = newGridPos;
    }



    bool IsMovable(Vector2 inputVec, out MapVec newGridPos)//指定方向に移動できるか
    {
        MapVec moveVec;
        moveVec.x = (int)inputVec.x;
        moveVec.y = -(int)inputVec.y;

        newGridPos = _currentPos + moveVec;

        if (!IsMovableMass(newGridPos)) return false;
        if (_myMapTrs.CurrentHierarchy.IsBlockedByWall(_currentPos, moveVec)) return false;

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
        _currentPos = _myMapTrs.Pos.gridPos;
        _moveCursorUI.OnStartDecide(_myMapTrs.CurrentHierarchy.MapToWorld(_currentPos));
    }

    public void OnFinish()
    {
        _moveCursorUI.OnFinishDecide();
        _pathWay.Push(_currentPos);//最終位置を入れる
    }

    private void Awake()
    {
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
    }
}
