

[System.Serializable]
public struct MapWall_Obstacle
{
    public int hierarchyIndex;
    public MapVec pos;
    public bool blockUnder;     // カメラから見てマスの下側または右側に配置する

    public MapWall_Obstacle(int hierarchyIndex, MapVec pos, bool blockUnder)
    {
        this.hierarchyIndex = hierarchyIndex;
        this.pos = pos;
        this.blockUnder = blockUnder;
    }

    public MapWall_Obstacle(MapPos mapPos, bool blockUnder)
    {
        this.hierarchyIndex = mapPos.hierarchyIndex;
        this.pos = mapPos.gridPos;
        this.blockUnder = blockUnder;
    }
}
