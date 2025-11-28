
[System.Serializable]
public struct Wall_Obstacle
{
    public MapVec pos;
    public MapVec dir;

    public Wall_Obstacle(MapVec pos, MapVec dir)
    {
        this.pos = pos;
        this.dir = dir;
    }
} 