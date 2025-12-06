using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ž©•ª‚ÌƒLƒƒƒ‰‚ðŒ³‚ÌŒü‚«‚É–ß‚·

public class LookDefaultDirection : MonoBehaviour
{
    Quaternion _lookDir;
    Transform _myTrs;

    public void LookDefault()
    {
        _myTrs.rotation = _lookDir;
    }

    private void Awake()
    {
        _lookDir=Quaternion.LookRotation(Vector3.forward);
        _myTrs = PlayersManager.GetComponentFromMinePlayer<Transform>();
    }
}
