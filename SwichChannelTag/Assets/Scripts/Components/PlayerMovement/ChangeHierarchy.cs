using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//プレイヤーの階層移動操作
//enabledをfalseにすれば、ボタンを押しても階層移動を出来なくすることが出来る

public class ChangeHierarchy : MonoBehaviour
{
    [Tooltip("シーン内のプレイヤーの情報を取得する機能")] [SerializeField] 
    ScenePlayerManager _scenePlayerManager;

    [Tooltip("マップ上の位置情報")] [SerializeField] 
    MapTransform _mapTrs;

    public void SwitchHierarchy_Inc(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SwitchHierarchy(true);
    }

    public void SwitchHierarchy_Dec(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SwitchHierarchy(false);
    }



    //private
    void SwitchHierarchy(bool inc)
    {
        if (!enabled) return;

        int delta = inc ? 1 : -1;

        int newHierarchyIndex = MathfExtension.CircularWrapping_Delta(_mapTrs.HierarchyIndex, delta, _mapTrs.Hierarchies.Length - 1);

        _mapTrs.Rewrite(newHierarchyIndex,true);
    }

    private void Start()
    {
        Init();
    }

    private void Init()//初期化処理
    {
        if (_scenePlayerManager != null) _mapTrs = _scenePlayerManager.MyComponent<MapTransform>();
        else if (_mapTrs == null) Debug.Log("このままだとプレイヤーが動けません！");
    }
}
