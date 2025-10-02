using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//プレイヤーの階層移動操作
//enabledをfalseにすれば、ボタンを押しても階層移動を出来なくすることが出来る

public class ChangeHierarchy : MonoBehaviour
{
    MapTransform _myMapTrs;//自分のマップ上の位置情報
    SetTransform _mySetTrs;

    public event Action<int> OnSwitchHierarchy_NewIndex;//階層切り替え時に呼ばれる(引数に新しい階層番号を入れる形式)
    public event Action OnSwitchHierarchy;//階層切り替え時に呼ばれる(引数なし)

    public bool IsMoved { get; set; } = false;

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

        int newHierarchyIndex = MathfExtension.CircularWrapping_Delta(_myMapTrs.Pos.hierarchyIndex, delta, _myMapTrs.Hierarchies.Length - 1);

        _myMapTrs.Rewrite(newHierarchyIndex);

        _mySetTrs.Position = _myMapTrs.CurrentWorldPos;

        OnSwitchHierarchy_NewIndex?.Invoke(newHierarchyIndex);
        OnSwitchHierarchy?.Invoke();

        IsMoved = true;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()//初期化処理
    {
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        _mySetTrs=PlayersManager.GetComponentFromMinePlayer<SetTransform>();
    }
}
