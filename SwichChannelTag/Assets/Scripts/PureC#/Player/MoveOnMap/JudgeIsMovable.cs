using UnityEngine;

//作成者:杉山
//移動方向先のマスに移動できるかを判定する機能

public class JudgeIsMovable
{
    MapTransform _myMapTrs;
    PathWay _pathWay;

    public void Awake(MapTransform myMapTrs,PathWay pathWay)
    {
        _myMapTrs = myMapTrs;
        _pathWay = pathWay;
    }

    public bool IsSuccessToMove(MapVec currentPos, MapVec moveVec, int remainingStep , out MapVec newGridPos, out bool isUndo)//移動に成功したかを判断する
    {
        bool destinationIsMovable = DestinationIsMovable(currentPos, moveVec, out newGridPos);//指定方向に移動できるか
        isUndo = _pathWay.IsOneStepBefore(newGridPos);//来た道を戻っているか

        if (!destinationIsMovable) return false;//移動先が移動できないマスであれば移動不可

        if (remainingStep <= 0 && !isUndo) return false;//残り移動可能マスが無くても戻るためなら移動可能

        return true;
    }

    bool DestinationIsMovable(MapVec currentPos, MapVec moveVec ,out MapVec newGridPos)//指定方向に移動できるか
    {
        newGridPos = currentPos + moveVec;

        if (!IsMovableMass(newGridPos)) return false;
        if (_myMapTrs.CurrentHierarchy.IsBlockedByWall(currentPos, moveVec)) return false;

        return true;
    }

    bool IsMovableMass(MapVec newPos)//移動可能なマスか
    {
        if (!_myMapTrs.CurrentHierarchy.IsInRange(newPos)) return false;//範囲外のマスであれば移動できない
        if (_myMapTrs.CurrentHierarchy.Mass[newPos] != E_Mass.Empty) return false;//そのマスが空マスでなければ移動できない

        return true;
    }
}
