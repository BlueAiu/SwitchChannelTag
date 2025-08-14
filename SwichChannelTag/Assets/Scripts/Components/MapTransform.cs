using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

//ì¬Ò:™R
//ƒLƒƒƒ‰‚Ìƒ}ƒbƒvã‚ÌˆÊ’uî•ñ

public partial class MapTransform : MonoBehaviour
{
    [Tooltip("ˆÚ“®‚·‚éŠK‘wˆê——")][SerializeField] Map_A_Hierarchy[] _hierarchies;
    [Tooltip("“®‚©‚·‘ÎÛ")][SerializeField] Transform _target;
    [Tooltip("ˆÊ’u")][SerializeField] MapVec _pos;
    [Tooltip("ŠK‘w”Ô†")][SerializeField] int _hierarchyIndex;


    public Transform Target { get { return _target; } }//“®‚©‚·‘ÎÛ

    public MapVec Pos//Œ»İ‚Ìƒ}ƒbƒvã‚ÌˆÊ’u
    {
        get { return _pos; }
        set { RewritePos(value, _hierarchyIndex); }
    }
    public Vector3 CurrentWorldPos { get { return CurrentHierarchy.MapToWorld(_pos); } }//Œ»İ‚Ìƒ[ƒ‹ƒhã‚ÌˆÊ’u

    public int HierarchyIndex //Œ»İ‚ÌŠK‘w”Ô†
    {
        get { return _hierarchyIndex; }
        set { RewritePos(_pos, value); }
    }
    public Map_A_Hierarchy CurrentHierarchy { get { return _hierarchies[_hierarchyIndex]; } }//Œ»İ‚ÌŠK‘w
    public Map_A_Hierarchy[] Hierarchies { get { return _hierarchies; } }//ˆÚ“®‚·‚éŠK‘wˆê——





    //private

    void RewritePos(MapVec newMapVec, int newHierarchyIndex)//ˆÊ’u‚ÆŠK‘w‚Ì‘‚«Š·‚¦
    {
        if(_target==null)
        {
            Debug.Log("Target‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñI");
            return;
        }

        //ˆÊ’u‚ª”ÍˆÍŠO‚¾‚Á‚½‚çŒx‚µ‚Ä’e‚­
        if (!CurrentHierarchy.IsInRange(newMapVec))
        {
            Debug.Log(newMapVec + "‚Í”ÍˆÍŠO‚ÌˆÊ’u‚Å‚·I");
            return;
        }

        //ŠK‘w”Ô†‚ª”ÍˆÍŠO‚¾‚Á‚½‚çŒx•”ÍˆÍ“à‚Éû‚ß‚é
        if (_hierarchyIndex < 0 || _hierarchyIndex >= _hierarchies.Length)
        {
            Debug.Log(_hierarchyIndex + "‚Í”ÍˆÍŠO‚ÌŠK‘w”Ô†‚Å‚·I");
            return;
        }

        _hierarchyIndex = newHierarchyIndex;
        _pos = newMapVec;
        Vector3 newPos = CurrentWorldPos;
        _target.position = newPos;
    }

    void Start()
    {
        RewritePos(_pos, _hierarchyIndex);
    }

    private void OnValidate()
    {
        RewritePos(_pos, _hierarchyIndex);
    }
}
