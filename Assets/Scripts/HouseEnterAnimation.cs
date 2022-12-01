using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseEnterAnimation : MonoBehaviour
{
    public GameObject Text;
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public HouseEnterAnimation() 
    {
        if (Input.anyKeyDown)
        {
            anim.Play("StartCam");
            Text.SetActive(false);
        }
    }
}
