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

    void Start()
    {
        _moveOnMap.Start();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector2 getVec = context.ReadValue<Vector2>();

        Debug.Log(getVec);

        if(!_moveOnMap.Move(getVec)) Debug.Log("移動できませんでした");
    }
}
