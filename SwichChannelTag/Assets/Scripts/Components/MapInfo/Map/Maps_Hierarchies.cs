using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬ŽÒ:™ŽR
//‘S‚Ä‚ÌŠK‘w‚Ìƒ}ƒbƒv‚ÌŠÇ—

public class Maps_Hierarchies : MonoBehaviour
{
    [Tooltip("ˆÚ“®‚Å‚«‚éŠK‘wˆê——")][SerializeField] Map_A_Hierarchy[] _maps;

    public Map_A_Hierarchy this[int index]//ˆÚ“®‚Å‚«‚éŠK‘wˆê——
    {
        get { return _maps[index]; }
    }

    public int Length { get { return _maps.Length; } }//ŠK‘w‚Ì”

    public bool IsInRange(int hierarchyIndex)//ŠK‘w”Ô†‚Ì‚Ý‚ª”ÍˆÍ“à‚©‚ð”»’è
    {
        return hierarchyIndex >= 0 && hierarchyIndex < _maps.Length;
    }
    public bool IsInRange(MapPos pos)//ŠK‘w”Ô†‚Æƒ}ƒXÀ•W—¼•û‚ª”ÍˆÍ“à‚©‚ð”»’è
    {
        return IsInRange(pos.hierarchyIndex) && _maps[pos.hierarchyIndex].IsInRange(pos.gridPos);
    }
}
