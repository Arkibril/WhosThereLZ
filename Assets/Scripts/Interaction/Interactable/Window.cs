using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public bool isIn;
    public bool isOpen;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Mark")
        {
            isIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Mark")
        {
            isIn = false;
        }
    }
}
