using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//ŠK‘w‚ğˆÚ“®‚ÉcinemachineƒJƒƒ‰‚Ì–Ú•W(Follow/LookAt)‚ğ•ÏX‚·‚é

public class SwitchCameraTarget_Hierarchy : MonoBehaviour
{
    [Tooltip("ŠK‘w‚²‚Æ‚Ì–Ú•W(Follow/LookAt)\n0ŒÂ–Ú‚©‚ç“V‘->’nã->’n–‚Ì‡‚É“ü‚ê‚Ä‚­‚¾‚³‚¢")]
    [SerializeField]
    Transform[] _mapVCamTargets;

    [SerializeField]
    CinemachineVirtualCamera _vCam;

    [SerializeField]
    ChangeHierarchy _changeHierarchy;

    [SerializeField]
    Maps_Hierarchies _maps_Hierarchies;

    public void Switch(int hierarchyIndex)//Ê‚·ŠK‘w‚ÌØ‚è‘Ö‚¦
    {
        if (!IsValid_VCamLength()) return;

        if (!_maps_Hierarchies.IsInRange(hierarchyIndex))
        {
            Debug.Log("”ÍˆÍŠO‚ÌŠK‘w”Ô†‚Å‚·I");
            return;
        }

        _vCam.Follow = _mapVCamTargets[hierarchyIndex];
        _vCam.LookAt = _mapVCamTargets[hierarchyIndex];
    }



    //private
    private void Awake()
    {
        IsValid_VCamLength();
    }

    private void OnEnable()
    {
        _changeHierarchy.OnSwitchHierarchy_NewIndex += Switch;
    }

    private void OnDisable()
    {
        _changeHierarchy.OnSwitchHierarchy_NewIndex -= Switch;
    }

    private void Start()
    {
        StartCoroutine(InitCamera());
    }

    IEnumerator InitCamera()
    {
        yield return new WaitUntil(CheckIsInitManager.Instance.GetIsInited); // wait InitPlayersPos
        MapTransform myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        Switch(myMapTrs.Pos.hierarchyIndex);
    }

    bool IsValid_VCamLength()
    {
        if (_mapVCamTargets.Length != _maps_Hierarchies.Length)
        {
            Debug.Log("ŠK‘w‚Ì”•ªA–Ú•W(Follow/LookAt)‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñI");
            return false;
        }

        return true;
    }
}
