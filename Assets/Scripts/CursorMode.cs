using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorMode : MonoBehaviour
{
    public bool cursorLocked;
    public bool CursorUnlocked;
    public bool cursorVisible;
    public bool cursorNotVisible;
    public bool ScreenLock;

    public static UnityEngine.CursorMode ForceSoftware { get; internal set; }

    void LateUpdate()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (CursorUnlocked)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (CursorUnlocked)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (cursorVisible)
        {
            Cursor.visible = true;
        }
        else if (cursorNotVisible)
        {
            Cursor.visible = false;
        }
        if (ScreenLock) 
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}