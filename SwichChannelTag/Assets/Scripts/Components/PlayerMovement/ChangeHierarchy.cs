using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//プレイヤーの階層移動操作
//enabledをfalseにすれば、ボタンを押しても階層移動を出来なくすることが出来る

public partial class ChangeHierarchy : MonoBehaviour
{
    [Tooltip("プレイヤーの位置をずらす機能")] [SerializeField]
    ShiftPlayersPosition _shiftPlayersPosition;
    [Tooltip("階層移動のクールダウン")] [SerializeField]
    CoolDown_ChangeHierarchy _coolDown;

    MapTransform _myMapTrs;//自分のマップ上の位置情報

    public event Action<int> OnSwitchHierarchy_NewIndex;//階層切り替え時に呼ばれる(引数に新しい階層番号を入れる形式)
    public event Action OnSwitchHierarchy;//階層切り替え時に呼ばれる(引数なし)

    public void SwitchHierarchy_Inc(InputAction.CallbackContext context)//キーボード操作用に一旦残す
    {
        if (!context.performed) return;

        const int delta = 1;
        int newHierarchyIndex = MathfExtension.CircularWrapping_Delta(_myMapTrs.Pos.hierarchyIndex, delta, _myMapTrs.Hierarchies.Length - 1);

        SwitchHierarchy(newHierarchyIndex);
    }

    public void SwitchHierarchy_Dec(InputAction.CallbackContext context)//キーボード操作用に一旦残す
    {
        if (!context.performed) return;

        const int delta = -1;
        int newHierarchyIndex = MathfExtension.CircularWrapping_Delta(_myMapTrs.Pos.hierarchyIndex, delta, _myMapTrs.Hierarchies.Length - 1);

        SwitchHierarchy(newHierarchyIndex);
    }

    public bool IsAbleToMoveTheHierarchy(int hierarchyIndex)//その階層に移動できるか
    {
        //自分の今いる階層番号と同じであれば移動できない
        if (_myMapTrs.Pos.hierarchyIndex == hierarchyIndex) return false;
        return true;
    }

    public bool SwitchHierarchy(int newHierarchyIndex)//階層移動
    {
        if (!enabled) return false;

        if (!IsAbleToMoveTheHierarchy(newHierarchyIndex)) return false;

        if(!_coolDown.CanChangeHierarchy) return false;

        _coolDown.SetLastChangedTurn();

        _shiftPlayersPosition.OnExit(_myMapTrs);

        _myMapTrs.Rewrite(newHierarchyIndex);

        _shiftPlayersPosition.OnEnter(_myMapTrs);

        OnSwitchHierarchy_NewIndex?.Invoke(newHierarchyIndex);
        OnSwitchHierarchy?.Invoke();

        return true;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()//初期化処理
    {
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
    }
}
