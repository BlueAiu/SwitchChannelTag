using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RouteSearch : MonoBehaviour
{
    MapTransform _myMapTrs;
    PathOfMap _path;

    int _mapHeight, _mapWidth;
    [SerializeField] bool writeLog = true;

    readonly MapVec[] direction =
    {
        MapVec.Up, MapVec.Down, MapVec.Left, MapVec.Right,
    };

    private void Awake()
    {
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        _mapHeight = _myMapTrs.CurrentHierarchy.MapSize_Y;
        _mapWidth = _myMapTrs.CurrentHierarchy.MapSize_X;
        _path = new(new MapVec(_mapWidth, _mapHeight));
    }

    public PathOfMap SearchPath(int hierarchy, MapVec start)
    {
        Queue<MapVec> que = new();
        que.Enqueue(start);

        _path.Clear();
        _path[start] = new PathOfMap.PathInfo(0, new MapVec(0, 0));

        while (que.Count > 0)
        {
            var cur = que.Dequeue();
            foreach(var d in direction)
            {
                var next = cur + d;

                // canMove
                var currentHierarchy = _myMapTrs.CurrentHierarchy;
                if (!currentHierarchy.IsInRange(next)) continue;
                if (currentHierarchy.Mass[next] != E_Mass.Empty) continue;
                if (currentHierarchy.IsBlockedByWall(cur,d)) continue;

                // explored
                if (_path[next].step != PathOfMap.unexplored) continue;

                que.Enqueue(next);
                var p = _path[cur];
                p.step++;
                p.dir = d;
                _path[next] = p;
            }
        }

        WriteLog();

        return _path;
    }

    void WriteLog()
    {
        if(!writeLog) return;

        string log = "bfs: \n";

        for(int i = 0; i < _mapWidth; i++)
        {
            for (int j = 0; j < _mapHeight; j++) 
            {
                MapVec v = new(j, i);
                int step = _path[v].step;
                var dir = _path[v].dir;
                char c_dir = 'o';
                if (dir == MapVec.Up) c_dir = '^';
                else if (dir == MapVec.Down) c_dir = 'v';
                else if (dir == MapVec.Left) c_dir = '>';
                else if (dir == MapVec.Right) c_dir = '<';
                log += string.Format("({0},{1}) ", step, c_dir);
            }
            log += '\n';
        }

        Debug.Log(log);
    }
}
