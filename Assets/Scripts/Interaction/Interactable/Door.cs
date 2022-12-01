using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked;
    public bool isOpen;

    public GameObject chairOnDoor = null;

    public AudioClip[] doorSounds;
    public AudioSource audioSource;

    private void Update()
    {
        audioSource = GetComponent<AudioSource>();

        CheckIfLocked();
    }

    void CheckIfLocked()
    {
        if (chairOnDoor != null && !chairOnDoor.GetComponent<Chair>().onDoor)
        {
            chairOnDoor = null;
        }

        if (chairOnDoor != null)
        {
            isLocked = true;
        }
        else
        {
            isLocked = false;
        }
    }

    void Sounds()
    {
        if (isOpen && !isLocked) Open();
        else Close();
    }

    void Open()
    {
        if (gameObject.CompareTag("MainDoor") || gameObject.CompareTag("RoomDoor"))
        {
            audioSource.clip = doorSounds[2];
            audioSource.Play();
        }

    }

    void Close()
    {
        if (gameObject.CompareTag("MainDoor") || gameObject.CompareTag("RoomDoor"))
        {
            audioSource.clip = doorSounds[1];
            audioSource.Play();
        }
    }
}

