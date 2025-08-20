using Photon.Pun;
using UnityEngine;

public class ReadinessManager : MonoBehaviour
{
    [SerializeField] string mainSceneName = "MainScene";

    public void TryStartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        var playerReadis = GetComponentInParent<PlayersManager>().
            GetComponentsFromPlayers<GettingReady>();

        if (IsReadyAll(playerReadis))
        {
            GetComponentInParent<SceneTransition>().LoadScene(mainSceneName);
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
