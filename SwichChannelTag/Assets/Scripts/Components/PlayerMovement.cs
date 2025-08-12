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
    [Tooltip("初期位置")][SerializeField] MapVec _startPoint;
    private MapVec _currentPos;//現在の位置情報

    public MapVec CurrentPos { get { return _currentPos; } }//現在の位置

    void Start()
    {
        _moveOnMap.RewritePos(out _currentPos,_startPoint);//現在位置の初期化
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector2 getVec = context.ReadValue<Vector2>();

        Debug.Log(getVec);

        if(!_moveOnMap.Move(ref _currentPos,getVec)) Debug.Log("移動できませんでした");
    }
}
