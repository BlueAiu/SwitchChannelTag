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
    [Tooltip("マップ上の位置情報")] [SerializeField] 
    MapTransform _mapTrs;

    public event Action<int> OnSwitchHierarchy_NewIndex;//階層切り替え時に呼ばれる(引数に新しい階層番号を入れる形式)
    public event Action OnSwitchHierarchy;//階層切り替え時に呼ばれる(引数なし)

    public bool IsMoved;//
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

        _mapTrs.Rewrite(newHierarchyIndex);

        OnSwitchHierarchy_NewIndex?.Invoke(newHierarchyIndex);
        OnSwitchHierarchy?.Invoke();

        IsMoved = true;
    }

    private void Start()
    {
        Init();
    }

    private void Init()//初期化処理
    {
        _mapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        IsMoved = false;
    }
}
