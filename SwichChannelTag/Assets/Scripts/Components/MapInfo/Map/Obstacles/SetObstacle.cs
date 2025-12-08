using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//作成者:杉山
//マップ上に障害物を置く

public class SetObstacle : MonoBehaviourPunCallbacks
{

    const int mapRows = 9;

    [SerializeField] Maps_Hierarchies _map;
    [SerializeField] GameObject _obstacleObject;
    [SerializeField] GameObject _wallObstacleObject;

    [SerializeField] float _wallTiltAngle = 15f;

    [SerializeField, TextArea(mapRows,mapRows)]
    [Tooltip("文字列のリストで障害物を設定します\n" +
        ". : 何も置かない\n" +
        "_ : 下に壁を置く\n" +
        "| : 左に壁を置く\n" +
        "L : 下と左に壁を置く\n" +
        "# : 障害物を置く\n" +
        "注意\n" +
        "文字列の長さ、リストのサイズに注意して下さい")]
    public string[] _obstaclesMaps;

    [SerializeField, TextArea(mapRows, mapRows)]
    public string[] _obstaclesMaps_2;

    List<GameObject> InstanceObs = new();

    void Start()
    {
        SetObstacleMap(_obstaclesMaps);
    }

    public void ChangeObstacleMap()
    {
        SetObstacleMap(_obstaclesMaps_2);
    }

    void SetObstacleMap(string[] setMap)
    {
        foreach (var i in InstanceObs) Destroy(i);
        InstanceObs.Clear();
        for(int i=0;i<_map.Length;i++) _map[i].ClearWalls();


        if (setMap.Length != _map.Length)
        { Debug.LogAssertion("ObstacleMapsのサイズを階層の数と揃えてください"); }

        for (int i = 0; i < _obstaclesMaps.Length; i++)
        {
            var map = setMap[i].Split('\n');
            if (map.Length != _map[i].MapSize_Y)
            { Debug.LogAssertionFormat("ObstacleMaps[{0}]のサイズをマップの高さと揃えてください", i); }

            for (int h = 0; h < map.Length; h++)
            {
                var str = map[h];

                for (int w = 0; w < str.Length; w++)
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

            default:
                Debug.LogWarning("指定以外の文字が確認されました \nこの文字は'.'とみなします");
                break;
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
        //範囲外であれば、生成失敗
        if (!_map.IsInRange(_obstaclePos))
        {
            Debug.Log("範囲外なので生成失敗");
            return;
        }

        Map_A_Hierarchy map = _map[_obstaclePos.hierarchyIndex];

        Vector3 pos = map.MapToWorld(_obstaclePos.gridPos);
        GameObject obstacleInstance = Instantiate(_obstacleObject);//障害物オブジェクトを生成
        obstacleInstance.transform.position = pos;
        obstacleInstance.GetComponent<ObstacleModel>().ChangeModel(_obstaclePos.hierarchyIndex);

        map.Mass[_obstaclePos.gridPos] = E_Mass.Obstacle;
    }

    void PutWallObstacle(MapWall_Obstacle setWall)
    {
        var wallDir = setWall.blockUnder ? MapVec.Up : MapVec.Left;

        MapPos wallPos0 = new(setWall.hierarchyIndex, setWall.pos);
        MapPos wallPos1 = new(setWall.hierarchyIndex, setWall.pos + wallDir);

        if (!_map.IsInRange(wallPos0) || !_map.IsInRange(wallPos1))
        {
            Debug.Log("マップの淵を含めた範囲外に壁を置こうとしています");
            return;
        }

        var map = _map[setWall.hierarchyIndex];
        map.AddWall(new Wall_Obstacle(setWall.pos, wallDir));

        Vector3 worldPos = (map.MapToWorld(wallPos0.gridPos) + map.MapToWorld(wallPos1.gridPos)) / 2.0f;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(wallDir.x, 0, wallDir.y));
        var i_wall = Instantiate(_wallObstacleObject, worldPos, rotation);
        i_wall.GetComponent<ObstacleModel>().ChangeModel(setWall.hierarchyIndex);

        bool isGridEven = (wallPos0.gridPos.x + wallPos0.gridPos.y) % 2 == 0;
        i_wall.transform.Rotate(Vector3.up * (isGridEven ? _wallTiltAngle : -_wallTiltAngle));

        InstanceObs.Add(i_wall);
    }

}
