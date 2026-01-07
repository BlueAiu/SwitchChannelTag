using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//受け取った経路を元にプレイヤーを移動させる

public class MoveOnPath : MonoBehaviour
{
    [Tooltip("プレイヤーの移動の様子")] [SerializeField]
    PlayerMoveAnimation _playerMoveAnimation;

    [Tooltip("プレイヤーの位置をずらす機能")] [SerializeField]
    ShiftPlayersPosition _shiftPlayersPosition;

    [Tooltip("自キャラを元の向きに直す機能")] [SerializeField]
    LookDefaultDirection _lookDefaultDirection;

    MapTransform _myMapTrs;//自分のマップ上の位置情報
    CanShift _myCanShift;
    IsMovingState _myIsMovingState;

    public event Action<MapPos> OnStartMove;//移動開始時(出発点の位置を伝える)
    public event Action<MapPos> OnFinishMove;//移動完了時(到着点の位置を伝える)

    public bool IsMoving { get => _myIsMovingState.IsMoving; }

    public void StartMove(MapVec[] path)//移動開始、pathは移動経路
    {
        StartCoroutine(Move(path));
    }

    //private

    IEnumerator Move(MapVec[] path)
    {
        _playerMoveAnimation.OnStartMove();
        _myIsMovingState.IsMoving = true;

        foreach (var p in path)
        {
            yield return MoveOneStep(p);
        }

        _playerMoveAnimation.OnFinishMove();
        _myIsMovingState.IsMoving = false;
    }

    IEnumerator MoveOneStep(MapVec newGridPos)//1マス移動
    {
        Vector3 start = _myMapTrs.CurrentWorldPos;//現在のマスの中心点
        Vector3 destination = _myMapTrs.CurrentHierarchy.MapToWorld(newGridPos);//移動先のマスの中心点

        _shiftPlayersPosition.OnExit(_myMapTrs);//ずらす処理
        _myCanShift.IsShiftAllowed = false;//自分がずらされないようにする

        OnStartMove?.Invoke(_myMapTrs.Pos);//移動開始時のコールバックを呼ぶ

        _playerMoveAnimation.StartMoveOnMass(start, destination);//移動アニメーション開始
        yield return new WaitUntil(() => !_playerMoveAnimation.IsMovingOnMass);//移動アニメーションが終わるまで待つ

        _myCanShift.IsShiftAllowed = true;//自分がずれてもいいようにする
        RewriteMyMapPos(newGridPos);//位置情報の書き換え(移動アニメーションが終わった後に)
        _shiftPlayersPosition.OnEnter(_myMapTrs);//ずらす処理
        _lookDefaultDirection.LookDefault();//自キャラを元の向きに戻す

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
        _myIsMovingState = PlayersManager.GetComponentFromMinePlayer<IsMovingState>();
    }
}
