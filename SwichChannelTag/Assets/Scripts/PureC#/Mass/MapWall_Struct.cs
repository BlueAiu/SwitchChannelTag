

[System.Serializable]
public struct MapWall_Obstacle
{
    public int hierarchyIndex;
    public MapVec pos;
    public bool blockUnder;     // カメラから見てマスの下側または右側に配置する
}
