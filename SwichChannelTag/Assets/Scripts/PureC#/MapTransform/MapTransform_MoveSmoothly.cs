using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void StartMoveSmoothly(MapVec newMapPos, float duration)
    {
        _moving = true;
        _endMapPos = newMapPos;
        _moveDuration = duration;
        _currentMoveTime = 0;//現在の時間を初期化
        RewritePos(Pos, HierarchyIndex);//位置を現在の位置に初期化

        //始点、終点のワールド座標を設定
        _startWorldPos = CurrentHierarchy.MapToWorld(Pos);
        _endWorldPos=CurrentHierarchy.MapToWorld(_endMapPos);
    }

    void UpdateMoveSmoothly()
    {
        if (!_moving) return;

        //時間更新
        _currentMoveTime += Time.deltaTime;

        //ターゲットのワールド座標をだんだんと始点から終点まで近づけていく
        float rate = _currentMoveTime/_moveDuration;
        Vector3 newWorldPos = Vector3.Lerp(_startWorldPos, _endWorldPos, rate);
        _target.transform.position = newWorldPos;

        //時間が過ぎたら移動を終える
        if(_currentMoveTime>=_moveDuration) EndMoveSmoothly();
    }

    void EndMoveSmoothly()
    {
        _moving=false;
        RewritePos(_endMapPos, HierarchyIndex);//位置を終点の位置に書き換え
    }
}
