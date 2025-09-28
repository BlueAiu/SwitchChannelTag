using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//プレイヤーのマップ上の移動操作
//enabledをfalseにすれば、ボタンを押しても移動を出来なくすることが出来る

public class MoveOnMap : MonoBehaviour
{
    [Tooltip("プレイヤーの移動の様子")] [SerializeField]
    PlayerMoveAnimation _playerMoveAnimation; 

    MapTransform _myMapTrs;//自分のマップ上の位置情報

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

        if (_playerMoveAnimation.IsMoving) return;//キャラが移動中であれば無視

        if (_remainingStep <= 0) return;//残り移動可能マスが0なら移動できない

        Vector2 getVec = context.ReadValue<Vector2>();

        if (!IsMovable(getVec, out MapVec newGridPos))
        {
            Debug.Log("移動に失敗");
            return;
        }

        //移動に成功
        Move(newGridPos);
    }



    //private
    private void Move(MapVec newGridPos)//移動処理
    {
        Vector3 start = _myMapTrs.CurrentWorldPos;//現在のマスの中心点

        //位置情報の書き換え
        MapPos newPos = _myMapTrs.Pos;
        newPos.gridPos = newGridPos;
        _myMapTrs.Rewrite(newPos);

        Vector3 destination = _myMapTrs.CurrentWorldPos;//移動先のマスの中心点

        _remainingStep--;//残り移動可能マスを減らす
        _playerMoveAnimation.StartMove(start, destination);//移動アニメーション開始
    }

    bool IsMovable(Vector2 inputVec,out MapVec newGridPos)//指定方向に移動できるか
    {
        MapVec moveVec;
        moveVec.x = (int)inputVec.x;
        moveVec.y = -(int)inputVec.y;

        newGridPos = _myMapTrs.Pos.gridPos + moveVec;

        if(!IsMovableMass(newGridPos)) return false;

        return true;
    }

    bool IsMovableMass(MapVec newPos)//移動可能なマスか
    {
        if (!_myMapTrs.CurrentHierarchy.IsInRange(newPos)) return false;//範囲外のマスであれば移動できない
        if (_myMapTrs.CurrentHierarchy.Mass[newPos] != E_Mass.Empty) return false;//そのマスが空マスでなければ移動できない

        return true;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()//初期化処理
    {
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
    }
}
