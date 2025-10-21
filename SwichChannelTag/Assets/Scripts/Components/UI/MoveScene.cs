using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [Tooltip("�J�ڂ̑҂�����")]
    [SerializeField] private float Waittime;
    DestroyObjects destroy;

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
            destroy.DestroyObj();
            SceneManager.LoadScene("TitleScene");
        }));
    }

    private IEnumerator LoadTime(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}
