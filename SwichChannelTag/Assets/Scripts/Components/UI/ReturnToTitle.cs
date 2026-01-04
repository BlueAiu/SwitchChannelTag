using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//作成者:杉山
//部屋から出てタイトルに戻る機能

public class ReturnToTitle : MonoBehaviourPunCallbacks
{
    [Tooltip("遷移の待ち時間")]
    [SerializeField] private float _waitTime = 0.5f;

    bool _requestReturnToTitle=false;

    // ボタンから呼ぶ
    public void OnClickReturnToTitle()
    {
        if (_requestReturnToTitle) return;

        _requestReturnToTitle = true;

        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        else if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        else
        {
            StartCoroutine(LoadTitle());
        }
    }

    public override void OnLeftRoom()
    {
        if (_requestReturnToTitle)
        {
            PhotonNetwork.Disconnect();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (_requestReturnToTitle)
        {
            StartCoroutine(LoadTitle());
        }
    }

    IEnumerator LoadTitle()
    {
        yield return new WaitForSeconds(_waitTime);

        SceneManager.LoadScene("TitleScene");
    }
}
