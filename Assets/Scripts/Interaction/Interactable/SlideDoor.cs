using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    private Door _door;

    private void Start()
    {
        _door = GetComponentInParent<Door>();
    }
    void Sounds()
    {
        if (_door.isOpen && !_door.isLocked) Open();
        else Close();
    }

    void Open()
    {
        if (gameObject.CompareTag("SlideDoor") || gameObject.CompareTag("Window"))
        {
            _door.audioSource.clip = _door.doorSounds[4];
            _door.audioSource.Play();
        }
    }

    void Close()
    {
        if (gameObject.CompareTag("SlideDoor") || gameObject.CompareTag("Window"))
        {
            _door.audioSource.clip = _door.doorSounds[5];
            _door.audioSource.Play();
        }
    }
}
