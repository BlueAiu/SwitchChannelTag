using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor.Experimental.GraphView;

//作成者:杉山
//マップ上を一瞬で移動するのではなく、スムーズに移動

public partial class MapTransform
{
    bool _moving=false;
    const float _minDuration = 0;

    Vector3 _startWorldPos;//始点のワールド座標
    Vector3 _endWorldPos;//終点のワールド座標

    MapVec _endMapPos;//終点のマップ上の位置

    float _moveDuration;//動くのにかける時間
    float _currentMoveTime;//現在の時間

    bool _isSync;//移動時に同期させるか

    void StartMoveSmoothly(MapVec newMapPos, float duration,bool isSync)
    {
        if (isSync) _myPhotonView.RPC(nameof(InitProcess), RpcTarget.All);
        else InitProcess(newMapPos,duration,isSync);
    }

    void UpdateMoveSmoothly()
    {
        if (!_moving) return;

        if (_isSync && !_myPhotonView.IsMine) return;//同期移動の場合、自分のでないなら位置の計算処理をしない

        //時間更新
        _currentMoveTime += Time.deltaTime;

        //ターゲットのワールド座標をだんだんと始点から終点まで近づけていく
        float rate = _currentMoveTime/_moveDuration;
        Vector3 newWorldPos = Vector3.Lerp(_startWorldPos, _endWorldPos, rate);

        //同期させる場合は位置の書き換え処理のみ同期させる
        if (_isSync) _myPhotonView.RPC(nameof(UpdateTargetPos), RpcTarget.All);
        else UpdateTargetPos(newWorldPos);

        //時間が過ぎたら移動を終える
        if(_currentMoveTime>=_moveDuration) EndMoveSmoothly();
    }

    void EndMoveSmoothly()
    {
        if (_isSync) _myPhotonView.RPC(nameof(ExitProcess), RpcTarget.All);
        else ExitProcess();
    }


    [PunRPC]
    void InitProcess(MapVec newMapPos, float duration, bool isSync)
    {
        _isSync = isSync;
        _moving=true;
        _endMapPos = newMapPos;
        _moveDuration = duration;
        _currentMoveTime = 0;//現在の時間を初期化
        Rewrite(Pos, HierarchyIndex);//位置を現在の位置に初期化

        //始点、終点のワールド座標を設定
        _startWorldPos = CurrentHierarchy.MapToWorld(Pos);
        _endWorldPos = CurrentHierarchy.MapToWorld(_endMapPos);
    }

    [PunRPC]
    void UpdateTargetPos(Vector3 newWorldPos)//位置だけ書き換える(計算とかは自分だけやればいい)
    {
        _target.transform.position = newWorldPos;
    }

    [PunRPC]
    void ExitProcess()
    {
        _moving = false;
        Rewrite(_endMapPos, HierarchyIndex);//位置を終点の位置に書き換え
    }

}
