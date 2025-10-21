using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteSearch : MonoBehaviour
{
    MapTransform _myMapTrs;
    PathOfMap _path;

    private void Awake()
    {
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        _path = new(new MapVec(_myMapTrs.CurrentHierarchy.MapSize_X, _myMapTrs.CurrentHierarchy.MapSize_Y));
    }

    public PathOfMap SearchPath(int hierarchy, MapVec start)
    {

        return null;
    }
}
