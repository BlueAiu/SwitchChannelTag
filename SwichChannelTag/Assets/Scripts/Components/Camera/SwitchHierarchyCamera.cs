using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�K�w���ڂ��J�����̐؂�ւ�

public class SwitchHierarchyCamera : MonoBehaviour
{
    [Tooltip("�K�w���Ƃ̃o�[�`�����J����\n0�ڂ���V��->�n��->�n���̏��ɓ���Ă�������")] [SerializeField] 
    CinemachineVirtualCamera[] _mapVCams;

    [SerializeField] 
    ChangeHierarchy _changeHierarchy;

    [SerializeField]
    Maps_Hierarchies _maps_Hierarchies;

    public void Switch(int hierarchyIndex)//�ʂ��K�w�̐؂�ւ�
    {
        if (!IsValid_VCamLength()) return;

        if(!_maps_Hierarchies.IsInRange(hierarchyIndex))
        {
            Debug.Log("�͈͊O�̊K�w�ԍ��ł��I");
            return;
        }

        for(int i=0; i<_mapVCams.Length ;i++)
        {
            _mapVCams[i].enabled = (i == hierarchyIndex);
        }
    }



    //private
    private void Awake()
    {
        IsValid_VCamLength();
        _changeHierarchy.OnSwitchHierarchy_NewIndex += Switch;
    }

    private void Start()
    {
        InitCamera();
    }

    void InitCamera()//�J�����̏���������
    {
        //�v���C���[�̏����ʒu�ɃJ���������킹��
        MapTransform myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        Switch(myMapTrs.Pos.hierarchyIndex);
    }

    bool IsValid_VCamLength()
    {
        if(_mapVCams.Length != _maps_Hierarchies.Length)
        {
            Debug.Log("�K�w�̐����A�J�������ݒ肳��Ă��܂���I");
            return false;
        }

        return true;
    }
}
