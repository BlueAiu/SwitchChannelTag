using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DecidePath
{
    Stack<MapVec> moveHistory = new();

    public MapVec[] MoveHistory { get =>  moveHistory.ToArray(); }
    public int MoveSteps { get => moveHistory.Count; }

    public bool WhetherUndoMove(MapVec newGridPos)
    {
        if (moveHistory.Count == 0) return false;
        return moveHistory.Peek() == newGridPos;
    }

    public void ClearMoveHistory()
    {
        moveHistory.Clear();
        ClearPath();
    }

    // ˆê‚Â‚à‚Ç‚é‘€ì‚ğ‚µ‚½‚©‚Ì‰Â”Û‚ğ•Ô‚·
    // return bool whether did undo move
    bool UpdateMoveHistory(MapVec curGridPos, MapVec newGridPos)
    {
        if(WhetherUndoMove(newGridPos))
        {
            moveHistory.Pop();
            return true;
        }
        else
        {
            moveHistory.Push(curGridPos);
            return false;
        }
    }
}
