
using UnityEngine;
using System;
using UnityEngine.UI;

//ì¬Ò:™R
//ƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½‚Æ‚«‚ÉŠK‘w”Ô†‚ğ“n‚µ‚ÄA“o˜^‚³‚ê‚Ä‚¢‚½ŠÖ”‚ğŒÄ‚Ño‚·‹@”\

public class SelectHierarchyButton
{
    int _hierarchyIndex;

    Button _button;

    public event Action<int> OnSubmittedButton;//ƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½‚Æ‚«‚É“o˜^‚³‚ê‚½ŠK‘w”Ô†‚ğ“n‚µ‚ÄŠÖ”‚ğŒÄ‚Ño‚·

    public SelectHierarchyButton(int hierarchyIndex,Button button)
    {
        _hierarchyIndex=hierarchyIndex;
        _button=button;
        _button.onClick.AddListener(OnSubmitted);
    }

    void OnSubmitted()
    {
        OnSubmittedButton?.Invoke(_hierarchyIndex);
    }

    public Button Button { get { return _button; } }
    public int HierarchyIndex { get { return _hierarchyIndex; } }
}
