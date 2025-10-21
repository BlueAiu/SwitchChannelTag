

[System.Serializable]
public struct MapWall_Obstacle
{
    public int hierarchyIndex;
    public MapVec pos;
    public bool blockUnder;     // �J�������猩�ă}�X�̉����܂��͉E���ɔz�u����

    public MapWall_Obstacle(MapPos mapPos, bool blockUnder)
    {
        this.hierarchyIndex = mapPos.hierarchyIndex;
        this.pos = mapPos.gridPos;
        this.blockUnder = blockUnder;
    }
}
