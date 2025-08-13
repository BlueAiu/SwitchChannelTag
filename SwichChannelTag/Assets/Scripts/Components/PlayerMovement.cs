using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//キャラの動くコンポーネント(プロトタイプ段階)

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("マップ上の移動関係")] [SerializeField] MoveOnMap _moveOnMap;
    [Tooltip("階層の移動関係")] [SerializeField] ChangeHierarchy _changeHierarchy;
    [Tooltip("初期位置")] [SerializeField] MapVec _startPoint;
    [Tooltip("初期階層番号")] [SerializeField] int _initHierarchyIndex;

    private MapVec _currentPos;//現在の位置情報
    private int _currentHierarchyIndex;//現在の階層番号

    public MapVec CurrentPos { get { return _currentPos; } }//現在の位置

    public int CurrentHierarchyIndex { get { return _currentHierarchyIndex; } }

    void Start()
    {
        //初期位置の設定
        Map_A_Hierarchy firstMap = _changeHierarchy.Change_Index(ref _currentHierarchyIndex, _initHierarchyIndex);
        _moveOnMap.RewritePos(out _currentPos,_startPoint, firstMap);
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector2 getVec = context.ReadValue<Vector2>();

        Debug.Log(getVec);

        if(!_moveOnMap.Move(ref _currentPos,getVec)) Debug.Log("移動できませんでした");
    }

    public void SwitchHierarchy_Inc(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Map_A_Hierarchy newMap = _changeHierarchy.Change_Delta(ref _currentHierarchyIndex,1);
        _moveOnMap.RewritePos(out _currentPos, _currentPos, newMap);
    }

    public void SwitchHierarchy_Dec(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Map_A_Hierarchy newMap = _changeHierarchy.Change_Delta(ref _currentHierarchyIndex,-1);
        _moveOnMap.RewritePos(out _currentPos, _currentPos, newMap);
    }
}
