using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//�쐬��:���R
//�v���C���[�̊K�w�ړ�����
//enabled��false�ɂ���΁A�{�^���������Ă��K�w�ړ����o���Ȃ����邱�Ƃ��o����

public class ChangeHierarchy : MonoBehaviour
{
    MapTransform _myMapTrs;//�����̃}�b�v��̈ʒu���
    SetTransform _mySetTrs;

    public event Action<int> OnSwitchHierarchy_NewIndex;//�K�w�؂�ւ����ɌĂ΂��(�����ɐV�����K�w�ԍ�������`��)
    public event Action OnSwitchHierarchy;//�K�w�؂�ւ����ɌĂ΂��(�����Ȃ�)

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

    private void Init()//����������
    {
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        _mySetTrs=PlayersManager.GetComponentFromMinePlayer<SetTransform>();
    }
}
