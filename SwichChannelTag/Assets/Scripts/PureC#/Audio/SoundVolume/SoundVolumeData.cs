using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//‰¹—Ê

public class SoundVolumeData
{
    float _value;//‰¹—Ê

    public SoundVolumeData(float value)
    {
        _value = value;
    }

    public float Value
    {
        get { return _value; }
        set {  _value = value; }
    }
}
