using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//‘S‚Ä‚ÌŠK‘w‚Ìƒ}ƒbƒv‚ÌŠÇ—

public class MapsHierarchies : MonoBehaviour
{
    [Tooltip("ˆÚ“®‚Å‚«‚éŠK‘wˆê——")][SerializeField] Map_A_Hierarchy[] _maps;

    public Map_A_Hierarchy this[int index]//ˆÚ“®‚Å‚«‚éŠK‘wˆê——
    {
        get { return _maps[index]; }
    }

    public int Length { get { return _maps.Length; } }//ŠK‘w‚Ì”
}
