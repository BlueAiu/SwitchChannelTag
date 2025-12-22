using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class DecidePath : MonoBehaviour
{
    [SerializeField] RepeatInputHandler _repeatInputHandler;
    [SerializeField] PathUI _pathUI;
    [SerializeField] PathWay _pathWay;
    [SerializeField] MoveCurcorUI _moveCursorUI;
    JudgeIsMovable _judgeIsMovable=new();//移動方向先のマスに動けるかを判定する機能
    MapTransform _myMapTrs;
    MapVec _currentPos;

    int _remainingStep = 0;//残り移動可能マス数

    public int RemainingStep
    {
        get { return _remainingStep; }
        set { _remainingStep = value; }
    }

    void MoveControl(Vector2 inputVec)//入力ベクトルを受け取って移動
    {
        if (!_judgeIsMovable.IsSuccessToMove(_currentPos, InputVecToMoveVec(inputVec), _remainingStep, out MapVec newGridPos, out bool isUndo))
        {
            Debug.Log("移動に失敗");
            return;
        }

        //移動に成功
        MoveCursor(newGridPos,isUndo);
    }

    void MoveCursor(MapVec newGridPos,bool isUndo)
    {
        Vector3 start = _myMapTrs.CurrentHierarchy.MapToWorld(_currentPos);  //現在のマスの中心点
        Vector3 destination = _myMapTrs.CurrentHierarchy.MapToWorld(newGridPos);    //移動先のマスの中心点

        //移動を戻したかに基づき残り移動可能マスを更新する
        if (isUndo)//来た道を戻っている
        {
            _pathWay.Pop();
            _remainingStep++;
            _pathUI.UndoPath();
        }
        else
        {
            _pathWay.Push(_currentPos);//移動前の位置を入れる
            _remainingStep--;
            _pathUI.InstancePath(destination - start,start);
        }

        _moveCursorUI.MoveCursor(destination);
        _currentPos = newGridPos;
    }

    MapVec InputVecToMoveVec(Vector2 inputVec)//Vector2の生の入力ベクトルからMapVecの移動方向ベクトルに変換する
    {
        inputVec = DirectionUtility.ToCardinal(inputVec);
        MapVec moveVec;
        moveVec.x = (int)inputVec.x;
        moveVec.y = -(int)inputVec.y;

        return moveVec;
    }

    public void OnStart()
    {
        _currentPos = _myMapTrs.Pos.gridPos;
        _moveCursorUI.OnStartDecide(_myMapTrs.CurrentHierarchy.MapToWorld(_currentPos));
    }

    public void OnFinish()
    {
        _moveCursorUI.OnFinishDecide();
        _pathWay.Push(_currentPos);//最終位置を入れる
    }

    private void Awake()
    {
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        _judgeIsMovable.Awake(_myMapTrs, _pathWay);
    }

    private void OnEnable()
    {
        _repeatInputHandler.OnInputVec2 += MoveControl;
    }

    private void OnDisable()
    {
        _repeatInputHandler.OnInputVec2 -= MoveControl;
    }
}
