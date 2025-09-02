using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGet : MonoBehaviour
{
    [SerializeField] GetPlayerInfo _getPlayerInfo;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo[] objects = _getPlayerInfo.PlayerInfos;

        for(int i=0; i<objects.Length ;i++)
        {
            Debug.Log(objects[i].ActorNum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
