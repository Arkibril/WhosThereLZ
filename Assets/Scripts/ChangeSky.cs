using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using RenderSettings = UnityEngine.RenderSettings;

public class ChangeSky : MonoBehaviour
{
    public Material skyMaterial;
    void Start()
    {
        RenderSettings.skybox = skyMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
