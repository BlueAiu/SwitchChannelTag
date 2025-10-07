using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
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

    private IEnumerator LoadTime(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }
}
