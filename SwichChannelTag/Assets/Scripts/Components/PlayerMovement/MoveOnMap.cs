using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//プレイヤーのマップ上の移動操作

public class MoveOnMap : MonoBehaviour
{
    [SerializeField] MapTransform _mapTrs;
    int _remainingStep=0;//残り移動可能マス数

    public int RemainingStep
    {
        get { return _remainingStep; }
        set { _remainingStep = value; }
    }

    public void MoveControl(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (_remainingStep <= 0) return;//残り移動可能マスが0なら移動できない

        Vector2 getVec = context.ReadValue<Vector2>();

        if (!Move(getVec)) return;

        _remainingStep--;//移動出来たなら、残り移動可能マスを減らしておく
    }

    bool Move(Vector2 inputVec)//指定方向に移動(移動に失敗したらfalseを返す)
    {
        MapVec moveVec;
        moveVec.x = (int)inputVec.x;
        moveVec.y = -(int)inputVec.y;

        MapVec newPos = _mapTrs.Pos + moveVec;

        if (!IsMovableMass(newPos))//移動できない場合
        {
            Debug.Log("移動に失敗");
            return false;
        }

        //移動可能な場合
        _mapTrs.Pos=newPos;
        return true;
    }

    bool IsMovableMass(MapVec newPos)//移動可能なマスか
    {
        if (!_mapTrs.CurrentHierarchy.IsInRange(newPos)) return false;//範囲外のマスであれば移動できない
        if (_mapTrs.CurrentHierarchy.Mass[newPos] != E_Mass.Empty) return false;//そのマスが空マスでなければ移動できない

        return true;
    }
}
