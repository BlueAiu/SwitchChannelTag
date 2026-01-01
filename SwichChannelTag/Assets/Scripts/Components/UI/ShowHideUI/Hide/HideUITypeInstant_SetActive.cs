using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//SetActive‚ÅUI‚ğ‘¦”ñ•\¦‚·‚é

public class HideUITypeInstant_SetActive : HideUITypeBase
{
    [Tooltip("”ñ•\¦‚É‚µ‚½‚¢UIŒQ")] [SerializeField]
    GameObject _hideUIObject;

    public override bool IsFinishedToHide()
    {
        return true;
    }

    public override void Hide()
    {
        _hideUIObject.SetActive(false);
    }
}
