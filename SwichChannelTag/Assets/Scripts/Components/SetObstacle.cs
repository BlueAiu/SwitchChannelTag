using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//�쐬��:���R
//�}�b�v��ɏ�Q����u��(�e�X�g�p)

public class SetObstacle : MonoBehaviour
{
    [Serializable]
    struct obstaclesMap
    {
        public List<string> map;
    }


    [SerializeField] Maps_Hierarchies _map;
    [SerializeField] GameObject _obstacleObject;
    [SerializeField] GameObject _wallObstacleObject;

    [SerializeField]
    [Tooltip("������̃��X�g�ŏ�Q����ݒ肵�܂�\n" +
        ". : �����u���Ȃ�\n" +
        "_ : ���ɕǂ�u��\n" +
        "| : �E�ɕǂ�u��\n" +
        "L : ���ƉE�ɕǂ�u��\n" +
        "# : ��Q����u��\n" +
        "����\n" +
        "������̒����A���X�g�̃T�C�Y�ɒ��ӂ��ĉ�����")]
    List<obstaclesMap> _obstaclesMaps; 
    
    void Start()
    {
        if (_obstaclesMaps.Count != _map.Length) 
        { Debug.LogAssertion("ObstacleMaps�̃T�C�Y���K�w�̐��Ƒ����Ă�������"); }

        for(int i=0;i<_obstaclesMaps.Count;i++)
        {
            var map = _obstaclesMaps[i].map;
            if(map.Count != _map[i].MapSize_Y)
            { Debug.LogAssertionFormat("ObstacleMaps[{0}]�̃T�C�Y���}�b�v�̍����Ƒ����Ă�������", i); }

            for(int h = 0; h < map.Count; h++)
            {
                var str = map[h];
                if(str.Length != _map[i].MapSize_X)
                { Debug.LogAssertionFormat("ObstacleMaps[{0}][{1}]�̕�����̒������}�b�v�̉����Ƒ����Ă�������", i, h); }

                for(int w = 0; w < map.Count; w++)
                {
                    char c = str[w];
                    MapPos pos = new MapPos(i, new MapVec(w, h));

                    PutObstacle(c, pos);
                }
            }
        }
    }

    void PutObstacle(char c, MapPos pos)
    {
        bool isWall = true;
        List<MapWall_Obstacle> walls = new();

        switch (c)
        {
            case '.': return;
            case '_':
                walls.Add(new MapWall_Obstacle(pos, true));
                break;
            case '|':
                walls.Add(new MapWall_Obstacle(pos, false));
                break;
            case 'L':
                walls.Add(new MapWall_Obstacle(pos, true));
                walls.Add(new MapWall_Obstacle(pos, false));
                break;
            case '#':
                isWall = false;
                break;

            default: break;
        }

        if (!isWall)
        {
            PutCupsuleObstacle(pos);
        }
        else
        {
            foreach(var wall in walls)
            { PutWallObstacle(wall); }
        }
    }

    void PutCupsuleObstacle(MapPos _obstaclePos)
    {
        //�͈͊O�ł���΁A�������s
        if (!_map.IsInRange(_obstaclePos))
        {
            Debug.Log("�͈͊O�Ȃ̂Ő������s");
            return;
        }

        Map_A_Hierarchy map = _map[_obstaclePos.hierarchyIndex];

        Vector3 pos = map.MapToWorld(_obstaclePos.gridPos);
        GameObject obstacleInstance = Instantiate(_obstacleObject);//��Q���I�u�W�F�N�g�𐶐�
        obstacleInstance.transform.position = pos;

        map.Mass[_obstaclePos.gridPos] = E_Mass.Obstacle;
    }

    void PutWallObstacle(MapWall_Obstacle setWall)
    {
        var wallDir = setWall.blockUnder ? MapVec.Up : MapVec.Right;

        MapPos wallPos0 = new(setWall.hierarchyIndex, setWall.pos);
        MapPos wallPos1 = new(setWall.hierarchyIndex, setWall.pos + wallDir);

        if (!_map.IsInRange(wallPos0) || !_map.IsInRange(wallPos1))
        {
            Debug.Log("�}�b�v�̕����܂߂��͈͊O�ɕǂ�u�����Ƃ��Ă��܂�");
            return;
        }

        var map = _map[setWall.hierarchyIndex];
        map.AddWall(new Wall_Obstacle(setWall.pos, wallDir));

        Vector3 worldPos = (map.MapToWorld(wallPos0.gridPos) + map.MapToWorld(wallPos1.gridPos)) / 2.0f;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(wallDir.x, 0, wallDir.y));
        Instantiate(_wallObstacleObject, worldPos, rotation);
    }

}
