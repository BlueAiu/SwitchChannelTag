using Photon.Pun;
using UnityEngine;

public class ReadinessManager : MonoBehaviour
{
    [SerializeField] string mainSceneName = "MainScene";

    public void TryStartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        var playerReadis = GetComponent<PlayersManager>().
            GetComponentsFromPlayers<GettingReady>();

        if (IsReadyAll(playerReadis))
        {
            GetComponent<SceneTransition>().LoadScene(mainSceneName);
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
