using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//プレイヤーの階層移動操作

public class ChangeHierarchy : MonoBehaviour
{
    [SerializeField] MapTransform _mapTrs;

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

    void SwitchHierarchy(bool inc)
    {
        int delta = inc ? 1 : -1;

        int newHierarchyIndex = MathfExtension.CircularWrapping_Delta(_mapTrs.HierarchyIndex, delta, _mapTrs.Hierarchies.Length - 1);

        _mapTrs.HierarchyIndex=newHierarchyIndex;
    }
}
