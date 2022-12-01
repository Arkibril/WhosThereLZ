using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Footstep : MonoBehaviour
{
    private FirstPersonController firstPersonController;
    private Player _player;
    public AudioSource audio;
    public AudioSource audioRun;
    public bool isRuning;
    void Start()
    {
        firstPersonController = this.GetComponent<FirstPersonController>();
        _player = this.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Vertical") && Input.GetKey(KeyCode.LeftShift) && _player.m_sprint == true)
        {
              isRuning = true;
        }
        else 
        {
            isRuning = false;
        }

        if (firstPersonController.GetComponent<FirstPersonController>().m_isGrounded == true && Input.GetButton("Horizontal") && audio.isPlaying == false && isRuning == false)
        {
            audio.volume = Random.Range(0.3f, 9.0f);
            audio.pitch = Random.Range(0.8f, 1.5f);
            audio.Play();

            if (isRuning) 
            {
                audio.volume = Random.Range(0.3f, 9.0f);
                audio.pitch = Random.Range(0.8f, 1.5f);
                audioRun.Play();
            }
        }

        if (firstPersonController.GetComponent<FirstPersonController>().m_isGrounded == true && Input.GetButton("Vertical") && audio.isPlaying == false && isRuning == false)
        {
            audio.volume = Random.Range(0.3f, 9.0f);
            audio.pitch = Random.Range(0.8f, 1.5f);
            audio.Play();
        }

        if (firstPersonController.GetComponent<FirstPersonController>().m_isGrounded == true && Input.GetButton("Vertical") && audioRun.isPlaying == false && isRuning == true)
        {
            audioRun.volume = Random.Range(0.3f, 9.0f);
            audioRun.pitch = Random.Range(1.6f, 2.5f);
            audioRun.Play();
        }
    }
}
