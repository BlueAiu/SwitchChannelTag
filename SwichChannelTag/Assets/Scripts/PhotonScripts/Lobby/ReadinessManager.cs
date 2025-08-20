using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class ReadinessManager : MonoBehaviour
{
    [SerializeField] SceneAsset mainScene;

    public void TryStartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        var playerReadis = GetComponentInParent<PlayersManager>().
            GetComponentsFromPlayers<GettingReady>();

        if (IsReadyAll(playerReadis))
        {
            GetComponentInParent<SceneTransition>().LoadScene(mainScene);
            GetComponent<JoinControl>().IsRoomOpened = false;
            enabled = false;
        }
    }

    bool IsReadyAll(GettingReady[] readies)
    {
        foreach(var i in readies)
        {
            if (!i.IsReady) return false;
        }

        return true;
    }
}
