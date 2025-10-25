
public class PathOfMap
{
    public const int unexplored = -1;

    public struct PathInfo
    {
        public int step;
        public MapVec dir;

        public PathInfo(int step, MapVec dir)
        {
            this.step = step;
            this.dir = dir;
        }
    }

    PathInfo[,] _mass;

    public PathInfo this[MapVec pos]
    {
        get { return _mass[pos.y, pos.x]; }
        set { _mass[pos.y, pos.x] = value; }
    }

    public PathOfMap(MapVec size)
    {
        //マスのサイズの確定
        _mass = new PathInfo[size.y, size.x];

        Clear();
    }

    public void Clear()
    {
        for (int i = 0; i < _mass.GetLength(0); i++)
        {
            for (int j = 0; j < _mass.GetLength(1); j++)
            {
                _mass[i, j].step = unexplored;
                _mass[i, j].dir = new MapVec(0, 0);
            }
        }
    }
}
