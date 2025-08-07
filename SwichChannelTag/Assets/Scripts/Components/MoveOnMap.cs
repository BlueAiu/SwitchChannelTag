using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//キャラの動くコンポーネント(テスト用)

public class MoveOnMap : MonoBehaviour
{
    [SerializeField] Map_A_Hierarchy _map;
    [SerializeField] MapVec _startPoint;

    private MapVec _currentPos;

    void Start()
    {
        //位置の初期化
        RewritePos(_startPoint);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector2 getVec = context.ReadValue<Vector2>();

        Debug.Log(getVec);

        MapVec moveVec;
        moveVec.x = (int)getVec.x;
        moveVec.y = (int)getVec.y;

        RewritePos(_currentPos+moveVec);
    }

    void RewritePos(MapVec newMapVec)//位置の書き換え
    {
        _currentPos = _map.ClampInRange(newMapVec);//範囲外の位置に行かないようにするための処置
        Vector3 newPos;

        _map.Transit_FromMapVec_ToWorldVec(_currentPos, out newPos);
        transform.position = newPos;
    }
}
