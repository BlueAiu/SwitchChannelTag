using Photon.Pun;

//作成者:杉山
//部屋を出た時にPlayersManagerの中のデータをClearする

public class ClearPlayersManagerOnLeftRoom : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom()
    {
        PlayersManager.Clear();
    }
}
