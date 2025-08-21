using Photon.Pun;
using UnityEngine;

public class SceneTransition : MonoBehaviour 
{
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void LoadScene(string sceneName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
}
