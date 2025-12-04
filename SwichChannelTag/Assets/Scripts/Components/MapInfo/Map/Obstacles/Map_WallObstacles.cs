using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Map_A_Hierarchy : MonoBehaviour 
{
    HashSet<Wall_Obstacle> _walls = new();
   
    public void AddWall(Wall_Obstacle wall)
    {
        _walls.Add(wall);
        
        var nega_wall = new Wall_Obstacle(wall.pos + wall.dir, wall.dir * -1);
        _walls.Add(nega_wall);
    }

    public bool IsBlockedByWall(MapVec pos, MapVec dir)
    {
        return _walls.Contains(new Wall_Obstacle(pos, dir));
    }
}
