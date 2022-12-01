using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateObject : MonoBehaviour
{
    public GameObject Object;
    void Start()
    {
        Object.SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
