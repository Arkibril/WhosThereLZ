using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armoire : MonoBehaviour
{
    public bool isOpen;
    public AudioClip[] doorSounds;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Sounds()
    {
        if (isOpen) Open();
        else Close();
    }

    void Open()
    {
        if (gameObject.CompareTag("Armoire1"))
        {
            audioSource.clip = doorSounds[2];
            audioSource.Play();
        }
        else if (gameObject.CompareTag("Drawer"))
        {
            audioSource.clip = doorSounds[0];
            audioSource.Play();
        }

    }

    void Close()
    {
        if (gameObject.CompareTag("Armoire1"))
        {
            audioSource.clip = doorSounds[3];
            audioSource.Play();
        }
        else if (gameObject.CompareTag("Drawer"))
        {
            audioSource.clip = doorSounds[1];
            audioSource.Play();
        }
    }
}
