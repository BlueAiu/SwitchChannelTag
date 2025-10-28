using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine;

//�쐬��:���R
//�Q�[���̓��v���
//�o�߃^�[�����̊Ǘ�

public class GameStatsManager : MonoBehaviourPunCallbacks
{
    private const string TURN_KEY = "TurnNum";
    const int _invalidTurnNum = -1;

    public static GameStatsManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }



    public void SetTurn(int newTurnNum)//�^�[������ݒ�
    {
        Hashtable props = new Hashtable();
        props[TURN_KEY] = newTurnNum;
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }

    public int GetTurn()//�^�[�������擾
    {
        if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(TURN_KEY))
        {
            return (int)PhotonNetwork.CurrentRoom.CustomProperties[TURN_KEY];
        }

        Debug.Log("�^�[�����̎擾�Ɏ��s");
        return _invalidTurnNum;
    }
}
