using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

//作成者:杉山
//シーン開始時にDontDestroyOnLoad内の全てのオブジェクトを破壊する機能

public class DestroyAllDontDestroyOnLoadObjects : MonoBehaviour
{
    void Start()
    {
        DestroyAll();
    }

    void DestroyAll()
    {
        // 一時的なオブジェクトを作成して DontDestroyOnLoad シーンを取得
        GameObject temp = new GameObject("Temp");
        DontDestroyOnLoad(temp);

        Scene dontDestroyScene = temp.scene;
        Destroy(temp);

        // DontDestroyOnLoad シーン内の全 GameObject を削除
        foreach (GameObject obj in dontDestroyScene.GetRootGameObjects())
        {
            //PhotonHandlerを消すと部屋に入れなくなるので、それ以外を消すようにする
            if (obj.GetComponent<PhotonHandler>() != null)
            {
                continue;
            }

            Destroy(obj);
        }
    }
}
