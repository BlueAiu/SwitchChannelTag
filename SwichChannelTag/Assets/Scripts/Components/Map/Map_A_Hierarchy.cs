using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//�쐬��:���R
//1�K�w���Ƃ̃}�b�v�̊Ǘ�
//_centerTrs��[0,0]�Ƃ���-Z������_mapSize_Y�A+X������_mapSize_X���̍L���̃}�b�v��W�J
//����̕ύX�\����e
//�ʒu��Ή�������n�̃��\�b�h���N���X�Ƃ��Ă܂Ƃ߂�

public partial class Map_A_Hierarchy : MonoBehaviour
{
    [Tooltip("�}�b�v�̃T�C�Y")] [SerializeField] MapVec _mapSize;
    [Tooltip("��}�X���Ƃ̊Ԋu")] [SerializeField] float _gapDistance;
    [Tooltip("[0,0]�_�̈ʒu�ƂȂ�Transform")] [SerializeField] Transform _centerTrs;

    MassOfMap _mass;//�}�X�̊Ǘ�

    public MassOfMap Mass { get { return _mass; } }

    //�}�b�v�̃T�C�Y��������
    public int MapSize_X { get { return _mapSize.x; } }
    public int MapSize_Y { get { return _mapSize.y; } }

    //�}�X���W���͈͓����𔻒�
    public bool IsInRange(MapVec gridPos)
    {
        if (gridPos.x < 0 || gridPos.x >= _mapSize.x) return false;

        if (gridPos.y < 0 || gridPos.y >= _mapSize.y) return false;

        return true;
    }

    //�}�X���W��͈͓��Ɏ��߂�
    public MapVec ClampInRange(MapVec gridPos)
    {
        gridPos.x = Mathf.Clamp(gridPos.x, 0, _mapSize.x-1);

        gridPos.y = Mathf.Clamp(gridPos.y, 0, _mapSize.y-1);

        return gridPos;
    }

    //�}�X���W�����[���h���W�ɕϊ�(�ϊ��Ɏ��s������false��Ԃ�)
    public Vector3 MapToWorld(MapVec gridPos)
    {
        //�͈͊O�ł���Όx������
        if (!IsInRange(gridPos)) Debug.Log("���̍��W�͔͈͊O�ł��I");

        Vector3 ret = new();

        Vector3 centerVec = _centerTrs.position;
        ret = centerVec;

        ret.x += gridPos.x * _gapDistance;//X�����̌v�Z
        ret.z -= gridPos.y * _gapDistance;//Y�����̌v�Z

        return ret;
    }


    //private

    private void Awake()
    {
        _mass = new MassOfMap(_mapSize);
    }
}
