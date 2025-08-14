using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//プレイヤーのマップ上の移動操作

public class MoveOnMap : MonoBehaviour
{
    [SerializeField] MapTransform _mapTrs;

    public void MoveControl(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector2 getVec = context.ReadValue<Vector2>();

        if (!Move(getVec)) Debug.Log("移動できませんでした");
    }

    bool Move(Vector2 inputVec)//指定方向に移動(移動に失敗したらfalseを返す)
    {
        MapVec moveVec;
        moveVec.x = (int)inputVec.x;
        moveVec.y = -(int)inputVec.y;

        MapVec newPos = _mapTrs.Pos + moveVec;

        if (!_mapTrs.CurrentHierarchy.IsInRange(newPos) || _mapTrs.CurrentHierarchy.Mass[newPos] != E_Mass.Empty) return false;//移動できない場合

        _mapTrs.Pos=newPos;
        return true;
    }
}
