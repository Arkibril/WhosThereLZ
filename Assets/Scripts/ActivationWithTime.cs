using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationWithTime : MonoBehaviour
{
    public float Time;
    public GameObject Object;
    void Start()
    {
        StartCoroutine(TimeActivation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimeActivation() 
    {
        yield return new WaitForSeconds(Time);
        Object.SetActive(true);
    }
}
