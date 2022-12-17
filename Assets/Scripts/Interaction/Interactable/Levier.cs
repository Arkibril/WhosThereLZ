using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : MonoBehaviour
{

    public bool isOn;

    public Material mat;
    public Material[] lightEnabled;
    public Material[] lightDisabled;

    public Animator anim;
    [SerializeField]
    private Light[] _light;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isOn", true);

        _light = FindObjectsOfType<Light>();
        ActivateLight();

        isOn = true;
    }

    private void Update()
    {
        if (isOn)
        {
            mat.color = Color.green;
            mat.SetColor("_EmissionColor", Color.green);
        }
        else
        {
            mat.color = Color.red;
            mat.SetColor("_EmissionColor", Color.red);
        }
    }

    public void ActivateLight()
    {
        for(int i = 0; i < _light.Length; i++)
        {
            if(!_light[i].CompareTag("Light"))
            {
                _light[i].GetComponent<Light>().enabled = true;
            }           
        }
    }

    public void DesactivateLight()
    {
        for (int i = 0; i < _light.Length; i++)
        {
            if (!_light[i].CompareTag("Light"))
            {
                _light[i].GetComponent<Light>().enabled = false;
            }
        }
    }

}
