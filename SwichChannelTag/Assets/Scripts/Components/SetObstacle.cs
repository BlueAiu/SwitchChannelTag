using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//作成者:杉山
//マップ上に障害物を置く(テスト用)

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

            //範囲外であれば、生成失敗
            if (!_map.IsInRange(_obstaclePos))
            {
                Debug.Log("範囲外なので生成失敗");
                continue;
            }
            
            Map_A_Hierarchy map = _map[_obstaclePos.hierarchyIndex];

            Vector3 pos = map.MapToWorld(_obstaclePos.gridPos);
            GameObject obstacleInstance = Instantiate(_obstacleObject);//障害物オブジェクトを生成
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
                Debug.Log("マップの淵を含めた範囲外に壁を置こうとしています");
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
