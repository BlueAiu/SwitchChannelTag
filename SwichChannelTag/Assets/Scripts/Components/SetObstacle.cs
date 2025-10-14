using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//�쐬��:���R
//�}�b�v��ɏ�Q����u��(�e�X�g�p)

public class SetObstacle : MonoBehaviour
{
    [SerializeField] Maps_Hierarchies _map;
    [SerializeField] GameObject _obstacleObject;
    [SerializeField] MapPos[] _obstaclePoses;
    [SerializeField] GameObject _wallObstacleObject;
    [SerializeField] MapWall_Obstacle[] _setWalls;
    
    void Start()
    {
        for(int i=0; i<_obstaclePoses.Length ;i++)
        {
            MapPos _obstaclePos = _obstaclePoses[i];

            //�͈͊O�ł���΁A�������s
            if (!_map.IsInRange(_obstaclePos))
            {
                Debug.Log("�͈͊O�Ȃ̂Ő������s");
                continue;
            }
            
            Map_A_Hierarchy map = _map[_obstaclePos.hierarchyIndex];

            Vector3 pos = map.MapToWorld(_obstaclePos.gridPos);
            GameObject obstacleInstance = Instantiate(_obstacleObject);//��Q���I�u�W�F�N�g�𐶐�
            obstacleInstance.transform.position = pos;

            map.Mass[_obstaclePos.gridPos] = E_Mass.Obstacle;
        }

        foreach(var setWall in _setWalls)
        {
            var wallDir = setWall.blockUnder ? MapVec.Up : MapVec.Right;

            MapPos wallPos0 = new(setWall.hierarchyIndex, setWall.pos);
            MapPos wallPos1 = new(setWall.hierarchyIndex, setWall.pos + wallDir);

            if(!_map.IsInRange(wallPos0) || !_map.IsInRange(wallPos1))
            {
                Debug.Log("�}�b�v�̕����܂߂��͈͊O�ɕǂ�u�����Ƃ��Ă��܂�");
                continue;
            }

            var map = _map[setWall.hierarchyIndex];
            map.AddWall(new Wall_Obstacle(setWall.pos, wallDir));

            Vector3 worldPos = (map.MapToWorld(wallPos0.gridPos) + map.MapToWorld(wallPos1.gridPos)) / 2.0f;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(wallDir.x, 0, wallDir.y));
            Instantiate(_wallObstacleObject, worldPos, rotation);
        }
    }
}
