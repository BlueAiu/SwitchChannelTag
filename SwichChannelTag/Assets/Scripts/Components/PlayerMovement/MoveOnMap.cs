using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//プレイヤーのマップ上の移動操作
//enabledをfalseにすれば、ボタンを押しても移動を出来なくすることが出来る

public partial class MoveOnMap : MonoBehaviour
{
    [Tooltip("プレイヤーの移動の様子")] [SerializeField]
    PlayerMoveAnimation _playerMoveAnimation;

    [Tooltip("プレイヤーの位置をずらす機能")] [SerializeField] 
    ShiftPlayersPosition _shiftPlayersPosition;

    MapTransform _myMapTrs;//自分のマップ上の位置情報
    CanShift _myCanShift;

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

        Vector2 getVec = context.ReadValue<Vector2>();

        if (!IsMovable(getVec, out MapVec newGridPos))
        {
            Debug.Log("移動に失敗");
            return;
        }

        if (_remainingStep <= 0 && !WhetherUndoMove(newGridPos)) return;    // dont move if no steps remaining

        //移動に成功
        StartCoroutine(Move(newGridPos));
    }



    //private
    IEnumerator Move(MapVec newGridPos)//移動処理
    {
        Vector3 start = _myMapTrs.CurrentWorldPos;//現在のマスの中心点
        Vector3 destination = _myMapTrs.CurrentHierarchy.MapToWorld(newGridPos);//移動先のマスの中心点

        //移動を戻したかに基づき残り移動可能マスを更新する
        if (UpdateMoveHistory(_myMapTrs.Pos.gridPos, newGridPos))
        {
            _remainingStep++;
            UndoPath();
        }
        else
        {
            _remainingStep--;
            InstancePath(destination - start);
        }

        _shiftPlayersPosition.OnExit(_myMapTrs);//ずらす処理
        _myCanShift.IsShiftAllowed = false;//自分がずらされないようにする

        _playerMoveAnimation.StartMove(start, destination);//移動アニメーション開始

        yield return new WaitUntil(()=>!_playerMoveAnimation.IsMoving);//移動アニメーションが終わるまで待つ

        _myCanShift.IsShiftAllowed = true;//自分がずれてもいいようにする
        
        //位置情報の書き換え
        MapPos newPos = _myMapTrs.Pos;
        newPos.gridPos = newGridPos;
        _myMapTrs.Rewrite(newPos);

        _shiftPlayersPosition.OnEnter(_myMapTrs);//ずらす処理
    }

    bool IsMovable(Vector2 inputVec,out MapVec newGridPos)//指定方向に移動できるか
    {
        MapVec moveVec;
        moveVec.x = (int)inputVec.x;
        moveVec.y = -(int)inputVec.y;

        newGridPos = _myMapTrs.Pos.gridPos + moveVec;

        if(!IsMovableMass(newGridPos)) return false;
        if(_myMapTrs.CurrentHierarchy.IsBlockedByWall(_myMapTrs.Pos.gridPos, moveVec)) return false;

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
        _myCanShift = PlayersManager.GetComponentFromMinePlayer<CanShift>();
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        _state = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
    }
}
