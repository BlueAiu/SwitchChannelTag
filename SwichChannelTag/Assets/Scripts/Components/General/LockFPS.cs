using UnityEngine;

//ì¬Ò:™R
//FPS‚ğŒÅ’è‚³‚¹‚é‹@”\

public class LockFPS : MonoBehaviour
{
    [SerializeField]
    int _frameRate = 60;

    private void Start()
    {
        Application.targetFrameRate = _frameRate;
    }
}
