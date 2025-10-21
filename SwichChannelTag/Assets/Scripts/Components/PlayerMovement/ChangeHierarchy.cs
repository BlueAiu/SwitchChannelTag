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
    [Tooltip("�v���C���[�̈ʒu�����炷�@�\")] [SerializeField]
    ShiftPlayersPosition _shiftPlayersPosition;

    MapTransform _myMapTrs;//�����̃}�b�v��̈ʒu���

    public event Action<int> OnSwitchHierarchy_NewIndex;//�K�w�؂�ւ����ɌĂ΂��(�����ɐV�����K�w�ԍ�������`��)
    public event Action OnSwitchHierarchy;//�K�w�؂�ւ����ɌĂ΂��(�����Ȃ�)

    public void SwitchHierarchy_Inc(InputAction.CallbackContext context)//�L�[�{�[�h����p�Ɉ�U�c��
    {
        if (!context.performed) return;

        const int delta = 1;
        int newHierarchyIndex = MathfExtension.CircularWrapping_Delta(_myMapTrs.Pos.hierarchyIndex, delta, _myMapTrs.Hierarchies.Length - 1);

        SwitchHierarchy(newHierarchyIndex);
    }

    public void SwitchHierarchy_Dec(InputAction.CallbackContext context)//�L�[�{�[�h����p�Ɉ�U�c��
    {
        if (!context.performed) return;

        const int delta = -1;
        int newHierarchyIndex = MathfExtension.CircularWrapping_Delta(_myMapTrs.Pos.hierarchyIndex, delta, _myMapTrs.Hierarchies.Length - 1);

        SwitchHierarchy(newHierarchyIndex);
    }

    public bool IsAbleToMoveTheHierarchy(int hierarchyIndex)//���̊K�w�Ɉړ��ł��邩
    {
        //�����̍�����K�w�ԍ��Ɠ����ł���Έړ��ł��Ȃ�
        if (_myMapTrs.Pos.hierarchyIndex == hierarchyIndex) return false;
        return true;
    }

    public bool SwitchHierarchy(int newHierarchyIndex)//�K�w�ړ�
    {
        if (!enabled) return false;

        if (!IsAbleToMoveTheHierarchy(newHierarchyIndex)) return false;

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

    private void Init()//����������
    {
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
    }
}
