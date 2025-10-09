using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//�쐬��:���R
//�v���C���[�̃}�b�v��̈ړ�����
//enabled��false�ɂ���΁A�{�^���������Ă��ړ����o���Ȃ����邱�Ƃ��o����

public class MoveOnMap : MonoBehaviour
{
    [Tooltip("�v���C���[�̈ړ��̗l�q")] [SerializeField]
    PlayerMoveAnimation _playerMoveAnimation;

    [Tooltip("�v���C���[�̈ʒu�����炷�@�\")] [SerializeField] 
    ShiftPlayersPosition _shiftPlayersPosition;

    MapTransform _myMapTrs;//�����̃}�b�v��̈ʒu���
    CanShift _myCanShift;

    int _remainingStep=0;//�c��ړ��\�}�X��

    public int RemainingStep
    {
        get { return _remainingStep; }
        set { _remainingStep = value; }
    }

    public void MoveControl(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!enabled) return;

        if (_playerMoveAnimation.IsMoving) return;//�L�������ړ����ł���Ζ���

        if (_remainingStep <= 0) return;//�c��ړ��\�}�X��0�Ȃ�ړ��ł��Ȃ�

        Vector2 getVec = context.ReadValue<Vector2>();

        if (!IsMovable(getVec, out MapVec newGridPos))
        {
            Debug.Log("�ړ��Ɏ��s");
            return;
        }

        //�ړ��ɐ���
        StartCoroutine(Move(newGridPos));
    }



    //private
    IEnumerator Move(MapVec newGridPos)//�ړ�����
    {
        Vector3 start = _myMapTrs.CurrentWorldPos;//���݂̃}�X�̒��S�_
        Vector3 destination = _myMapTrs.CurrentHierarchy.MapToWorld(newGridPos);//�ړ���̃}�X�̒��S�_

        _remainingStep--;//�c��ړ��\�}�X�����炷
        _shiftPlayersPosition.OnExit(_myMapTrs);//���炷����
        _myCanShift.IsShiftAllowed = false;//���������炳��Ȃ��悤�ɂ���

        _playerMoveAnimation.StartMove(start, destination);//�ړ��A�j���[�V�����J�n

        yield return new WaitUntil(()=>!_playerMoveAnimation.IsMoving);//�ړ��A�j���[�V�������I���܂ő҂�

        _myCanShift.IsShiftAllowed = true;//����������Ă������悤�ɂ���
        
        //�ʒu���̏�������
        MapPos newPos = _myMapTrs.Pos;
        newPos.gridPos = newGridPos;
        _myMapTrs.Rewrite(newPos);

        _shiftPlayersPosition.OnEnter(_myMapTrs);//���炷����
    }

    bool IsMovable(Vector2 inputVec,out MapVec newGridPos)//�w������Ɉړ��ł��邩
    {
        MapVec moveVec;
        moveVec.x = (int)inputVec.x;
        moveVec.y = -(int)inputVec.y;

        newGridPos = _myMapTrs.Pos.gridPos + moveVec;

        if(!IsMovableMass(newGridPos)) return false;
        if(!_myMapTrs.CurrentHierarchy.IsBlockedByWall(_myMapTrs.Pos.gridPos, moveVec)) return false;

        return true;
    }

    bool IsMovableMass(MapVec newPos)//�ړ��\�ȃ}�X��
    {
        if (!_myMapTrs.CurrentHierarchy.IsInRange(newPos)) return false;//�͈͊O�̃}�X�ł���Έړ��ł��Ȃ�
        if (_myMapTrs.CurrentHierarchy.Mass[newPos] != E_Mass.Empty) return false;//���̃}�X����}�X�łȂ���Έړ��ł��Ȃ�

        return true;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()//����������
    {
        _myCanShift = PlayersManager.GetComponentFromMinePlayer<CanShift>();
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
    }
}
