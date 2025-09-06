using Photon.Pun;
using UnityEngine;

public class ReadinessManager : MonoBehaviour
{
    [SerializeField] string mainSceneName = "MainScene";
    [SerializeField] PlayersManager playersManager;
    [SerializeField] SceneTransition sceneTransition;

    public void TryStartGame()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        var playerReadis = playersManager.GetComponentsFromPlayers<GettingReady>();

        if (IsReadyAll(playerReadis))
        {
            sceneTransition.LoadScene(mainSceneName);
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
