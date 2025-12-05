using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    bool _isMoving = false;

    public event Action<MapPos> OnStartMove;//移動開始時(出発点の位置を伝える)
    public event Action<MapPos> OnFinishMove;//移動完了時(到着点の位置を伝える)

    public bool IsMoving { get => _isMoving; }


    public void StartMoveOnPath()
    {
        StartCoroutine(MoveOnPath());
    }

    IEnumerator MoveOnPath()
    {
        _isMoving = true;

        foreach(var p in MovePath)
        {
            yield return Move(p);
        }

        _isMoving = false;
        ClearMoveHistory();
    }

    //private
    IEnumerator Move(MapVec newGridPos)//移動処理
    {
        Vector3 start = _myMapTrs.CurrentWorldPos;//現在のマスの中心点
        Vector3 destination = _myMapTrs.CurrentHierarchy.MapToWorld(newGridPos);//移動先のマスの中心点

        _shiftPlayersPosition.OnExit(_myMapTrs);//ずらす処理
        _myCanShift.IsShiftAllowed = false;//自分がずらされないようにする

        OnStartMove?.Invoke(_myMapTrs.Pos);//移動開始時のコールバックを呼ぶ

        _playerMoveAnimation.StartMove(start, destination);//移動アニメーション開始
        yield return new WaitUntil(()=>!_playerMoveAnimation.IsMoving);//移動アニメーションが終わるまで待つ

        _myCanShift.IsShiftAllowed = true;//自分がずれてもいいようにする
        RewriteMyMapPos(newGridPos);//位置情報の書き換え(移動アニメーションが終わった後に)
        _shiftPlayersPosition.OnEnter(_myMapTrs);//ずらす処理

        OnFinishMove?.Invoke(_myMapTrs.Pos);//移動終了時のコールバックを呼び出す
    }

    void RewriteMyMapPos(MapVec newGridPos)
    {
        MapPos newPos = _myMapTrs.Pos;
        newPos.gridPos = newGridPos;
        _myMapTrs.Rewrite(newPos);
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
