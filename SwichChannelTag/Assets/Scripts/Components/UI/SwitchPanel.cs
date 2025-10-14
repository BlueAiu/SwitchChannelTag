using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者：木村
public class SwitchPanel : MonoBehaviour
{
    [Tooltip("表示、非表示させるパネル")]
    [SerializeField] GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        if(Panel == null)
        {
            Debug.LogError("The object is not attached!");
        }
    }

    public void SignPanel()
    {
        if(!Panel.activeSelf) 
        {
            Panel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
    }
}
