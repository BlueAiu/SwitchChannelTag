using Photon.Pun;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviourPunCallbacks
{
    [Tooltip("‘JˆÚ‚Ì‘Ò‚¿ŽžŠÔ")]
    [SerializeField] private float Waittime;

    public void MoveTitletoLobby()
    {
        StartCoroutine(LoadTime(Waittime, () =>
        {
            SceneManager.LoadScene("LobbyScene");
        }));
    }

    public void MoveLobbytoTitle()
    {
        StartCoroutine(LoadTime(Waittime, () =>
        {
            
            PhotonNetwork.LeaveRoom();
            
        }));
    }

    private IEnumerator LoadTime(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

    private void Destroyobj()
    {
        var obj = new GameObject("LobbySceneObj");
        DontDestroyOnLoad(obj);
        var dontDestroyScene = obj.scene;
        DestroyImmediate(obj);

        foreach (var rootObj in dontDestroyScene.GetRootGameObjects())
        {
            Destroy(rootObj);
        }

        Debug.Log("Destroyed");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left room");
        Destroyobj();
        SceneManager.LoadScene("TitleScene");
    }
}
