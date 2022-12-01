using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixFps : MonoBehaviour
{
    public int fps;

    void Start()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = fps;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
