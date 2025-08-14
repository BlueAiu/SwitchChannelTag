using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

//ì¬Ò:™R
//ƒLƒƒƒ‰‚Ìƒ}ƒbƒvã‚ÌˆÊ’uî•ñ

public partial class MapTransform : MonoBehaviour
{
    [Tooltip("ˆÚ“®‚·‚éŠK‘wˆê——")][SerializeField] Map_A_Hierarchy[] _hierarchies;
    [Tooltip("“®‚©‚·‘ÎÛ")][SerializeField] Transform _target;
    [Tooltip("‰ŠúˆÊ’u")][SerializeField] MapVec _startPoint;
    [Tooltip("‰ŠúŠK‘w”Ô†")][SerializeField] int _initHierarchyIndex;

    private MapVec _currentPos;//Œ»İ‚ÌˆÊ’uî•ñ
    private int _currentHierarchyIndex;//Œ»İ‚ÌŠK‘w”Ô†


    public Transform Target { get { return _target; } }//“®‚©‚·‘ÎÛ

    public MapVec CurrentPos { get { return _currentPos; } }//Œ»İ‚Ìƒ}ƒbƒvã‚ÌˆÊ’u
    public Vector3 CurrentWorldPos { get { return CurrentHierarchy.MapToWorld(_currentPos); } }//Œ»İ‚Ìƒ[ƒ‹ƒhã‚ÌˆÊ’u

    public int CurrentHierarchyIndex { get { return _currentHierarchyIndex; } }//Œ»İ‚ÌŠK‘w”Ô†
    public Map_A_Hierarchy CurrentHierarchy { get { return _hierarchies[CurrentHierarchyIndex]; } }//Œ»İ‚ÌŠK‘w
    public Map_A_Hierarchy[] Hierarchies { get { return _hierarchies; } }//ˆÚ“®‚·‚éŠK‘wˆê——

    public void RewritePos(MapVec newMapVec, int newHierarchyIndex)//ˆÊ’u‚ÆŠK‘w‚Ì‘‚«Š·‚¦
    {
        _currentHierarchyIndex = newHierarchyIndex;
        RewritePos(newMapVec);
    }

    public void RewritePos(int newHierarchyIndex)//ŠK‘w‚Ì‚İ‚Ì‘‚«Š·‚¦
    {
        RewritePos(_currentPos,newHierarchyIndex);
    }

    public void RewritePos(MapVec newMapVec)//ˆÊ’u‚Ì‚İ‚Ì‘‚«Š·‚¦
    {
        _currentPos = newMapVec;
        Vector3 newPos = CurrentHierarchy.MapToWorld(_currentPos);
        _target.position = newPos;
    }


    //private

    void Start()
    {
        //‰ŠúˆÊ’u‚Ìİ’è
        RewritePos(_startPoint, _initHierarchyIndex);
    }
}
