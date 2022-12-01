using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closets : MonoBehaviour
{
    public bool isIn;
    public bool isHide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Mark")
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
