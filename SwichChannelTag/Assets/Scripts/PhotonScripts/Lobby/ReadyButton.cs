using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    [SerializeField] TMP_Text selfText;
    [SerializeField] string unReadyText;
    [SerializeField] string readyText;

    //プレイヤー生成時参照を渡してもらう
    GettingReady ownPlayerReady;
    public GameObject OwnPlayer
    {
        set { ownPlayerReady = value.GetComponent<GettingReady>(); }
    }

    private void Start()
    {
        if(selfText == null)
        {
            selfText = GetComponentsInChildren<TMP_Text>()[0];
        }

        this.gameObject.SetActive(false);
    }

    public void SwitchReady()
    {
        if (selfText == null) return;

        ownPlayerReady.SwitchReady();
        selfText.text = ownPlayerReady.IsReady ? readyText : unReadyText;
    }

    /*private void Update()
    {
        ShowReady();
    }

    private void ShowReady()
    {
        if(ownPlayerReady != null)
        {
            this.gameObject.SetActive(true);
        }
    }*/
}
