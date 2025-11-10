using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine;

//ì¬Ò:™R
//‰Šú‰»ˆ—‚ğÏ‚Ü‚¹‚½‚©‚ğ“`‚¦‚é

public class CheckIsInitManager : MonoBehaviourPunCallbacks
{
    private const string ISINITED_KEY = "IsInited";

    public static CheckIsInitManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public override void OnJoinedRoom()
    {
        SetIsInited(false);
    }

    public bool GetIsInited()
    {
        if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(ISINITED_KEY))
        {
            return (bool)PhotonNetwork.CurrentRoom.CustomProperties[ISINITED_KEY];
        }

        Debug.Log("æ“¾‚É¸”s");
        return false;
    }

    public void CompletedInit()//‰Šú‰»ˆ—‚ğŠ®—¹
    {
        SetIsInited(true);
    }

    void SetIsInited(bool value)
    {
        Hashtable props = new Hashtable();
        props[ISINITED_KEY] = value;
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }
}
