using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer : MonoBehaviour
{

    public bool isOn = true;
    public AudioSource audio;
    public Animator anim;

    private void Start()
    {
        StartCoroutine(WaitForTrue());

        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isOn)
        {
            audio.Stop();
            anim.SetBool("isOn", false);
        }
    }

    IEnumerator WaitForTrue()
    {
        yield return new WaitForSeconds(1f);
        if (isOn)
        {
            audio.Play();
            anim.SetBool("isOn", true);
        }
    }

}
