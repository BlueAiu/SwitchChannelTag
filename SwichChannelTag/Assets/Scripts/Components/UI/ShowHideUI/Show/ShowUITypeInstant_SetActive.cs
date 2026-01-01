//ì¬Ò:™R
//SetActive‚ÅUI‚ğ‘¦”ñ•\¦‚·‚é

using UnityEngine;

public class ShowUITypeInstant_SetActive : ShowUITypeBase
{
    [Tooltip("•\¦‚µ‚½‚¢UIŒQ")] [SerializeField]
    GameObject _showUIObject;

    public override bool IsFinishedToShow()
    {
        return true;
    }

    public override void Show()
    {
        _showUIObject.SetActive(true);
    }
}
