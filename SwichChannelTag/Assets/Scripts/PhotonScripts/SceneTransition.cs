using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class SceneTransition : MonoBehaviour 
{
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void LoadScene(SceneAsset scene)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(scene.name);
        }
    }
}
