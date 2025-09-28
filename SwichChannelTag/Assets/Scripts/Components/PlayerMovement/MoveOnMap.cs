using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//プレイヤーのマップ上の移動操作
//enabledをfalseにすれば、ボタンを押しても移動を出来なくすることが出来る

public class MoveOnMap : MonoBehaviour
{
    [Tooltip("一つのマスの移動にかける時間")] [SerializeField]
    float _moveDuration;

    [Tooltip("マップ上の位置情報")] [SerializeField]
    MapTransform _mapTrs;

    int _remainingStep=0;//残り移動可能マス数

    public int RemainingStep
    {
        get { return _remainingStep; }
        set { _remainingStep = value; }
    }

    public void MoveControl(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!enabled) return;

        //if (_mapTrs.Moving) return;//キャラが移動中であれば無視

        if (_remainingStep <= 0) return;//残り移動可能マスが0なら移動できない

        Vector2 getVec = context.ReadValue<Vector2>();

        if (!Move(getVec)) return;

        _remainingStep--;//移動出来たなら、残り移動可能マスを減らしておく
    }



    //private
    bool Move(Vector2 inputVec)//指定方向に移動(移動に失敗したらfalseを返す)
    {
        MapVec moveVec;
        moveVec.x = (int)inputVec.x;
        moveVec.y = -(int)inputVec.y;

        MapVec newPos = _mapTrs.Pos.gridPos + moveVec;

        if (!IsMovableMass(newPos))//移動できない場合
        {
            Debug.Log("移動に失敗");
            return false;
        }

        //移動可能な場合
        //_mapTrs.MoveSmoothly(newPos,_moveDuration);
        return true;
    }

    bool IsMovableMass(MapVec newPos)//移動可能なマスか
    {
        if (!_mapTrs.CurrentHierarchy.IsInRange(newPos)) return false;//範囲外のマスであれば移動できない
        if (_mapTrs.CurrentHierarchy.Mass[newPos] != E_Mass.Empty) return false;//そのマスが空マスでなければ移動できない

        return true;
    }

    private void Start()
    {
        Init();
    }

    private void Init()//初期化処理
    {
        _mapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
    }
}
